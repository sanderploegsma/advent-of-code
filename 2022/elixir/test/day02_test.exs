defmodule Day02Test do
  use ExUnit.Case

  @example_input """
  A Y
  B X
  C Z
  """

  test "part one", do: assert(Day02.part_one(@example_input) == 15)
  test "part two", do: assert(Day02.part_two(@example_input) == 12)
end
