namespace UnitTests

module Navigation =
    open Common.Navigation
    open FsUnit.Xunit
    open Xunit

    [<Fact>]
    let ``Navigate directions`` () =
        let directions = [North; North; East; West; West; South; North; West; West; West; East]
        List.fold move (0, 0) directions |> should equal (-3, 2)

    [<Fact>]
    let ``Navigate rotations`` () =
        let rotations = [(5, Right); (3, Left); (2, Left); (6, Left); (4, Right)]
        let takeSteps (position, direction) (steps, rotation) = 
            let newPosition = List.replicate steps direction |> List.fold move position
            let newDirection = turn direction rotation
            (newPosition, newDirection)

        List.fold takeSteps ((0, 0), North) rotations |> should equal ((-3, 3), West)
