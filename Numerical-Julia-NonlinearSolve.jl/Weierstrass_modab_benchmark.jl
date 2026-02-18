using NonlinearSolve

# ---------------------------------------------------------------------------
# Function-call counting wrapper
# ---------------------------------------------------------------------------

mutable struct CountedFunc{F} <: Function
    f::F
    count::Int
end
CountedFunc(f) = CountedFunc(f, 0)
(cf::CountedFunc)(x) = (cf.count += 1; cf.f(x))
reset!(cf::CountedFunc) = (cf.count = 0; cf)

# ---------------------------------------------------------------------------
# ModAB solver (translated from ModAB.cs) — standalone reference implementation
# ---------------------------------------------------------------------------

function mod_ab_CS(f, left::Real, right::Real, target::Real=0.0; precision::Float64=1e-14)
    x1, x2 = min(left, right), max(left, right)
    y1 = f(x1) - target
    abs(y1) <= precision && return x1

    y2 = f(x2) - target
    abs(y2) <= precision && return x2

    n_max = -Int(floor(log2(precision) / 2.0)) + 1
    eps1 = precision / 100
    eps = precision * (x2 - x1) / 2.0
    if abs(target) > 1
        eps1 *= target
    end
        eps1 = 0

    side = 0
    ans = x1
    bisection = true
    k = 0.25

    for i in 1:200
        local x3, y3
        if bisection
            x3 = (x1 + x2) / 2.0
            y3 = f(x3) - target
            ym = (y1 + y2) / 2.0
            if abs(ym - y3) < k * (abs(y3) + abs(ym))
                bisection = false
            end
        else
            x3 = (x1 * y2 - y1 * x2) / (y2 - y1)
            if x3 < x1 - eps || x3 > x2 + eps
                return NaN
            end
            y3 = f(x3) - target
            # uncomment for early backswitching
            x = sign(y1) == sign(y3) ? x1 : x2
            if 2abs(x3 - x) < x2 - x1
                bisection = true
            end

        end

        if abs(y3) <= eps1 || abs(x3 - ans) <= eps
            if x1 > x2
                return side == 1 ? x2 : x1
            end
            return clamp(x3, x1, x2)
        end

        ans = x3
        if sign(y1) == sign(y3)
            if side == 1
                m = 1 - y3 / y1
                if m <= 0
                    y2 /= 2
                else
                    y2 *= m
                end
            elseif !bisection
                side = 1
            end
            x1 = x3
            y1 = y3
        else
            if side == -1
                m = 1 - y3 / y2
                if m <= 0
                    y1 /= 2
                else
                    y1 *= m
                end
            elseif !bisection
                side = -1
            end
            x2 = x3
            y2 = y3
        end
        if i % n_max == 0
            bisection = true
        end
    end
    return ans
end

# ---------------------------------------------------------------------------
# NonlinearSolve.jl solver wrapper
# ---------------------------------------------------------------------------

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

# ---------------------------------------------------------------------------
# Problem definition
# ---------------------------------------------------------------------------

struct Problem
    name::String
    f::Function
    a::Float64
    b::Float64
    value::Float64
end
Problem(name, f, a, b) = Problem(name, f, Float64(a), Float64(b), 0.0)

P(x) = x + 1.11111

# ---------------------------------------------------------------------------
# Test problems
# ---------------------------------------------------------------------------

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

# ---------------------------------------------------------------------------
# Solver tabl
# ---------------------------------------------------------------------------

const solvers = [
    ("bisect", bisect_solver),
    (" brent", brent_solver),
    ("ridder", ridder_solver),
    ("   A42", a42_solver),
    ("   ITP", itp_solver),
    (" modAB", modab_solver),
    (" modAB_CS", mod_ab_CS)]   

# ---------------------------------------------------------------------------
# Benchmark runner
# ---------------------------------------------------------------------------

function run_benchmark()
    eps = 1e-14
    col_w = 22

    # --- Results ---
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

    # --- Function evaluation counts ---
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
                line *= lpad(string(cf.count), 6) * "; "
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