function mod_ab_CS(f, left::Real, right::Real, target::Real=0.0; precision::Float64=1e-14)
    x1, x2 = min(left, right), max(left, right)
    y1 = f(x1) - target
    abs(y1) <= precision && return x1
    y2 = f(x2) - target
    abs(y2) <= precision && return x2
    eps1 = precision / 100
    eps = precision * (x2 - x1) / 2.0
    if abs(target) > 1
        eps1 *= target
    else
        eps1 = 0
    end
    side = 0
    ans = x1
    bisection = true
    threshold = 0.0  # Threshold to fall back to bisection if AB fails to shrink the interval enough
    C = 16 # safetly factor for threshold corresponding to 4 iterations = 2^4
    for i in 1:200
        local x3, y3
        if bisection
            x3 = (x1 + x2) / 2.0
            y3 = f(x3) - target
            ym = (y1 + y2) / 2.0
            # calculate k on each bisection step with account for local function properties and symmetry
            y1a = abs(y1)
            y2a = abs(y2)
            r = min(y1a, y2a) / max(y1a, y2a)
            k = r^0.25 # Factor for limiting deviation from straight line
            if abs(ym - y3) < k * (abs(y3) + abs(ym))
                bisection = false
                threshold = (x2 - x1) * C
            end
        else
            x3 = (x1 * y2 - y1 * x2) / (y2 - y1)
            if x3 < x1 - eps || x3 > x2 + eps
                return NaN
            end
            y3 = f(x3) - target
            threshold /= 2
        end

        if abs(y3) <= eps1 || abs(x3 - ans) <= eps
            return x3
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
            x1, y1 = x3, y3
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
            x2, y2 = x3, y3
        end
        if x2 - x1 > threshold # AB failed to shrink the interval enough
            bisection = true
        end
    end
    return NaN
end