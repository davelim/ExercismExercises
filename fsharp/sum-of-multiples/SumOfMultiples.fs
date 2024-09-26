module SumOfMultiples

let addItem upperBound acc itemBase =
    match itemBase with
    | 0 -> acc
    | _ -> acc + Set(seq{itemBase..itemBase..upperBound})

let sumSet set =
    (0, set) ||> Set.fold(fun acc n -> acc + n)

let sum (numbers: int list) (upperBound: int): int =
    let addItemBakedUpperBound = addItem (upperBound - 1)
    (Set.empty, numbers) ||> List.fold addItemBakedUpperBound |> sumSet