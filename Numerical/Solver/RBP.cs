namespace Proektsoft.Numerical
{
    public static partial class Solver
    {
        // Finds the root of "F(x) = y0" within the interval [x1. x2]
        // with the specified precision, using the RBP method:
        // Alojz Suhadolnik, Combined bracketing methods for solving nonlinear equations,
        // Applied Mathematics Letters, Volume 25, Issue 11, 2012, Pages 1755-1760, ISSN 0893-9659
        // https://doi.org/10.1016/j.aml.2012.02.006
        // F(x) must be continuous and sign(F(x1) - y0) ≠ sign(F(x2) - y0)

        public static double RBP(Func<double, double> F,
            double x1, double x2, double y0 = 0.0, double precision = 1e-14)
        {
            if (!Initialize(x1, x2, F, y0, precision,
                out Node p1, out Node p2, out Node eps))
                return double.NaN;

            Node p3 = new(Node.Mid(p1, p2), F, y0);
            var x0 = p3.X;
            for (int i = 1; i <= MaxIterations; ++i)
            {
                var dx = p2.X - p1.X;
                if ((p3.X.EqualsBinary(p2.X) || p3.X.EqualsBinary(p1.X)))
                {
                    EvaluationCount = 2 * i;
                    return p3.X;
                }
                double a = (p3.Y - p1.Y) / ((p3.X - p1.X) * dx);
                double b = (p2.Y - p3.Y) / ((p2.X - p3.X) * dx);
                double A = b - a;
                double B = a * (p2.X - p3.X) + b * (p3.X - p1.X);
                double C = p3.Y;
                double D = B * B - 4.0 * A * C;
                Node p;
                p.X = p3.X - 2.0 * C / (B + Math.Sign(B) * Math.Sqrt(D));
                p.Y = F(p.X) - y0;
                if (Math.Sign(p1.Y) != Math.Sign(p.Y))
                {
                    p2 = p;
                    if (Math.Sign(p1.Y) == Math.Sign(p3.Y))
                        p1 = p3;
                }
                else
                {
                    p1 = p;
                    if (Math.Sign(p2.Y) == Math.Sign(p3.Y))
                        p2 = p3;
                }
                var dy = Math.Abs(p2.Y - p1.Y); 
                if (dy > 10.0 * dx || dy < 0.1 * dx)
                    p3.X = Node.Mid(p1, p2);
                else
                    p3.X = Node.Sec(p1, p2);

                p3.Y = F(p3.X) - y0;
                if (Math.Abs(p3.Y) < eps.Y || Math.Abs(p3.X - x0) < eps.X)
                {
                    EvaluationCount = 2 * i + 2;
                    return p3.X;
                }
                x0 = p3.X;
            }
            EvaluationCount = 2 * MaxIterations + 2;
            return double.NaN;
        }
    }
}