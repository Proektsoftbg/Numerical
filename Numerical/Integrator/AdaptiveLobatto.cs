namespace Proektsoft.Numerical
{
    public static partial class Integrator
    {
        private static readonly double alpha = Math.Sqrt(2.0 / 3.0);
        private static readonly double beta = 1.0 / Math.Sqrt(5.0);

        // Calculates the definite integral of the function "F"
        // in the interval [x1, x2] numerically, with the specified percision.
        // Implements the Adaptive Lobatto quadrature as published by
        // Gander, W., Gautschi, W. Adaptive Quadrature—Revisited.
        // BIT Numerical Mathematics 40, 84–101 (2000). 
        // https://doi.org/10.1023/A:1022318402393

        public static double AdaptiveLobatto(Func<double, double> F,
            double x1, double x2, double Precision = 1e-14)
        {
            var h = x2 - x1;
            double eps = Math.Max(Precision, 1e-14) * Math.Abs(h);
            IterationCount = 0;
            return Lobatto(F, new(x1, F), new(x2, F), eps, 1);
        }

        private static double Lobatto(Func<double, double> F, 
            Node p1, Node p2, double eps, int depth)
        {
            const double k1 = 1.0 / 1470.0;
            const double k2 = 1.0 / 6.0;
            double h = (p2.X - p1.X) / 2.0;
            double ah = alpha * h;
            double bh = beta * h;
            Node p3 = new(Node.Mid(p1, p2), F);
            Node p4 = new(p3.X - ah, F);
            Node p5 = new(p3.X - bh, F);
            Node p6 = new(p3.X + bh, F);
            Node p7 = new(p3.X + ah, F);
            IterationCount += 5;

            double a1 = h * k1 * (77.0 * (p1.Y + p2.Y) + 432.0 * (p4.Y + p7.Y) + 625.0 * (p5.Y + p6.Y) + 672.0 * p3.Y);
            double a2 = h * k2 * (p1.Y + p2.Y + 5.0 * (p5.Y + p6.Y));

            if (depth > 1 && Math.Abs(a1 - a2) < eps || depth > 15)
                return a1;

            depth++;
            return Lobatto(F, p1, p4, eps, depth) +
                   Lobatto(F, p4, p5, eps, depth) +
                   Lobatto(F, p5, p3, eps, depth) +
                   Lobatto(F, p3, p6, eps, depth) +
                   Lobatto(F, p6, p7, eps, depth) +
                   Lobatto(F, p7, p2, eps, depth);
        }
    }
}
