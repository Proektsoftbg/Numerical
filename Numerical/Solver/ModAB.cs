namespace Proektsoft.Numerical
{
    public static partial class Solver
    {
        // Finds the root of "F(x) = y0" within the interval [x1. x2]
        // with the specified precision, using modified Anderson Bjork's method (Ganchovski, Traykov)
        // F(x) must be continuous and sign(F(x1) - y0) ≠ sign(F(x2) - y0)

        public static double ModAB(Func<double, double> f, double left, double right, double target, double precision = 1e-14)
        {
            double x1 = Math.Min(left, right), y1 = f(x1) - target;
            if (Math.Abs(y1) <= precision)
                return x1;

            double x2 = Math.Max(left, right), y2 = f(x2) - target;
            if (Math.Abs(y2) <= precision)
                return x2;

            var nMax = -(int)(Math.Log2(precision) / 2.0) + 1;
            double eps1 = precision / 4, eps = precision * (x2 - x1) / 2.0;
            if (Math.Abs(target) > 1)
                eps1 *= target;

            var side = 0;
            var ans = x1;
            var bisection = true;
            const double k = 0.25;
            const int n = 100;
            for (int i = 1; i <= n; ++i)
            {
                double x3, y3;
                if (bisection)
                {
                    x3 = (x1 + x2) / 2.0;
                    y3 = f(x3) - target;
                    var ym = (y1 + y2) / 2.0;
                    // Check if function is close to straight line
                    if (Math.Abs(ym - y3) < k * (Math.Abs(y3) + Math.Abs(ym)))
                        bisection = false;
                }
                else
                {
                    x3 = (x1 * y2 - y1 * x2) / (y2 - y1);
                    if (x3 < x1 - eps || x3 > x2 + eps)
                    {
                        return double.NaN;
                    }
                    y3 = f(x3) - target;
                }
                var err = Math.Abs(y3);
                if (err < eps1 || Math.Abs(x3 - ans) < eps)
                {
                    IterationCount = i;
                    if (x1 > x2)
                        return side == 1 ? x2 : x1;

                    return Math.Clamp(x3, x1, x2);
                }
                ans = x3;
                if (Math.Sign(y1) == Math.Sign(y3))
                {
                    if (side == 1)
                    {
                        var m = 1 - y3 / y1;
                        if (m <= 0)
                            y2 /= 2;
                        else
                            y2 *= m;
                    }
                    else if (!bisection)
                        side = 1;

                    x1 = x3;
                    y1 = y3;
                }
                else
                {
                    if (side == -1)
                    {
                        var m = 1 - y3 / y2;
                        if (m <= 0)
                            y1 /= 2;
                        else
                            y1 *= m;
                    }
                    else if (!bisection)
                        side = -1;

                    x2 = x3;
                    y2 = y3;
                }
                if (i % nMax == 0)
                    bisection = true;
            }
            IterationCount = MaxIterations;
            return ans;
        }
    }
}