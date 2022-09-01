# Numerical

This library provides numerical methods for integration, differentiation, root-finding and extrema (minimum and maximum) of functions, implemented in C#. It includes the following methods:

## Root-finding (bracketing methods)

* Bisection
* False-position (regula-falsi)
* ModFP - modified false-position (Ganchovski, 2022)
* Illinois method
* Anderson-Bjork's method
* ModAB - modified Anderson-Bjork (Traykov & Ganchovski, 2022)
* ITP (Oliveira & Takahashi, 2021)
* Ridders' method
* Brent's method
* RBP (Suhadolnik, 2012)

## Numerical integration

* Romberg's method
* Adaptive Simpson's quadrature
* Adaptive Lobatto quadrature (Gander & Gautschi, 2000)
* Gauss-Kronrod quadrature (G7K15, G15K31, G30K61)
* Tanh-Sinh quadrature (Takashi & Mori, 1974)

Tanh-Sinh quadrature has been additionally improved by Michashki & Mosig (2016) and Van Engelen (2022). Further improvements has been made in the current implementation:

1. Abscissas and weights are precomputed and saved. This accelerates the calculations when the method has to be called multiple times.
2. Additional error check is performed when the integral should evaluate to zero. In this case, it is better to estimate the absolute error instead of the relative one.

## Numerical differentiation

* First derivative by Richardson extrapolation on a 2 node symetrical stencil

## Optimization by golden section search method

* Local minimum of a function
* Local maximum of a function

# Numerical.Benchmark

The Numerical.Benchmark project provides a simple tool to test and compare different numerical methods.

Some results are presented in the [/Numerical.Benchmark/doc](https://github.com/Proektsoftbg/Numerical/blob/main/Numerical.Benchmark/doc) folder.
