# frozen_string_literal: true

input = File.read('input/01.txt').lines(chomp: true)

def part_one(line)
  digits = line.tr('^0-9', '')
  return if digits[0].nil?

  (digits[0] + digits[-1]).to_i
end

puts "Part one: #{input.map(&method(:part_one)).sum}"

$digit_values = {
  "1": 1,
  "one": 1,
  "2": 2,
  "two": 2,
  "3": 3,
  "three": 3,
  "4": 4,
  "four": 4,
  "5": 5,
  "five": 5,
  "6": 6,
  "six": 6,
  "7": 7,
  "seven": 7,
  "8": 8,
  "eight": 8,
  "9": 9,
  "nine": 9
}

def part_two(line)
  first_digit = nil
  first_digit_idx = line.length
  last_digit = nil
  last_digit_idx = -1

  $digit_values.each do |key, value|
    digit = key.to_s
    if line.include?(digit) && line.index(digit) < first_digit_idx
      first_digit_idx = line.index(digit)
      first_digit = value
    end
    if line.include?(digit) && line.rindex(digit) > last_digit_idx
      last_digit_idx = line.rindex(digit)
      last_digit = value
    end
  end

  first_digit * 10 + last_digit
end

puts "Part two: #{input.map(&method(:part_two)).sum}"
