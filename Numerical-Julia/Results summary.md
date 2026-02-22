# Count of function evaluations

Func | bisect | brent | ridder | alefeld | ITP  | modAB | modAB_CS
---  | ---:   | ---:  | ---:   | ----:   | ---: | ---:  | ---:
SUM  | 4416   | 5442  | 3776   | 94295   | 2313 | 1710  | 1698
AVE  | 48,5   | 59,8  | 41,5   | 1036,2  | 25,4 | 18,8  | 18,7
REL  | 260%   | 320%  | 222%   | 5553%   | 136% | 101%  | 100%
Func | bisect | brent | ridder | alefeld | ITP  | modAB | modAB_CS

# SciML Performance Benchmarks
Function | Roots.jl | Alefeld | Bisection | Brent | Falsi | ITP | Ridder | ModAB
-- | --: | --: | --: | --: | --: | --: | --: | --:
Wilkinson-like polynomia | 12 | 6,3 | 5,7 | 2,3 | 34,5 | 2,7 | 3,1 | 1,8
sin(x) - 0,5x | 18,5 | 12,5 | 9,2 | 5,6 | 14,6 | 4 | 3,6 | 2,6
exp(x) - 1 - x - x²/2 | 19,7 | 22,9 | 9,2 | 6,2 | 482,3 | 4,2 | 4,7 | 3
1/(x-0,5) - 2 | 12,9 | 3,9 | 5,7 | 2,6 | 73,5 | 3,7 | 2,2 | 1,7
log(x) - x + 2 | 19,1 | 16,5 | 11,2 | 4,7 | 26,2 | 5,1 | 4,6 | 3,8
sin(20x) + 0,1x | 16,4 | 21,1 | 11,7 | 4,9 | 8,5 | 4,5 | 7,8 | 3,9
x³ - 2x² + x | 12,9 | 5,3 | 5,6 | 2,3 | 81,5 | 3,1 | 2,3 | 1,7
x·sin(1/x) - 0,1 | 20 | FAIL | 10,5 | 9,4 | 16,3 | 4,2 | 3,3 | 3,8
Total | 131,5 | 88,5 | 68,8 | 38 | 737,4 | 31,5 | 31,6 | 22,3
REL | 590% | 397% | 309% | 170% | 3307% | 141% | 142% | 100%
