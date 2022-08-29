namespace Proektsoft.Numerical
{
    internal struct Node
    {
        public Node(double x, double y)
        {
            X = x;
            Y = y;  
        }

        public Node(double x, Func<double, double> F)
        {
            X = x;
            Y = F(x);
        }

        public Node(double x, Func<double, double> F, double y0)
        {
            X = x;
            Y = F(x) - y0;
        }

        public double X;
        public double Y;

        public static double Sec(Node p1, Node p2) =>
            (p1.X * p2.Y - p1.Y * p2.X) / (p2.Y - p1.Y);

        public static double Mid(Node p1, Node p2) =>
            (p1.X + p2.X) / 2.0;
    }
}
