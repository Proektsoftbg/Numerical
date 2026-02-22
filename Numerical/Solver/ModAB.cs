namespace Proektsoft.Numerical
{
    public static partial class Solver
    {
        // Finds the root of "F(x) = y0" within the interval [x1. x2]
        // with the specified precision, using modified Anderson Bjork's method (Ganchovski, Traykov)
        // F(x) must be continuous and sign(F(x1) - y0) ≠ sign(F(x2) - y0)
        public static double ModAB(Func<double, double> F, double x1, double x2, double y0, double precision = 1e-14)
        {
            if (!Initialize(x1, x2, F, y0, precision,
            out Node p1, out Node p2, out Node eps))
                return double.NaN;

            var side = 0; // Store the side that moved last: -1 for left, 1 for right, 0 for none
            var x0 = p1.X;
            var bisection = true;
            double threshold = 0.0; // Threshold to reset to bisection
            const double C = 16; // Safety factor of 4 iteration behind the threshold
            for (int i = 1; i <= MaxIterations; ++i)
            {
                Node p3;
                if (bisection)
                {
                    p3 = new Node(Node.Mid(p1, p2), F, y0);
                    var ym = (p1.Y + p2.Y) / 2.0;
                    double y1 = Math.Abs(p1.Y), y2 = Math.Abs(p2.Y);
                    var r = y1 > 0 && y2 > 0 ? Math.Min(y1, y2) / Math.Max(y1, y2) : 1.0;
                    var k = Math.Pow(r, 0.25); // Factor for limiting deviation from straight line
                    // Check if function is close enough to straight line and switch to false-position
                    if (Math.Abs(ym - p3.Y) < k * (Math.Abs(p3.Y) + Math.Abs(ym)))
                    {
                        bisection = false;
                        threshold = (p2.X - p1.X) * C;
                    }
                }
                else
                {
                    var x3 = Node.Sec(p1, p2);
                    // Check if the new point falls outside the interval
                    if (x3 < p1.X - eps.X || x3 > p2.X + eps.X)
                        return double.NaN;

                    p3 = new Node(x3, F, y0);
                    threshold /= 2.0;
                }
                // Check for convergence and return the result
                if (Math.Abs(p3.Y) <= eps.Y || Math.Abs(p3.X - x0) <= eps.X)
                {
                    EvaluationCount = i + 2;
                    return p3.X;
                }
                x0 = p3.X; // Stores the value for the next iteration to check for convergence
                if (Math.Sign(p1.Y) == Math.Sign(p3.Y))
                {
                    if (side == 1) // Apply Anderson-Bjork' correction to the right side
                    {
                        var m = 1 - p3.Y / p1.Y;
                        if (m <= 0)
                            p2.Y /= 2;
                        else
                            p2.Y *= m;
                    }
                    else if (!bisection)
                        side = 1;

                    p1 = p3;
                }
                else
                {
                    if (side == -1) // Apply Anderson-Bjork' correction to the left side
                    {
                        var m = 1 - p3.Y / p2.Y;
                        if (m <= 0)
                            p1.Y /= 2;
                        else
                            p1.Y *= m;
                    }
                    else if (!bisection)
                        side = -1;

                    p2 = p3;
                }
                if (p2.X - p1.X > threshold) // If AB failed to shrink the interval enough reset to bisection
                    bisection = true;
            }
            EvaluationCount = MaxIterations + 2;
            return double.NaN;
        }
    }
}