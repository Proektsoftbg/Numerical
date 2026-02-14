namespace Proektsoft.Numerical
{
    public static partial class Solver
    {
        // Finds the root of "F(x) = y0" within the interval [x1. x2]
        // with the specified precision, using the ITP method:
        // Oliveira, I. F. D. and Ricardo H. C. Takahashi.
        // “An Enhancement of the Bisection Method Average Performance Preserving Minmax Optimality.”
        // ACM Transactions on Mathematical Software (TOMS) 47 (2021): 1 – 24.
        // https://doi.org/10.1145/3423597
        // F(x) must be continuous and sign(F(x1) - y0) ≠ sign(F(x2) - y0)

        public static double ITP(Func<double, double> F,
            double x1, double x2, double y0 = 0.0, double precision = 1e-14)
        {
            if (!Initialize(x1, x2, F, y0, precision,
                out Node p1, out Node p2, out Node eps))
                return double.NaN;

            double eps2 = 2d * precision;
            double k1 = 0.2 / (x2 - x1), k2 = 2d;
            int n0 = 1;
            int nb = (int)Math.Ceiling(Math.Log2((p2.X - p1.X) / eps2));
            int nmax = nb + n0;
            for (int i = 1; i <= MaxIterations; i++)
            {
                double xb = Node.Mid(p1, p2);
                double xf = Node.Sec(p1, p2);
                double σ = Math.Sign(xb - xf);
                double δ = Math.Min(k1 * Math.Pow(p2.X - p1.X, k2), Math.Abs(xb - xf));
                double xt = xf + σ * δ;
                double rho = Math.Min(eps2 * Math.Pow(2d, nmax - i) - (p2.X - p1.X) / 2d, Math.Abs(xt - xb));
                xt = xb - σ * rho;
                Node pt = new(xt, F, y0);
                if (Math.Abs(pt.Y) <= eps.Y || p2.X - p1.X < eps.X)
                {
                    IterationCount = i;
                    return xt;
                }
                if (Math.Sign(p1.Y) == Math.Sign(pt.Y))
                    p1 = pt;
                else
                    p2 = pt;
            }
            IterationCount = MaxIterations;
            return double.NaN;
        }
    }
}
