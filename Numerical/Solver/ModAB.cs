namespace Proektsoft.Numerical
{
    public static partial class Solver
    {
        // Finds the root of "F(x) = y0" within the interval [x1. x2]
        // with the specified precision, using modified Anderson Bjork's method (Ganchovski, Traykov)
        // F(x) must be continuous and sign(F(x1) - y0) ≠ sign(F(x2) - y0)

        public static double ModAB(Func<double, double> F,
            double x1, double x2, double y0 = 0.0, double precision = 1e-14)
        {
            const double k = 0.25;
            if (!Initialize(x1, x2, F, y0, precision,
                out Node p1, out Node p2, out Node eps))
                return double.NaN;

            int N = -(int)(Math.Log2(precision) / 2) + 1;
            int side = 0;
            double x0 = p1.X;
            bool Bisection = true;
            for (int i = 1; i <= MaxIterations; i++)
            {
                Node p3;
                if (Bisection)
                {
                    p3 = new(Node.Mid(p1, p2), F, y0);
                    double ym = (p1.Y + p2.Y) / 2;
                    //Check if function is close to straight line
                    if (Math.Abs(ym - p3.Y) < k * (Math.Abs(p3.Y) + Math.Abs(ym)))
                        Bisection = false;
                }
                else
                    p3 = new(Node.Sec(p1, p2), F, y0);

                if (Math.Abs(p3.Y) <= eps.Y || Math.Abs(p3.X - x0) < eps.X)
                {
                    IterationCount = i;
                    return p3.X;
                }

                x0 = p3.X;
                if (Math.Sign(p1.Y) == Math.Sign(p3.Y))
                {
                    if (side == 1)
                    {
                        double m = 1 - p3.Y / p1.Y;
                        if (m <= 0)
                            p2.Y /= 2;
                        else
                            p2.Y *= m;
                    }
                    if (!Bisection)
                        side = 1;

                    p1 = p3;
                }
                else
                {
                    if (side == -1)
                    {
                        double m = 1 - p3.Y / p2.Y;
                        if (m <= 0)
                            p1.Y /= 2;
                        else
                            p1.Y *= m;
                    }
                    if (!Bisection)
                        side = -1;

                    p2 = p3;
                }
                if (i % N == 0)
                    Bisection = true;
            }
            IterationCount = MaxIterations;
            return double.NaN;
        }


        public static double ModABPaper(Func<double, double> F, double x1, double x2, double y0 = 0.0, double precision = 1e-14)
        {
            double y1 = F(x1), y2 = F(x2);          //Evaluate the function at both ends
            int side = 0;                           //For tracking the side that has moved at the previous iteration
            int N = -(int)(Math.Log2(precision) / 2) + 1; //Expected number of iterations
            double x0 = x1;                         //For storing the abscissa from the previous iteration
            bool Bisection = true;
            double eps = precision;
            for (int i = 1; i <= MaxIterations; ++i)
            {
                double x3, y3;
                if (Bisection)                       //Bisection step
                {
                    x3 = (x1 + x2) / 2; y3 = F(x3);  //Midpoint abscissa and function value
                    double ym = (y1 + y2) / 2;       //Ordinate of chord at midpoint
                    if (Math.Abs(ym - y3) < 0.25 * (Math.Abs(ym) + Math.Abs(y3)))
                        Bisection = false;           //Switch to false-position
                }
                else                                 //False-position step
                {
                    x3 = (x1 * y2 - y1 * x2) / (y2 - y1);
                    y3 = F(x3);
                }
                if (y3 == 0 || Math.Abs(x3 - x0) <= eps)    //Convergence check
                {
                    IterationCount = i;
                    return x3;                  //Return the result
                }

                x0 = x3;                        //Store the abscissa for the next iteration
                if (side == 1)                  //Apply Anderson-Bjork modification for side 1
                {
                    double m = 1 - y3 / y1;
                    if (m <= 0) y2 *= 0.5; else y2 *= m;
                }
                else if (side == 2)             //Apply Anderson-Bjork modification for side 2
                {
                    double m = 1 - y3 / y2;
                    if (m <= 0) y1 /= 2; else y1 *= m;
                }
                if (Math.Sign(y1) == Math.Sign(y3))     //If the left interval does not change sign
                {
                    if (!Bisection) side = 1;       //Store the side that move
                    x1 = x3; y1 = y3;           //Move the left end
                }
                else                    //If the right interval does not change sign
                {
                    if (!Bisection) side = 2;       //Store the side that move
                    x2 = x3; y2 = y3;           //Move the right end
                }
                if (i % N == 0)             //If the expected number of iterations is exceeded
                {
                    Bisection = true;           //Reset to bisection
                    side = 0;
                }
            }
            IterationCount = MaxIterations;
            return double.NaN;            //If the algorithm failed to converge for n iterations
        }
    }
}
