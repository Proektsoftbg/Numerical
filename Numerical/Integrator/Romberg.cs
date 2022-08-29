namespace Proektsoft.Numerical
{
    public static partial class Integrator
    {
        // Calculates the definite integral of the function "F"
        // in the interval [x1, x2] numerically, with the specified percision.
        // Implements the Romberg integration rule
        // Romberg, W. (1955), "Vereinfachte numerische Integration",
        // Det Kongelige Norske Videnskabers Selskab Forhandlinger, Trondheim, 28 (7): 30–36
        // https://doi.org/10.1007/BF02559689

        public static double Romberg(Func<double, double> F, double x1, double x2)
        {
            int n = 14, n1 = n - 1, ny = (int)Math.Pow(2, n);
            double h = x2 - x1;
            double[] R = new double[n];
            double[] y = new double[ny + 1];
            double x = x1;
            double h1 = h / ny;
            for (int j = 0; j <= ny; j++)
            {
                y[j] = F(x);
                x += h1;
            }
            //trapezoidal rule
            int k = ny / 2;
            for (int j = 0; j < n; j++)
            {
                R[j] = (y[0] + y[ny]) / 2;
                int i = k;
                while (i < ny)
                {
                    R[j] += y[i];
                    i += k;
                }
                h /= 2;
                R[j] = R[j] * h;
                k /= 2;
            }
            // Richardson extrapolation
            double m = 1;
            for (int j = 1; j < n; j++)
            {
                m *= 4;
                double m1 = m - 1;
                for (int i = n - 1; i >= j; --i)
                    R[i] = (m * R[i] - R[i - 1]) / m1;
            }
            IterationCount = n;
            return R[n1];
        }
    }
}
