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

    n_max = -int(math.log2(precision) / 2.0) + 1
    eps1 = precision / 100
    eps = precision * (x2 - x1) / 2.0
    if abs(target) > 1:
        eps1 *= target
    else:
        eps1 = 0
        
    side = 0
    ans = x1
    bisection = True
    k = 0.25
    n = 100

    for i in range(1, n + 1):
        if bisection:
            x3 = (x1 + x2) / 2.0
            y3 = f(x3) - target
            ym = (y1 + y2) / 2.0
            if abs(ym - y3) < k * (abs(y3) + abs(ym)):
                bisection = False
        else:
            x3 = (x1 * y2 - y1 * x2) / (y2 - y1)
            if x3 < x1 - eps or x3 > x2 + eps:
                return float('nan')
            y3 = f(x3) - target

        err = abs(y3)
        if err < eps1 or abs(x3 - ans) < eps:
            if x1 > x2:
                return x2 if side == 1 else x1
            return max(x1, min(x3, x2))

        ans = x3
        if math.copysign(1, y1) == math.copysign(1, y3):
            if side == 1:
                m = 1 - y3 / y1
                if m <= 0:
                    y2 /= 2
                else:
                    y2 *= m
            elif not bisection:
                side = 1
            x1 = x3
            y1 = y3
        else:
            if side == -1:
                m = 1 - y3 / y2
                if m <= 0:
                    y1 /= 2
                else:
                    y1 *= m
            elif not bisection:
                side = -1
            x2 = x3
            y2 = y3

        if i % n_max == 0:
            bisection = True

    return ans