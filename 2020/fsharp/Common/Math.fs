namespace Common

module Math =
    open System

    /// Calculate the Greatest Common Divisor between a and b
    let rec gcd a b = if b = 0 then a else gcd b (a % b)

    /// Calculate the Least Common Multiple between a and b
    let lcm a b = abs (a * b) / gcd a b

    /// Convert degrees to radians
    let rad n = Math.PI * n / 180.0

    /// Rotate a point by degrees
    let rotate (x, y) deg =
        let angle = rad deg
        let x' = x * cos angle - y * sin angle
        let y' = y * cos angle + x * sin angle
        (x', y')
