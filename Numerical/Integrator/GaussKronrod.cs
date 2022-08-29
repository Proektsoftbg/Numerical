namespace Proektsoft.Numerical
{
    public static partial class Integrator
    {
        // Calculates the definite integral of the function "F"
        // in the interval [x1, x2] numerically, with the specified percision.
        // Implements the Gauss-Kronrod quadrature rules as follows:
        // G7K15 - 7 Gauss points, 15 Kronrod points
        public static double G7K15(Func<double, double> F, 
            double x1, double x2, double Precision = 1e-14) =>
            GaussKronrod(F, x1, x2, 7, Precision);

        // G15K31 - 5 Gauss points, 31 Kronrod points
        public static double G15K31(Func<double, double> F, 
            double x1, double x2, double Precision = 1e-14) =>
            GaussKronrod(F, x1, x2, 15, Precision);

        // G30K61 - 30 Gauss points, 61 Kronrod points
        public static double G30K61(Func<double, double> F, 
            double x1, double x2, double Precision = 1e-14) =>
            GaussKronrod(F, x1, x2, 30, Precision);

        // General procedure for gaussNodesCount Gauss nodes
        private static double GaussKronrod(Func<double, double> F, 
            double x1, double x2, int gaussNodesCount, double Precision)
        {
            Node[] G, K;
            switch (gaussNodesCount)
            {
                case 7:
                    G = G7;
                    K = K15;
                    break;
                case 15:
                    G = G15;
                    K = K31;
                    break;
                case 30:
                    G = G30;
                    K = K61;
                    break;
                default:
                    ArgumentOutOfRangeException e = new(nameof(gaussNodesCount), "Invalid number of Gauss quadrature nodes.");
                    throw e;
            }
            int k = gaussNodesCount % 2;
            int n = gaussNodesCount + 1;
            k = 1 - k;
            IterationCount = 2 * gaussNodesCount + 1;
            double d = (x2 - x1) / 2;
            double xm = (x1 + x2) / 2;  
            double QG = 0, QK = 0;  
            for (int i = 0, j = 0; i < n; ++i)
            {
                double y = F(xm - K[i].X * d);
                if (K[i].X != 0d)
                    y += F(xm + K[i].X * d);

                QK += y * K[i].Y;
                if (i % 2 == k)
                {
                    QG += y * G[j].Y;
                    ++j;
                }
            }
            double err = Math.Abs(QK - QG);
            double eps = Math.Abs(Precision * QK);
            if (err > eps)
                IterationCount = int.MaxValue;

            return QK;
        }
    }
}
