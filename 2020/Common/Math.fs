namespace Common

module Math =
    /// Calculate the Greatest Common Divisor between a and b
    let rec gcd a b = if b = 0 then a else gcd b (a % b)

    /// Calculate the Least Common Multiple between a and b
    let lcm a b = abs (a * b) / gcd a b

