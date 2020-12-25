module Day25

open System.IO

let transform x subjectNumber = (x * subjectNumber) % 20201227L

let findLoopSize publicKey =
    let mutable loops = 0
    let mutable result = 1L

    while result <> publicKey do
        result <- transform result 7L
        loops <- loops + 1

    loops

let findEncryptionKey publicKey loopSize =
    let mutable key = 1L

    for _ in 1 .. loopSize do
        key <- transform key publicKey

    key

[<EntryPoint>]
let main argv =
    let pk1, pk2 = 
        File.ReadLines("Input.txt") 
        |> Seq.map int64
        |> Seq.toList
        |> fun s -> s.[0], s.[1]

    let ls1, ls2 = findLoopSize pk1, findLoopSize pk2
    let ek1, ek2 = findEncryptionKey pk1 ls2, findEncryptionKey pk2 ls1
    printfn "EncryptionKey1: %d, EncryptionKey2: %d" ek1 ek2
    0
