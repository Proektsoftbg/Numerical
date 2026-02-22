namespace Proektsoft.Numerical
{
    public static partial class Solver
    {
        // Finds the root of "F(x) = y0" within the interval [x1. x2]
        // with the specified precision, using Brent's method:
        // R. P. Brent, An algorithm with guaranteed convergence for finding a zero of a function,
        // The Computer Journal, Volume 14, Issue 4, 1971, Pages 422–425
        // https://doi.org/10.1093/comjnl/14.4.422 
        // F(x) must be continuous and sign(F(x1) - y0) ≠ sign(F(x2) - y0)

        public static double Brent(Func<double, double> F,
            double x1, double x2, double y0 = 0.0, double precision = 1e-14)
        {
            if (!Initialize(x1, x2, F, y0, precision,
                out Node p1, out Node p2, out Node eps))
                return double.NaN;

            eps.X *= 2;
            Node p3 = p2;
            double d = 0.0, e = 0.0, min1, min2;
            for (int i = 1; i <= MaxIterations; i++)
            {
                if (Math.Sign(p2.Y) == Math.Sign(p3.Y))
                {
                    p3 = p1; //Rename a, b, c and adjust bounding interval
                    e = d = p2.X - p1.X;
                }
                if (Math.Abs(p3.Y) < Math.Abs(p2.Y))
                {
                    p1 = p2;
                    p2 = p3;
                    p3 = p1;
                }
                double xm = (p3.X - p2.X) / 2;
                if (Math.Abs(p2.Y) <= eps.Y || Math.Abs(xm) <= eps.X)  //Convergence check.
                {
                    EvaluationCount = i + 2;
                    return p2.X;
                }
                if (Math.Abs(e) >= eps.X && Math.Abs(p1.Y) > Math.Abs(p2.Y))
                {
                    double s = p2.Y / p1.Y; //Attempt inverse quadratic interpolation.
                    double p, q, r;
                    if (p1.X == p3.X)
                    {
                        p = 2.0 * xm * s;
                        q = 1.0 - s;
                    }
                    else
                    {
                        q = p1.Y / p3.Y;
                        r = p2.Y / p3.Y;
                        p = s * (2.0 * xm * q * (q - r) - (p2.X - p1.X) * (r - 1.0));
                        q = (q - 1.0) * (r - 1.0) * (s - 1.0);
                    }
                    if (p > 0.0)
                        q = -q; //Check whether in bounds.

                    p = Math.Abs(p);
                    min1 = 3.0 * xm * q - Math.Abs(eps.X * q);
                    min2 = Math.Abs(e * q);
                    if (2.0 * p < (min1 < min2 ? min1 : min2))
                    {
                        e = d; //Accept interpolation.
                        d = p / q;
                    }
                    else //Interpolation failed, use bisection.
                    {
                        d = xm;
                        e = d;
                    }
                }
                else //Bounds decreasing too slowly, use bisection.
                {
                    d = xm;
                    e = d;
                }
                p1 = p2; //Move last best guess to a.
                if (Math.Abs(d) > eps.X) //Evaluate new trial root.
                    p2.X += d;
                else
                    p2.X += Math.Abs(eps.X) * Math.Sign(xm);

                p2.Y = F(p2.X) - y0;
            }
            EvaluationCount = MaxIterations + 2;
            return double.NaN;
        }
    }
}
