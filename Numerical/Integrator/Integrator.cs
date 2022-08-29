namespace Proektsoft.Numerical
{
    //This class provides methods for numerical integration of functions
    public static partial class Integrator
    {
        public static int IterationCount { get; private set; }

        static Integrator()
        {
            GetTanhSinhAbscissasAndWieghts();
        }
    }
}
