namespace Proektsoft.Numerical
{
    public static partial class Solver
    {
        // Finds the root of "F(x) = y0" within the interval [x1. x2]
        // with the specified precision, using the bisection method
        // F(x) must be coutinuous and sign(F(x1) - y0) ≠ sign(F(x2) - y0)

        public static double Bisection(Func<double, double> F,
            double x1, double x2, double y0 = 0.0, double precision = 1e-14)
        {
            if (!Initialize(x1, x2, F, y0, precision,
                out Node p1, out Node p2, out Node eps))
                return double.NaN;

            for (int i = 1; i <= MaxIterations; i++)
            {
                Node p3 = new(Node.Mid(p1, p2), F, y0);
                double d = Math.Abs(p2.X - p1.X);
                if (Math.Abs(p3.Y) <= eps.Y || d <= eps.X)
                {
                    if (d <= eps.X)
                        p3.X = Node.Sec(p1, p2);
                    IterationCount = i;
                    return p3.X;
                }
                if (Math.Sign(p1.Y) == Math.Sign(p3.Y))
                    p1 = p3;
                else
                    p2 = p3;
            }
            IterationCount = MaxIterations;
            return double.NaN;
        }
    }
}
