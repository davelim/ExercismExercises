module SumOfMultiples

let addItem upperBound itemBase =
    match itemBase with
    | 0 -> []
    | _ -> [itemBase..itemBase..upperBound]

let sum (numbers: int list) (upperBound: int): int =
    let addItemBakedUpperBound = addItem (upperBound - 1)
    numbers |> List.collect addItemBakedUpperBound |> List.distinct |> List.sum