namespace Proektsoft.Numerical
{
    public static class Optimizer
    {
        // Finds a local minimum of the functions F(x)
        // within the interval [x1, x2] with the specified precision
        // using the golden section search algorithm

        public static double Minimum(Func<double, double> F, 
            double x1, double x2, double precision = 1e-14) => 
            GoldenSectionSearch(F, x1, x2, precision, false);

        // Finds a local maximum of the functions F(x)
        // within the interval [x1, x2] with the specified precision
        // using the golden section search algorithm

        public static double Maximum(Func<double, double> F, 
            double x1, double x2, double precision = 1e-14) => 
            GoldenSectionSearch(F, x1, x2, precision, true);

        // Common golden section search algorithm for finding an extremum

        private static double GoldenSectionSearch(Func<double, double> F, 
            double x1, double x2, double precision, bool isMinimum)
        {
            const double k = 0.618033988749897;
            var x3 = x2 - k * (x2 - x1);
            var x4 = x1 + k * (x2 - x1);
            var eps = precision * (Math.Abs(x3) + Math.Abs(x4)) / 2;
            var eps2 = precision * precision;
            while (Math.Abs(x2 - x1) > eps)
            {
                if (isMinimum == F(x3) < F(x4))
                {
                    x2 = x4;
                    x4 = x3;
                    x3 = x2 - k * (x2 - x1);
                }
                else
                {
                    x1 = x3;
                    x3 = x4;
                    x4 = x1 + k * (x2 - x1);
                }
                eps = precision * (Math.Abs(x3) + Math.Abs(x4));
                if (eps < eps2)
                    eps = eps2;
            }
            return F((x1 + x2) / 2);
        }
    }
}
