module Day07

open System.IO

type Node =
    { Name: string
      Weight: int
      Children: string list }

type TreeNode =
    { Name: string
      Weight: int
      Children: TreeNode list }

let parse (line: string): Node =
    let parseLeaf (leaf: string): Node =
        let parts = leaf.Split(" ")
        let weight = parts.[1].Trim('(', ')')
        { Name = parts.[0]
          Weight = int weight
          Children = [] }

    let parts = line.Split(" -> ")
    if Array.length parts < 2 then
        parseLeaf parts.[0]
    else
        let node = parseLeaf parts.[0]
        { node with
              Children = parts.[1].Split(", ") |> Array.toList }

let findRootNode (input: Node list): Node =
    let hasNoParent (item: Node) =
        List.forall (fun (node: Node) -> not (List.contains item.Name node.Children)) input

    List.find hasNoParent input

let rec buildTree (input: Node list) (root: Node): TreeNode =
    let findNodeByName name =
        List.find (fun (node: Node) -> node.Name = name) input

    let children =
        root.Children
        |> List.map findNodeByName
        |> List.map (buildTree input)

    { Name = root.Name
      Weight = root.Weight
      Children = children }

let rec calculateTotalWeight (tree: TreeNode) =
    tree.Weight
    + List.sumBy calculateTotalWeight tree.Children

let rec findUnbalancedNode (tree: TreeNode): int option =
    let children =
        tree.Children
        |> List.map (fun node -> (node, calculateTotalWeight node))

    let totalWeights = children |> List.map (fun (_, w) -> w)

    let (correctTotalWeight, _) =
        totalWeights
        |> List.countBy id
        |> List.filter (fun (_, count) -> count > 1)
        |> List.head

    children
    // Find a child that has a different weight
    |> List.tryFind (fun (_, w) -> w <> correctTotalWeight)
    |> Option.bind (fun (outlier, totalWeight) ->
        // If the outlier has children with different weights, recurse
        findUnbalancedNode outlier
        // Otherwise, this node is wrong
        |> Option.orElse (Some(outlier.Weight + correctTotalWeight - totalWeight)))

[<EntryPoint>]
let main argv =
    let input =
        File.ReadLines("input.txt")
        |> Seq.map parse
        |> Seq.toList

    let rootNode = findRootNode input
    printfn "Part one: %s" rootNode.Name

    let tree = buildTree input rootNode
    findUnbalancedNode tree |> printfn "Part two: %A"
    0
