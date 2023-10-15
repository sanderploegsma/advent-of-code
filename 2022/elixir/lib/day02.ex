defmodule Day02 do
  @input File.read!("../input/day02.txt")
  @scores_part_one Enum.with_index([nil, :BX, :CY, :AZ, :AX, :BY, :CZ, :CX, :AY, :BZ])
  @scores_part_two Enum.with_index([nil, :BX, :CX, :AX, :AY, :BY, :CY, :CZ, :AZ, :BZ])

  def part_one(input \\ @input), do: calculate_score(input, @scores_part_one)

  def part_two(input \\ @input), do: calculate_score(input, @scores_part_two)

  defp calculate_score(input, scores) do
    parse_input(input)
    |> Enum.map(&Keyword.get(scores, String.to_atom(&1)))
    |> Enum.sum()
  end

  defp parse_input(input) do
    input
    |> String.split("\n", trim: true)
    |> Enum.map(&String.replace(&1, " ", ""))
  end
end
