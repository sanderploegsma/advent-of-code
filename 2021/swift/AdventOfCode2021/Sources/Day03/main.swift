import Foundation
import Algorithms
import Common

let input = Bundle.module.readLines(fromResource: "Input")

var gammaRate: [Character] = []
var epsilonRate: [Character] = []

for position in 0...11 {
    let zeroes = input.filter { $0[position] == "0" }
    let ones = input.filter { $0[position] == "1" }

    if ones.count > zeroes.count {
        gammaRate.append("1")
        epsilonRate.append("0")
    } else {
        gammaRate.append("0")
        epsilonRate.append("1")
    }
}

let gammaRateValue = Int(String(gammaRate), radix: 2)!
let epsilonRateValue = Int(String(epsilonRate), radix: 2)!

print("Part one: \(gammaRateValue * epsilonRateValue)")

var oxygenGeneratorRating = input
var co2ScrubberRating = input

for position in 0...11 {
    if oxygenGeneratorRating.count > 1 {
        let zeroes = oxygenGeneratorRating.filter { $0[position] == "0" }
        let ones = oxygenGeneratorRating.filter { $0[position] == "1" }
        
        if ones.count >= zeroes.count {
            oxygenGeneratorRating = ones
        } else {
            oxygenGeneratorRating = zeroes
        }
    }

    if co2ScrubberRating.count > 1 {
        let zeroes = co2ScrubberRating.filter { $0[position] == "0" }
        let ones = co2ScrubberRating.filter { $0[position] == "1" }
        
        if zeroes.count <= ones.count {
            co2ScrubberRating = zeroes
        } else {
            co2ScrubberRating = ones
        }
    }
}

let oxygenGeneratorRatingValue = Int(oxygenGeneratorRating.first!, radix: 2)!
let co2ScrubberRatingValue = Int(co2ScrubberRating.first!, radix: 2)!

print("Part two: \(oxygenGeneratorRatingValue * co2ScrubberRatingValue)")

extension StringProtocol {
    subscript(offset: Int) -> Character {
        self[index(startIndex, offsetBy: offset)]
    }
}
