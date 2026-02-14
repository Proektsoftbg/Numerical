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

## Summary of results

### Root Finding

|Method	|bs	  |fp	  |mfp	|ill	|AB	  |ITP	|mAB	|Rid	|Bre |
|-------|-----|-----|-----|-----|-----|-----|-----|-----|----|
|SUM	  |2855	|5631	|1279	|1967	|1980	|1768	|1022	|1348	|1634|
|AVE	  |46	  |91	  |21	  |32	  |32	  |29	  |16	  |22	  |26  |
|MAX	  |48	  |200	|47	  |200	|200	|49	  |47	  |78	  |132 |

Legend:  
bs – Bisection method  
fp – False position  
mfp – Modified false position  
ill – Illinois method  
AB – Anderson-Bjork  
ITP – Interpolate, truncate, project  
mAB – Modified Anderson-Bjork (new)  
Rid – Ridders  
Brе – Brent  

### Numerical Integration

| Method | Exact | Romberg | Simpson | Lobatto | TanhSinh | G7K15 | G15K31 | G30K61 | G49K99 |
|--------|-------|---------|---------|---------|----------|-------|--------|--------|--------|
| f1 | 2,350402 | 2,350402 | 2,350402 | 2,350402 | 2,350402 | 2,350402 | 2,350402 | 2,350402 | 2,350402 |
| f2 | 1,682942 | 1,682942 | 1,682942 | 1,682942 | 1,682942 | 1,682942 | 1,682942 | 1,682942 | 1,682942 |
| f3 | −2,518199 | −2,518199 | −2,518199 | −2,518199 | −2,518199 | −2,518247 | −2,518199 | −2,518199 | −2,518199 |
| f4 | 1,570796 | 1,570796 | 1,570796 | 1,570796 | 1,570796 | 1,570904 | 1,570808 | 1,570798 | 1,570797 |
| f5 | 0,735000 | 0,734942 | 0,735000 | 0,735000 | 0,006026 | 0,729815 | 0,782954 | 0,757116 | 0,724529 |
| f6 | 1,785398 | 1,785398 | 1,785398 | 1,785398 | 1,785398 | 1,785452 | 1,785404 | 1,785399 | 1,785398 |
| f7 | 2,828427 | NaN | NaN | ? | 2,828427 | 2,763828 | 2,797264 | 2,812606 | 2,818677 |
| f8 | 12,034539 | 12,052206 | 12,034539 | 12,034539 | 12,034539 | 8,334189 | 10,050636 | 11,329626 | 11,849609 |
| f9 | 0,000000 | −8,80E-18 | 0,000000 | −1,52E-15 | 0,000000 | 0,000000 | 0,000000 | 0,000000 | 0,000000 |
| f10 | 1,500000 | 1,500000 | 1,500000 | 1,500000 | 1,500000 | 1,501369 | 1,499899 | 1,499973 | 1,500031 |

## Errors

| Method | Exact | Romberg | Simpson | Lobatto | TanhSinh | G7K15 | G15K31 | G30K61 | G49K99 |
|--------|-------|---------|---------|---------|----------|-------|--------|--------|--------|
| f1 | 0,00E+00 | 1,51E-15 | 0,00E+00 | 0,00E+00 | 1,13E-15 | 1,89E-16 | 0,00E+00 | 0,00E+00 | 3,78E-16 |
| f2 | 0,00E+00 | 9,37E-15 | 1,32E-16 | 1,32E-16 | 1,32E-16 | 1,32E-16 | 1,32E-16 | 0,00E+00 | 2,64E-16 |
| f3 | 0,00E+00 | 2,47E-15 | 1,76E-16 | 3,53E-16 | 8,82E-16 | 1,88E-05 | 7,80E-10 | 0,00E+00 | 0,00E+00 |
| f4 | 0,00E+00 | 1,66E-07 | 0,00E+00 | 3,11E-15 | 2,83E-16 | 6,86E-05 | 7,64E-06 | 9,98E-07 | 2,34E-07 |
| f5 | 0,00E+00 | 7,92E-05 | 1,60E-13 | 2,66E-12 | 9,92E-01 | 7,05E-03 | 6,52E-02 | 3,01E-02 | 1,42E-02 |
| f6 | 0,00E+00 | 7,32E-08 | 0,00E+00 | 1,37E-15 | 1,24E-16 | 3,02E-05 | 3,36E-06 | 4,39E-07 | 1,03E-07 |
| f7 | 0,00E+00 | NaN | NaN | ? | 3,65E-09 | 2,28E-02 | 1,10E-02 | 5,59E-03 | 3,45E-03 |
| f8 | 0,00E+00 | 1,47E-03 | 5,71E-12 | 1,10E-13 | 9,27E-14 | 3,07E-01 | 1,65E-01 | 5,86E-02 | 1,54E-02 |
| f9 | 0,00E+00 | −8,80E-18 | 0,00E+00 | −1,52E-15 | 0,00E+00 | 0,00E+00 | 0,00E+00 | 0,00E+00 | 0,00E+00 |
| f10 | 0,00E+00 | 0,00E+00 | 0,00E+00 | 1,63E-15 | 1,92E-08 | 9,13E-04 | 6,72E-05 | 1,78E-05 | 2,10E-05 |

## Number of function evaluations

| Method | Exact | Romberg | Simpson | Lobatto | TanhSinh | G7K15 | G15K31 | G30K61 | G49K99 |
|--------|-------|---------|---------|---------|----------|-------|--------|--------|--------|
| f1 | | 16385 | 49789 | 425 | 117 | 15 | 31 | 61 | 99 |
| f2 | | 16385 | 38249 | 395 | 59 | 15 | 31 | 61 | 99 |
| f3 | | 16385 | 211037 | 2075 | 117 | 15 | 31 | 61 | 99 |
| f4 | | 16385 | 290113 | 2975 | 57 | 15 | 31 | 61 | 99 |
| f5 | | 16385 | 401 | 455 | 73 | 15 | 31 | 61 | 99 |
| f6 | | 16385 | 145489 | 1445 | 59 | 15 | 31 | 61 | 99 |
| f7 | | 16385 | 1821545 | 10085 | 7457 | 15 | 31 | 61 | 99 |
| f8 | | 16385 | 1344784533 | 2285 | 233 | 15 | 31 | 61 | 99 |
| f9 | | 16385 | 1823089 | 11495 | 3 | 15 | 31 | 61 | 99 |
| f10 | | 16385 | 109 | 245 | 7141 | 15 | 31 | 61 | 99 |

> **0** – No convergence &emsp; Precision = 10⁻¹⁴
