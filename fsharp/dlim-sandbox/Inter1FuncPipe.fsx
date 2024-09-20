// val getOddSquares : xs:int list -> int list
let getOddSquares xs =
    xs
    |> List.filter (fun x -> x % 2 <> 0)
    |> List.map (fun x -> x * x)
// val it : unit = ()
printfn "%A" (getOddSquares [1..10])
(*
> dotnet fsi Inter1FuncPipe.fsx
[1; 9; 25; 49; 81]
*)