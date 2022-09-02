using Proektsoft.Numerical;

namespace Numerical.Benchmark
{
    internal static class Integration
    {
        private static readonly Problem[] problems =
        {
            new Problem()
            {
                Name = "f1",
                F = (x) => Math.Exp(x),
                Value = Math.E - 1d / Math.E,
                a = -1d,
                b = 1d
            },
            new Problem()
            {
                Name = "f2",
                F = (x) => Math.Cos(x),
                Value = Math.Sin(1d) - Math.Sin(-1d),
                a = -1d,
                b = 1d
            },
            new Problem()
            {
                Name = "f3",
                F = (x) => Math.Tan(x - 1d/2d),
                Value = Math.Log(Math.Abs(Math.Cos(-3d/2d)/Math.Cos(1d/2d))),
                a = -1d,
                b = 1d
            },
            new Problem()
            {
                Name = "f4",
                F = (x) => Math.Sqrt(1d - x*x),
                Value = Math.PI/2d,
                a = -1d,
                b = 1d
            },
            new Problem()
            {
                Name = "f5",
                F = (x) => x < 0.1 ? x/3d : 1d,
                Value = 0.9 - 1.1*0.9/6d,
                a = -1d,
                b = 1d
            },
            new Problem()
            {
                Name = "f6",
                F = (x) => x < 0d ? Math.Sqrt(1d - x*x) : 1d,
                Value = Math.PI/4d + 1d,
                a = -1d,
                b = 1d
            },
            new Problem()
            {
                Name = "f7",
                F = (x) => 1/Math.Sqrt(x + 1d),
                Value = 2 * Math.Sqrt(2d),
                a = -1d,
                b = 1d
            },
            new Problem()
            {
                Name = "f8",
                F = (x) => Math.Tan(Math.PI/4d*(x + 1d)),
                Value = -4d/Math.PI * Math.Log(Math.Cos(Math.PI/2d)),
                a = -1d,
                b = 1d
            }
        };

        internal static void Run()
        {
            const double eps = 1e-14;
            for (int i = 0; i < 3; ++i)
            {
                switch (i)
                {
                    case 0: Console.WriteLine("Result"); break;
                    case 1: Console.WriteLine("Error"); break;
                    case 2: Console.WriteLine("Iteration count"); break;
                }
                Console.WriteLine("Method; Romberg; Simpson; Lobatto; TanhSinh; G7K15; G15K31; G30K61");
                foreach (Problem p in problems)
                {
                    Console.Write(p.Name + "; ");
                    for (int j = 0; j < 7; ++j)
                    {
                        var result = j switch
                        {
                            0 => Integrator.Romberg(p.F, p.a, p.b),
                            1 => Integrator.AdaptiveSimpson(p.F, p.a, p.b, eps),
                            2 => Integrator.AdaptiveLobatto(p.F, p.a, p.b, eps),
                            3 => Integrator.TanhSinh(p.F, p.a, p.b, eps),
                            4 => Integrator.G7K15(p.F, p.a, p.b, eps),
                            5 => Integrator.G15K31(p.F, p.a, p.b, eps),
                            6 => Integrator.G30K61(p.F, p.a, p.b, eps),
                            _ => throw new NotImplementedException()
                        };
                        var error = Math.Abs((result - p.Value) / p.Value);
                        Console.Write((i == 0 ? result : i == 1 ? error : Integrator.IterationCount) + "; ");
                    }
                    Console.Write("\n\r");
                }
                Console.Write("\n\r");
            }
            Console.ReadKey();
        }
    }
}
