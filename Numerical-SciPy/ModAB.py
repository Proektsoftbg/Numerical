import math

def mod_ab(f, left, right, target, precision=1e-14):
    """
    Finds the root of f(x) = target within [left, right] using
    modified Anderson-Björk method (Ganchovski, Traykov).
    f(x) must be continuous and sign(f(left) - target) ≠ sign(f(right) - target).
    """
    x1, x2 = min(left, right), max(left, right)
    y1 = f(x1) - target
    if abs(y1) <= precision:
        return x1

    y2 = f(x2) - target
    if abs(y2) <= precision:
        return x2

    eps1 = precision / 100
    eps = precision * (x2 - x1) / 2.0
    if abs(target) > 1:
        eps1 *= target
    else:
        eps1 = 0
        
    side = 0
    x0 = x1
    bisection = True
    threshold = 0.0  # Threshold to fall back to bisection if AB fails to shrink the interval enough
    C = 16 # safetly factor for threshold corresponding to 4 iterations = 2^4
    n = 100

    for i in range(1, n + 1):
        if bisection:
            x3 = (x1 + x2) / 2.0
            y3 = f(x3) - target
            ym = (y1 + y2) / 2.0
            # calculate k on each bisection step with account for local function properties and symmetry
            y1a = abs(y1)
            y2a = abs(y2)
            r = min(y1a, y2a) / max(y1a, y2a) if y1a > 0 and y2a > 0 else 1
            k = r ** 0.25 # Factor for limiting deviation from straight line
            if abs(ym - y3) < k * (abs(y3) + abs(ym)):
                bisection = False
                threshold = (x2 - x1) * C
        else:
            x3 = (x1 * y2 - y1 * x2) / (y2 - y1)
            if x3 < x1 - eps or x3 > x2 + eps:
                return float('nan')
            
            y3 = f(x3) - target
            threshold /= 2

        if abs(y3) <= eps1 or abs(x3 - x0) <= eps:
            return x3

        x0 = x3
        if math.copysign(1, y1) == math.copysign(1, y3):
            if side == 1:
                m = 1 - y3 / y1
                if m <= 0:
                    y2 /= 2
                else:
                    y2 *= m
            elif not bisection:
                side = 1
            x1, y1 = x3, y3
        else:
            if side == -1:
                m = 1 - y3 / y2
                if m <= 0:
                    y1 /= 2
                else:
                    y1 *= m
            elif not bisection:
                side = -1
            x2, y2 = x3, y3

        if x2 - x1 > threshold: # AB failed to shrink the interval enough
            bisection = True

    return float('nan')