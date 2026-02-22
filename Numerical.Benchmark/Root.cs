using Proektsoft.Numerical;

namespace Numerical.Benchmark
{
    internal class Root
    {
        private static double P(double x) => x + 1.11111;

        // Test examples from various publications as specified bellow
        private static readonly Problem[] problems1 =
        {
            //Sérgio Galdino. A family of regula falsi root-finding methods
            new Problem {
                Name = "f01",
                F = (x) => Math.Pow(x, 3) - 1,
                a = 0.5,
                b = 1.5
            },
            new Problem
            {
                Name = "f02",
                F = (x) => Math.Pow(x, 2)*(Math.Pow(x, 2)/3 + Math.Sqrt(2)*Math.Sin(x)) - Math.Sqrt(3)/18,
                a = 0.1,
                b = 1
            },
            new Problem
            {
                Name = "f03",
                F = (x) => 11*Math.Pow(x, 11) - 1,
                a = 0.1,
                b = 1
            },
            new Problem
            {
                Name = "f04",
                F = (x) => Math.Pow(x, 3) + 1,
                a = -1.8,
                b = 0
            },
            new Problem
            {
                Name = "f05",
                F = (x) => Math.Pow(x, 3) - 2*x - 5,
                a = 2,
                b = 3
            },
            new Problem
            {
                Name = "f06",
                F = (x) => 2*x*Math.Exp(-5) + 1 - 2*Math.Exp(-5*x),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f07",
                F = (x) => 2*x*Math.Exp(-10) + 1 - 2*Math.Exp(-10*x),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f08",
                F = (x) => 2*x*Math.Exp(-20) + 1 - 2*Math.Exp(-20*x),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f09",
                F = (x) => (1 + Math.Pow(1 - 5, 2))*Math.Pow(x, 2) - Math.Pow(1 - 5*x, 2),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f10",
                F = (x) => (1 + Math.Pow(1 - 10, 2))*Math.Pow(x, 2) - Math.Pow(1 - 10*x, 2),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f11",
                F = (x) => (1 + Math.Pow(1 - 20, 2))*Math.Pow(x, 2) - Math.Pow(1 - 20*x, 2),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f12",
                F = (x) => Math.Pow(x, 2) - Math.Pow(1 - x, 5),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f13",
                F = (x) => Math.Pow(x, 2) - Math.Pow(1 - x, 10),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f14",
                F = (x) => Math.Pow(x, 2) - Math.Pow(1 - x, 20),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f15",
                F = (x) => (1 + Math.Pow(1 - 5, 4))*x -Math.Pow(1 - 5*x, 4),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f16",
                F = (x) => (1 + Math.Pow(1 - 10, 4))*x - Math.Pow(1 - 10*x, 4),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f17",
                F = (x) => (1 + Math.Pow(1 - 20, 4))*x - Math.Pow(1 - 20*x, 4),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f18", F = (x) => Math.Exp(-5*x)*(x - 1) + Math.Pow(x, 5),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f19",
                F = (x) => Math.Exp(-10*x)*(x - 1) + Math.Pow(x, 10),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f20",
                F = (x) => Math.Exp(-20*x)*(x - 1) + Math.Pow(x, 20),
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f21",
                F = (x) => Math.Pow(x, 2) + Math.Sin(x/5) - 1d/4d,
                a = 0,
                b = 1
                },
            new Problem
            {
                Name = "f22",
                F = (x) => Math.Pow(x, 2) + Math.Sin(x/10) - 1d/4d,
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f23",
                F = (x) => Math.Pow(x, 2) + Math.Sin(x/20) - 1d/4d,
                a = 0,
                b = 1
            },
            new Problem
            {
                Name = "f24",
                F = (x) => (x + 2)*(x + 1)*Math.Pow(x - 3, 3),
                a = 2.6,
                b = 4.6
            },
            new Problem
            {
                Name = "f25",
                F = (x) => Math.Pow(x - 4, 5) * Math.Log(x),
                a = 3.6,
                b = 5.6
            },
            new Problem
            {
                Name = "f26",
                F = (x) => Math.Pow(Math.Sin(x) - x/4, 3),
                a = 2,
                b = 4
            },
            new Problem
            {
                Name = "f27",
                F = (x) => (81 - P(x)*(108 - P(x)*(54 - P(x)*(12 - P(x)))))*Math.Sign(P(x) - 3),
                a = 1,
                b = 3
            },
            new Problem
            {
                Name = "f28",
                F = (x) => Math.Sin(Math.Pow(x - 7.143, 3)),
                a = 7,
                b = 8
            },
            new Problem
            {
                Name = "f29",
                F = (x) => Math.Exp(Math.Pow(x - 3, 5)) - 1,
                a = 2.6,
                b = 4.6
            },
            new Problem
            {
                Name = "f30",
                F = (x) => Math.Exp(Math.Pow(x - 3, 5)) - Math.Exp(x - 1),
                a = 4,
                b = 5
            },
            //My functions
            new Problem
            {
                Name = "f31",
                F = (x) => Math.PI - 1/x,
                a = 0.05,
                b = 5
            },
            new Problem
            {
                Name = "f32",
                F = (x) => 4 - Math.Tan(x),
                a = 0,
                b = 1.5
            },
            //Steven A. Stage. Comments on An Improvement to the Brent’s Method 
            new Problem
            {
                Name = "f33",
                F = (x) => Math.Cos(x) - Math.Pow(x, 3),
                a = 0,
                b = 4
            },
            new Problem
            {
                Name = "f34",
                F = (x) => Math.Cos(x) - x,
                a = -11,
                b = 9
            },
            new Problem
            {
                Name = "f35",
                F = (x) => Math.Sqrt(Math.Abs(x - 2d/3d))*(x <= 2d/3d ? 1 : -1) - 0.1,
                a = -11,
                b = 9
            },
            new Problem
            {
                Name = "f36",
                F = (x) => Math.Pow(Math.Abs(x - 2d/3d), 0.2)*(x <= 2d/3d ? 1 : -1),
                a = -11,
                b = 9
            },
            new Problem
            {
                Name = "f37",
                F = (x) => Math.Pow(x - 7d/9d, 3) + (x - 7d/9d) * 1e-3,
                a = -11,
                b = 9
            },
            new Problem
            {
                Name = "f38",
                F = (x) => x <= 1d/3d ? -0.5 : 0.5,
                a = -11,
                b = 9
            },
            new Problem
            {
                Name = "f39",
                F = (x) => x <= 1d/3d ? -1e-3 : 1 - 1e-3,
                a = -11,
                b = 9
            },
            new Problem
            {
                Name = "f40",
                F = (x) => x == 0 ? 0 : 1 / (x - 2d/3d),
                a = -11,
                b = 9
            },
            //A. Swift and G.R. Lindfield. Comparison of a Continuation Method with Brents Method for the Numerical Solution of a Single Nonlinear Equation
            new Problem
            {
                Name = "f41",
                F = (x) => 2*x*Math.Exp(-5) - 2*Math.Exp(-5*x) + 1,
                a = 0,
                b = 10
            },
            new Problem
            {
                Name = "f42",
                F = (x) => (Math.Pow(x, 2) - x - 6)*(Math.Pow(x, 2) - 3*x + 2),
                a = 0,
                b = Math.PI
            },
            new Problem
            {
                Name = "f43",
                F = (x) => Math.Pow(x, 3),
                a = -1,
                b = 1.5
            },
            new Problem
            {
                Name = "f44",
                F = (x) => Math.Pow(x, 5),
                a = -1,
                b = 1.5
            },
            new Problem
            {
                Name = "f45",
                F = (x) => Math.Pow(x, 7),
                a = -1,
                b = 1.5
            },
            new Problem
            {
                Name = "f46",
                F = (x) => (Math.Exp(-5*x) - x - 0.5)/Math.Pow(x, 5),
                a = 0.09,
                b = 0.7
            },
            new Problem
            {
                Name = "f47",
                F = (x) => 1/Math.Sqrt(x) - 2*Math.Log(5e3*Math.Sqrt(x)) + 0.8,
                a = 0.0005,
                b = 0.5
            },
            new Problem
            {
                Name = "f48",
                F = (x) => 1/Math.Sqrt(x) - 2*Math.Log(5e7*Math.Sqrt(x)) + 0.8,
                a = 0.0005,
                b = 0.5
            },
            new Problem
            {
                Name = "f49",
                F = (x) => x <= 0 ? -Math.Pow(x, 3) - x - 1 : Math.Pow(x, 1/3) - x - 1,
                a = -1,
                b = 1
            },
            new Problem
            {
                Name = "f50",
                F = (x) => Math.Pow(x, 3) - 2*x - x + 3,
                a = -3,
                b = 2
            },
            new Problem
            {
                Name = "f51",
                F = (x) => Math.Log(x),
                a = 0.5,
                b = 5
            },
            new Problem
            {
                Name = "f52",
                F = (x) => (10 - x)*Math.Exp(-10*x) - Math.Pow(x, 10) + 1,
                a = 0.5,
                b = 8
            },
            new Problem
            {
                Name = "f53",
                F = (x) => Math.Exp(Math.Sin(x)) - x - 1,
                a = 1.0,
                b = 4
            },
            new Problem
            {
                Name = "f54",
                F = (x) => 2*Math.Sin(x) - 1,
                a = 0.1,
                b = Math.PI/3
            },
            new Problem
            {
                Name = "f55",
                F = (x) => (x - 1)*Math.Exp(-x),
                a = 0.0,
                b = 1.5
            },
            new Problem
            {
                Name = "f56",
                F = (x) => Math.Pow(x - 1, 3) - 1,
                a = 1.5,
                b = 3
            },
            new Problem
            {
                Name = "f57",
                F = (x) => Math.Exp(Math.Pow(x, 2) + 7*x - 30) - 1,
                a = 2.6,
                b = 3.5
            },
            new Problem
            {
                Name = "f58",
                F = (x) => Math.Atan(x) - 1,
                a = 1.0,
                b = 8
            },
            new Problem
            {
                Name = "f59",
                F = (x) => Math.Exp(x) - 2*x - 1,
                a = 0.2,
                b = 3
            },
            new Problem
            {
                Name = "f60",
                F = (x) => Math.Exp(-x) - x - Math.Sin(x),
                a = 0.0,
                b = 2
            },
            new Problem
            {
                Name = "f61",
                F = (x) => Math.Pow(x, 2) - Math.Pow(Math.Sin(x),2)  - 1,
                a = -1,
                b = 2
            },
            new Problem
            {
                Name = "f62",
                F = (x) => Math.Sin(x) - x/2,
                a = Math.PI/2,
                b = Math.PI
            }
        };

        // Test examples from the publication
        // Oliveira I. F. D., Takahashi R. H. C.
        // An Enhancement of the Bisection Method Average Performance Preserving Minmax Optimality

        private static readonly Problem[] problems2 =
        {
            new Problem //Lambert
            {
                Name = "f63",
                F = (x) => x * Math.Exp(x) - 1d,
                a = -1d,
                b = 1d
            },
            new Problem //Trigonometric 1
            {
                Name = "f64",
                F = (x) => Math.Tan(x - 1d/10d),
                a = -1d,
                b = 1d
            },
            new Problem //Trigonometric 2
            {
                Name = "f65",
                F = (x) => Math.Sin(x) + 0.5,
                a = -1d,
                b = 1d
            },
            new Problem //Polynomial 1
            {
                Name = "f66",
                F = (x) => 4 * Math.Pow(x, 5d) + x * x + 1d,
                a = -1d,
                b = 1d
            },
            new Problem //Polynomial 2
            {
                Name = "f67",
                F = (x) => x + Math.Pow(x, 10d) - 1d,
                a = -1d,
                b = 1d
            },
            new Problem //Exponential
            {
                Name = "f68",
                F = (x) => Math.Pow(Math.PI, x) - Math.E,
                a = -1d,
                b = 1d
            },
            new Problem //Logarithmic
            {
                Name = "f69",
                F = (x) => Math.Log(Math.Abs(x - 10d/9d)),
                a = -1d,
                b = 1d
            },
            new Problem //Posynomial
            {
                Name = "f70",
                F = (x) => 1d/3d + Math.Sign(x) * Math.Cbrt(Math.Abs(x)) + Math.Pow(x, 3d),
                a = -1d,
                b = 1d
            },
            new Problem //Poly.Frac.
            {
                Name = "f71",
                F = (x) => (x + 2d/3d)/(x + 101d/100d),
                a = -1d,
                b = 1d
            },
            new Problem //Polynomial 3
            {
                Name = "f72",
                F = (x) => Math.Pow(x * 1e6 - 1d, 3d),
                a = -1d,
                b = 1d
            },
            new Problem //Exp. Poly.
            {
                Name = "f73",
                F = (x) => Math.Exp(x) * Math.Pow(x * 1e6 - 1d, 3d),
                a = -1d,
                b = 1d
            },
            new Problem //Tan. Poly.
            {
                Name = "f74",
                F = (x) => Math.Pow(x - 1d/3d, 2d) * Math.Atan(x - 1d/3d),
                a = -1d,
                b = 1d
            },
            new Problem //Circles
            {
                Name = "f75",
                F = (x) => Math.Sign(3d*x - 1d) * (1d - Math.Sqrt(1d - Math.Pow(3d*x - 1d, 2d)/81d)),
                a = -1d,
                b = 1d
            },
            new Problem //Step Function
            {
                Name = "f76",
                F = (x)  => x > (1d - 1e6) / 1e6 ? (1d + 1e6) / 1e6 : 0d - 1d,
                a = -1d,
                b = 1d
            },
            new Problem //Geometric
            {
                Name = "f77",
                F = (x) => x != 1d/21d ? 1/(21d*x - 1d) : 0d,
                a = -1d,
                b = 1d
            },
            new Problem //Trunc.Poly.
            {
                Name = "f78",
                F = (x) => x * x / 4d + Math.Ceiling(x/2d) - 0.5,
                a = -1d,
                b = 1d
            },
            new Problem //Staircase
            {
                Name = "f79",
                F = (x) => Math.Ceiling(10d*x - 1d) + 0.5,
                a = -1d,
                b = 1d
            },
            new Problem //Noisy Line
            {
                Name = "f80",
                F = (x) => x + Math.Sin(x*1e6)/10d + 1e-3,
                a = -1d,
                b = 1d
            },
            new Problem //Warsaw
            {
                Name = "f81",
                F = (x) => x > -1 ? 1 + Math.Sin(1d/(x + 1d)) : 0d - 1d,
                a = -1d,
                b = 1d
            },
            new Problem //Sawtooth
            {
                Name = "f82",
                F = (x) => 202d*x - 2*Math.Floor((2d*x + 1e-2)/2e-2) - 0.1,
                a = -1d,
                b = 1d
            },
            new Problem //Sawtooth Cube
            {
                Name = "f83",
                F = (x) => Math.Pow(202d*x - 2d*Math.Floor((2d*x + 1e-2)/2e-2) - 0.1, 3d),
                a = -1d,
                b = 1d
            },
        };

        //SciML Benchmarks test suite
        private static readonly Problem[] problems3 =
        {
            new Problem  // Polynomial with multiple roots  
            { 
                 Name = "f84",
                 F = (x) => (x - 1) * (x - 2) * (x - 3) * (x - 4) * (x - 5) - 0.05,
                 a = 0.5,
                 b = 5.5
             },

            new Problem  // Function 2: Trigonometric with multiple roots
            {
                 Name = "f85",
                 F = (x) => Math.Sin(x) - 0.5*x - 0.3,
                 a = -10.0,
                 b = 10.0
             },

            new Problem  // Function 3: Exponential function (sensitive near zero)
            {
                Name = "f86",
                F = (x) =>  Math.Exp(x) - 1 - x - x*x/2 - 0.005,
                a = -2.0,
                b = 2.0
            },

            new Problem  // Function 4: Rational function with pole
            {
                 Name = "f87",
                 F = (x) =>  1/(x - 0.5) - 2 - 0.05,
                 a = 0.6,
                 b = 2.0,
            },

            new Problem  // Function 5: Logarithmic function
            {
                 Name = "f88",
                 F = (x) => Math.Log(x) - x + 2 - 0.05,
                 a = 0.1,
                 b = 3.0,
            },

            new Problem  // Function 6: High oscillation function
            {
                 Name = "f89",
                 F = (x) =>  Math.Sin(20*x) + 0.1*x - 0.1,
                 a = -4.0,
                 b = 5.0,
            },

            new Problem  // Function 7: Function with very flat region
            {
                 Name = "f90",
                 F = (x) =>  x*x*x - 2*x*x + x - 0.025,
                 a = -1.0,
                 b = 2.0,
            },

            new Problem  // Function 8: Bessel-like function
            {
                 Name = "f91",
                 F = (x) =>  x*Math.Sin(1/x) - 0.1 - 0.01,
                 a = 0.01,
                 b = 1.0
             },
        };

        internal static void Run()
        {
            const double eps = 1e-14;
            for (int i = 0; i < 3; ++i)
            {
                switch (i)
                {
                    case 0: Console.WriteLine("Results"); break;
                    case 1: Console.WriteLine("Function values"); break;
                    case 2: Console.WriteLine("Evaluation count"); break;
                }
                Console.WriteLine("Func;   bs;    fp;   mfp;   ill;    ab;   ITP;   rid;    br;   RBP;   mAB");
                var problems = (new[] { problems1, problems2, problems3 }).SelectMany(x => x).ToArray(); ;
                foreach (Problem p in problems)
                {
                    Console.Write(p.Name + "; ");
                    for (int j = 0; j < 10; ++j)
                    {
                        var result = j switch
                        {
                            0 => Solver.Bisection(p.F, p.a, p.b, p.Value, eps),
                            1 => Solver.FalsePosition(p.F, p.a, p.b, p.Value, eps),
                            2 => Solver.ModFalsePosition(p.F, p.a, p.b, p.Value, eps),
                            3 => Solver.Illinois(p.F, p.a, p.b, p.Value, eps),
                            4 => Solver.AndersonBjork(p.F, p.a, p.b, p.Value, eps),
                            5 => Solver.ITP(p.F, p.a, p.b, p.Value, eps),
                            6 => Solver.Ridders(p.F, p.a, p.b, p.Value, eps),
                            7 => Solver.Brent(p.F, p.a, p.b, p.Value, eps),
                            8 => Solver.RBP(p.F, p.a, p.b, p.Value, eps),
                            9 => Solver.ModAB(p.F, p.a, p.b, p.Value, eps),
                           _ => throw new NotImplementedException()
                        };
                        var count = Math.Abs(Solver.EvaluationCount);
                        if (count == 0) count = 1;
                        count = 4 - (int)(Math.Floor(Math.Log10(count)));
                        var s = new string(' ', count) + Solver.EvaluationCount.ToString();
                        if (i == 0)
                            Console.Write(result+ "; ");
                        else if (i == 1)
                            Console.Write(double.IsNaN(result) ? "NaN; " : p.F(result) + "; ");
                        else
                            Console.Write(s + "; ");
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
