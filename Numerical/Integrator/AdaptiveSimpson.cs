namespace Proektsoft.Numerical
{
    public static partial class Integrator
    {
        // Calculates the definite integral of the function "F"
        // in the interval [x1, x2] numerically, with the specified percision.
        // Implements the Adaptive Simpsons quadrature
        // J. N. Lyness. 1969. Notes on the Adaptive Simpson Quadrature Routine.
        // J. ACM 16, 3 (July 1969), 483–495. 
        // https://doi.org/10.1145/321526.321537

        public static double AdaptiveSimpson(Func<double, double> F, 
            double x1, double x2, double Precision = 1e-14)
        {
            double h = (x2 - x1) / 2.0;
            Node p1 = new(x1, F);
            Node p2 = new(x2, F);
            Node p3 = new(Node.Mid(p1, p2), F);
            IterationCount = 3;
            double eps = Math.Max(Precision, 1e-16) * Math.Abs(h);
            double a0 = h * Simp(p1, p3, p2);
            double area = Simpson(F, p1, p3, p2, a0, eps, 1);
            return area * Math.Sign(h) / 3.0;
        }

        private static double Simpson(Func<double, double> F,
            Node p1, Node p3, Node p2,
            double a0, double eps, int depth)
        {
            const double c = 1.0 / 15.0;
            double h = (p2.X - p1.X) / 4.0;
            Node p4 = new(Node.Mid(p1, p3), F);
            Node p5 = new(Node.Mid(p3, p2), F);
            IterationCount += 2;
            double a1 = h * Simp(p1, p4, p3);
            double a2 = h * Simp(p3, p5, p2);
            double a = a1 + a2;
            double d = a - a0;

            if (depth > 2 && Math.Abs(d) < 45.0 * eps || depth > 40)
                return a + d * c;

            ++depth;
            eps /= 2.0;
            return Simpson(F, p1, p4, p3, a1, eps, depth) +
            Simpson(F, p3, p5, p2, a1, eps, depth);
        }

        private static double Simp(Node a, Node c, Node b) => a.Y + 4.0 * c.Y + b.Y;
    }
}
