defmodule Day01Test do
  use ExUnit.Case

  @example_input """
  1000
  2000
  3000

  4000

  5000
  6000

  7000
  8000
  9000

  10000
  """

  test "part one", do: assert(Day01.part_one(@example_input) == 24000)
  test "part two", do: assert(Day01.part_two(@example_input) == 45000)
end
