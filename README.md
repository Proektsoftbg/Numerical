# Numerical

This library provides numerical methods for integration, differentiation, root-finding and extrema (minimum and maximum) of functions, implemented in C#. It includes the following methods:

## Root-finding (bracketing methods)

* Bisection
* False-position (regula-falsi)
* ModFP - modified false-position (Ganchovski)
* Illinois method
* Anderson-Bjork's method
* ModAB - modified Anderson-Bjork (Traykov & Ganchovski)
* ITP (Oliveira & Takahashi)
* Ridders' method
* Brent's method
* RBP (Suhadolnik)

## Numerical integration

* Romberg's method
* Adaptive Simpson's quadrature
* Adaptive Lobatto quadrature (Gander & Gautschi)
* Gauss-Kronrod quadrature (G7K15, G15K31, G30K61)
* Tanh-Sinh quadrature (Takashi & Mori)

## Numerical differentiation

* First derivative by Richardson extrapolation on a 2 node symetrical stencil

## Extremum by golden section search

* Local minimum of a function
* Local maximum of a function

#bNumerical.Benchmark

The Numerical.Benchmark project provides a simple tool to test and compare different numerical methods.

Some results are presented in the https://github.com/Proektsoftbg/Numerical/blob/main/Numerical.Benchmark/doc/ folder.