let product n =
    let initialValue = 1
    let action productSoFar x = productSoFar * x
    [1..n] |> List.fold action initialValue

//test
product 10

let sumOfOdds n =
    let initialValue = 0
    let action sumSoFar x = if x%2=0 then sumSoFar else sumSoFar+x
    [1..n] |> List.fold action initialValue

//test
sumOfOdds 10

let alternatingSum n =
    let initialValue = (true,0)
    let action (isNeg,sumSoFar) x = if isNeg then (false,sumSoFar-x)
                                             else (true ,sumSoFar+x)
    [1..n] |> List.fold action initialValue |> snd

//test
alternatingSum 100

let sumOfSquaresWithFold n =
    let initialValue = 0
    let action sumSoFar x = sumSoFar + (x*x)
    [1..n] |> List.fold action initialValue

//test
sumOfSquaresWithFold 100

type NameAndSize= {Name:string;Size:int}
let maxNameAndSize list =
    let innerMaxNameAndSize initialValue rest =
        let action maxSoFar x = if maxSoFar.Size < x.Size then x else maxSoFar
        rest |> List.fold action initialValue
    // handle empty lists
    match list with
    | [] ->
        None
    | first::rest ->
        let max = innerMaxNameAndSize first rest
        Some max
// test
let list = [
    {Name="Alice"; Size=10}
    {Name="Bob"; Size=1}
    {Name="Carol"; Size=12}
    {Name="David"; Size=5}
    ]
maxNameAndSize list
maxNameAndSize []

// use List.maxBy()
let maxNameAndSize2 list =
    match list with
    | [] ->
        None
    | _ ->
        let max = list |> List.maxBy (fun item -> item.Size)
        Some max
maxNameAndSize2 list
maxNameAndSize2 []