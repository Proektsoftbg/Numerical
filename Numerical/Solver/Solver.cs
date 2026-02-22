namespace Proektsoft.Numerical
{
    // This class provides methods for solving the equation F(x) = y0 numerically
    // All methods are of bracketing type. They require an initial interval to be specified
    // that contains the root. The function F(x) must be continuous within the interval
    // and sign(F(x1) - y0) ≠ sign(F(x2) - y0)

    public static partial class Solver
    {

        private const int MaxIterations = 200;

        public static int EvaluationCount { get; private set; }


        private static bool Initialize(double x1, double x2, Func<double, double> F,
            double y0, double Precision, out Node p1, out Node p2, out Node eps)
        {
            p1 = new(x1, F, y0);
            p2 = new(x2, F, y0);
            eps = new(
                Precision * (x2 - x1), 
                Math.Abs(y0) > 1.0 ? eps.Y = Precision * y0 : 0.0
                );
            EvaluationCount = 0;
            return Math.Sign(p1.Y) != Math.Sign(p2.Y);
        }
    }
}