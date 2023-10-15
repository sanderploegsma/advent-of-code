defmodule Mix.Tasks.Solve do
  use Mix.Task

  def run([module | _]) do
    apply(String.to_existing_atom("Elixir.#{String.capitalize(module)}"), :part_one, [])
    |> IO.inspect(label: "Part one")

    apply(String.to_existing_atom("Elixir.#{String.capitalize(module)}"), :part_two, [])
    |> IO.inspect(label: "Part two")
  end
end
