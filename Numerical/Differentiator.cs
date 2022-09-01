namespace Proektsoft.Numerical
{
    public static class Differentiator
    {
        // Calculates the first derivative of the finction F(x) at point x
        // using Richardson extrapolation on a 2 node symetrical stencil
        public static double FirstDerivative(Func<double, double> F, 
            double x, double Precision = 1e-14) 
        {
            double delta = Math.Min(Math.Sqrt(Precision), 1e-3);
            double maxErr = Math.Max(50 * Precision, 1e-3);
            const int n = 7;
            var a = Math.Abs(x) < 1 ? 1 : x;
            var eps = Math.Cbrt(Math.BitIncrement(a) - a);
            var h = Math.Pow(2, n) * eps;
            var h2 = 2 * h;
            var r = new double[n];
            var err = delta / 2;
            for (int i = 0; i < n; ++i)
            {
                var x1 = x - h;
                var x2 = x1 + h2;
                var d = 1.0;
                var r0 = r[0];
                r[i] = (F(x2) - F(x1)) / h2;
                for (int k = i - 1; k >= 0; --k)
                {
                    d *= 4.0;
                    var k1 = k + 1;
                    r[k] = r[k1] + (r[k1] - r[k]) / (d - 1);
                }
                if (i >= 1)
                {
                    err = Math.Abs(r[0]) <= delta ?
                          Math.Abs(r[0] - r0) :
                          Math.Abs((r[0] - r0) / r[0]);

                    if (err < delta)
                        break;
                }
                h2 = h;
                h = h2 / 2;
            }
            double slope = err > maxErr ? double.NaN : r[0];
            return slope;
        }
    }
}
