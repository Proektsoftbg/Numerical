namespace Proektsoft.Numerical
{
    public static partial class Solver
    {
        // Finds the root of "F(x) = y0" within the interval [x1. x2]
        // with the specified precision, using the false-position (regula-falsi) method
        // F(x) must be coutinuous and sign(F(x1) - y0) ≠ sign(F(x2) - y0)

        public static double FalsePosition(Func<double, double> F,
            double x1, double x2, double y0 = 0.0, double precision = 1e-14)
        {
            if (!Initialize(x1, x2, F, y0, precision, 
                out Node p1, out Node p2, out Node eps))
                return double.NaN;

            double x0 = p1.X;
            for (int i = 1; i <= MaxIterations; i++)
            {
                Node p3 = new(Node.Sec(p1, p2), F, y0);
                if (Math.Abs(p3.Y) <= eps.Y || Math.Abs(p3.X - x0) <= eps.X)
                {
                    IterationCount = i;
                    return p3.X;
                }

                x0 = p3.X;
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
