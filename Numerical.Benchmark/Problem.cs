using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical.Benchmark
{
    internal struct Problem
    {
        public string Name;
        public Func<double, double> F;
        public double a;
        public double b;
        public double Value;
    }
}
