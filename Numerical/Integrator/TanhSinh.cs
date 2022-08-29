namespace Proektsoft.Numerical
{
    public static partial class Integrator
    {
        private const int n = 7;
        private static readonly int[] m = { 6, 7, 13, 26, 53, 106, 212 };
        private static readonly double[][] r = new double[n][];
        private static readonly double[][] w = new double[n][];

        // Calculates the definite integral of the function "F"
        // in the interval [x1, x2] numerically, with the specified percision.
        // Implements the Tanh-Sinh quadrature
        // Takahasi, Hidetosi; Mori, Masatake (1974),
        // "Double Exponential Formulas for Numerical Integration",
        // Publications of the Research Institute for Mathematical Sciences, 9 (3): 721–741 
        // https://www.ems-ph.org/journals/show_pdf.php?issn=0034-5318&vol=9&iss=3&rank=12

        public static double TanhSinh(Func<double, double> F, 
            double x1, double x2, double Precision = 1e-14)
        {
            IterationCount = 1;
            double c = (x1 + x2) / 2.0;
            double d = (x2 - x1) / 2.0;
            double s = F(c);
            double v, err;
            int i = 0;
            double eps = Math.Max(Precision, 1e-12);
            double tol = 10.0 * eps;
            do
            {
                double q, p = 0.0, fp = 0.0, fm = 0.0;
                int j = 0;
                do
                {
                    double x = r[i][j] * d;
                    if (x1 + x > x1)
                    {
                        double y = F(x1 + x);
                        ++IterationCount;
                        if (double.IsFinite(y))
                            fp = y;
                    }
                    if (x2 - x < x2)
                    {
                        double y = F(x2 - x);
                        ++IterationCount;
                        if (double.IsFinite(y))
                            fm = y;
                    }
                    q = w[i][j] * (fp + fm);
                    p += q;
                    ++j;
                } while (Math.Abs(q) > eps * Math.Abs(p) && j < m[i]);
                v = 2.0 * s;
                s += p;
                ++i;
            } while (Math.Abs(v - s) > tol * Math.Abs(s) && i < n);
            err = Math.Abs(v - s) / (Math.Abs(s) + eps);
            if (err > Math.Sqrt(eps))
                return double.NaN;

            return d * s * Math.Pow(2, 1 - i);
        }

        // Precomputes abscissas and weights for the Tanh-Sinh quadrature
        private static void GetTanhSinhAbscissasAndWieghts()
        {
            double t, h = 2.0;
            for (int i = 0; i < n; ++i)
            {
                h /= 2.0;
                double eh = Math.Exp(h);
                t = Math.Exp(h);
                r[i] = new double[m[i]];
                w[i] = new double[m[i]];
                if (i > 0)
                    eh *= eh;

                for (int j = 0; j < m[i]; ++j)
                {
                    double t1 = 1.0 / t;
                    double u = Math.Exp(t1 - t);
                    double u1 = 1.0 / (1.0 + u);
                    double d = 2.0 * u * u1;
                    if (d == 0)
                        break;
                    r[i][j] = d;
                    w[i][j] = (t1 + t) * d * u1;
                    t *= eh;
                }
            }
        }
    }
}
