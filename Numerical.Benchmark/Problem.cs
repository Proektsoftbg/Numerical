using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numerical.Benchmark
{
    // This class contains all the required data
    // to define a test problem for the numerical library
    internal struct Problem
    {
        public string Name;
        public Func<double, double> F;
        public double a;
        public double b;
        public double Value;
    }
}
