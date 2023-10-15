defmodule Day01 do
  @input File.read!("../input/day01.txt")

  def part_one(input \\ @input) do
    parse_input(input)
    |> Enum.max()
  end

  def part_two(input \\ @input) do
    parse_input(input)
    |> Enum.sort(:desc)
    |> Enum.take(3)
    |> Enum.sum()
  end

  defp parse_input(input) do
    input
    |> String.trim()
    |> String.split("\n\n")
    |> Enum.map(fn block ->
      block
      |> String.split("\n")
      |> Enum.map(&String.to_integer/1)
      |> Enum.sum()
    end)
  end
end
