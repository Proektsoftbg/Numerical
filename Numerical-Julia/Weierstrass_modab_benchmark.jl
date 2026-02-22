using NonlinearSolve
include("ModAB_CS.jl")

# Function-call counting wrapper
mutable struct CountedFunc{F} <: Function
    f::F
    count::Int
end
CountedFunc(f) = CountedFunc(f, 0)
(cf::CountedFunc)(x) = (cf.count += 1; cf.f(x))
reset!(cf::CountedFunc) = (cf.count = 0; cf)

# NonlinearSolve.jl solver wrapper
function make_nlsolve_solver(method, name::String)
    function solver(f, left::Real, right::Real, target::Real=0.0; precision::Float64=1e-14)
        g = target != 0 ? x -> f(x) - target : f
        a, b = min(left, right), max(left, right)
        try
            prob = IntervalNonlinearProblem((x, p) -> g(x), (a, b))
            sol = solve(prob, method; abstol=precision, maxiters=200)
            return sol.u
        catch
            return NaN
        end
    end
    return solver
end

bisect_solver  = make_nlsolve_solver(Bisection(),  "bisect")
brent_solver   = make_nlsolve_solver(Brent(),      "brent")
ridder_solver  = make_nlsolve_solver(Ridder(),     "ridder")
a42_solver     = make_nlsolve_solver(Alefeld(),    "A42")
itp_solver     = make_nlsolve_solver(ITP(),        "ITP")
modab_solver   = make_nlsolve_solver(ModAB(),      "modab")

# Problem definition
struct Problem
    name::String
    f::Function
    a::Float64
    b::Float64
    value::Float64
end
Problem(name, f, a, b) = Problem(name, f, Float64(a), Float64(b), 0.0)

P(x) = x + 1.11111

# Wierstrass function
function weierstrass(x; a::Float64=0.5, b::Int=13, n_terms::Int=100)
    # Validate parameters
    @assert 0 < a < 1 "Parameter a must satisfy 0 < a < 1"
    @assert b > 0 && isodd(b) "Parameter b must be a positive odd integer"
    @assert a * b > 1 + 3π/2 "Parameters must satisfy ab > 1 + 3π/2 for nowhere-differentiability"
    @assert n_terms > 0 "n_terms must be positive"
    result = sum(a^n * cos(b^n * π * x) for n in 0:n_terms-1)
    return result
end

const problems = [
    Problem("f01", x -> weierstrass(x), 0.25, 0.4),
    Problem("f02", x -> weierstrass(x), 0.62, 0.8),
    Problem("f03", x -> weierstrass(x), 0.24, 0.36),
]

# Solver tabl
const solvers = [
    ("bisect", bisect_solver),
    (" brent", brent_solver),
    ("ridder", ridder_solver),
    ("   A42", a42_solver),
    ("   ITP", itp_solver),
    (" modAB", modab_solver),
    (" modAB_CS", mod_ab_CS)]   

# Benchmark runner
function run_benchmark()
    eps = 1e-14
    col_w = 22

    #Results
    println("Results")
    header = lpad("Func", 4) * "; " * join([lpad(name, col_w) for (name, _) in solvers], "; ")
    println(header)
    for p in problems
        line = lpad(p.name, 4) * "; "
        for (name, solver) in solvers
            cf = CountedFunc(p.f, 0)
            try
                result = solver(cf, p.a, p.b, p.value; precision=eps)
                s = if isnan(result)
                    lpad("NaN", col_w)
                else
                    lpad(string(result), col_w)
                end
                line *= s * "; "
            catch e
                line *= lpad("ERR", col_w) * "; "
            end
        end
        println(line)
    end
    println()

    # Function evaluation counts
    println("Function evaluations")
    header = lpad("Func", 4) * "; " * join([lpad(name, 6) for (name, _) in solvers], "; ")
    println(header)
    total = zeros(Int, length(solvers))
    for p in problems
        line = lpad(p.name, 4) * "; "
        for (j, (name, solver)) in enumerate(solvers)
            cf = CountedFunc(p.f)
            try
                solver(cf, p.a, p.b, p.value; precision=eps)
                total[j] += cf.count
                line *= lpad(string(cf.count), 7) * "; "
            catch e
                line *= lpad("ERR", 6) * "; "
            end
        end
        println(line)
    end

    # Print totals
    line = lpad("SUM", 4) * "; "
    for t in total
        line *= lpad(string(t), 6) * "; "
    end
    println(line)
    println()
end

run_benchmark()