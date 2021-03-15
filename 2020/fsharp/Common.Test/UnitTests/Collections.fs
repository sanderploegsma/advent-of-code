namespace UnitTests

module Collections =

    open Common.Collections
    open Xunit
    open FsUnit.Xunit

    [<Fact>]
    let ``combinations should not allow negative n`` () =
        shouldFail (fun () -> combinations -1 [1;2;3] |> ignore)
    
    [<Fact>]
    let ``combinations should not allow n greater than list size`` () =
        shouldFail (fun () -> combinations 4 [1;2;3] |> ignore)

    [<Theory>]
    [<InlineData(1, 5)>]
    [<InlineData(2, 10)>]
    [<InlineData(3, 10)>]
    [<InlineData(4, 5)>]
    [<InlineData(5, 1)>]
    let ``combinations should create x lists of size n`` n x =
        let actual = combinations n [1;2;3;4;5]
    
        actual |> should haveLength x
        for l in actual do
            l |> should haveLength n
        
    [<Theory>]
    [<InlineData(1)>]
    [<InlineData(2)>]
    [<InlineData(3)>]
    [<InlineData(4)>]
    [<InlineData(5)>]
    let ``combinations should not contain duplicates`` n =
        let actual = combinations n [1;2;3;4;5]
        let distinct = List.distinct actual
    
        actual |> should equal distinct
