namespace Proektsoft.Numerical
{
    public static partial class Solver
    {
        // Finds the root of "F(x) = y0" within the interval [x1. x2]
        // with the specified precision, using modified false-position method (Ganchovski)
        // F(x) must be continuous and sign(F(x1) - y0) ≠ sign(F(x2) - y0)v

        public static double ModFalsePosition(Func<double, double> F,
            double x1, double x2, double y0 = 0.0, double precision = 1e-14)
        {
            const double k = 0.25;
            if (!Initialize(x1, x2, F, y0, precision,
                out Node p1, out Node p2, out Node eps))
                return double.NaN;

            double x0 = p1.X;
            bool Bisection = true;
            for (int i = 1; i <= MaxIterations; i++)
            {
                Node p3;
                if (Bisection)
                {
                    p3 = new(Node.Mid(p1, p2), F, y0);
                    double ym = (p1.Y + p2.Y) / 2;
                    //Check if function is close to straight line
                    if (Math.Abs(ym - p3.Y) < k * (Math.Abs(ym) + Math.Abs(p3.Y)) / 2)
                        Bisection = false;
                }
                else
                    p3 = new(Node.Sec(p1, p2), F, y0);

                if (Math.Abs(p3.Y) <= eps.Y || Math.Abs(p3.X - x0) < eps.X)
                {
                    EvaluationCount = i + 2;
                    return p3.X;
                }

                x0 = p3.X;
                if (Math.Sign(p1.Y) == Math.Sign(p3.Y))
                    p1 = p3;
                else
                    p2 = p3;
            }
            EvaluationCount = MaxIterations + 2;
            return double.NaN;
        }

    }
}
