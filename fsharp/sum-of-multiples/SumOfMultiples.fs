module SumOfMultiples

let sum (numbers: int list) (upperBound: int): int =
    let addItem upperBound itemBase =
        match itemBase with
        | 0 -> []
        | _ -> [itemBase..itemBase..upperBound]

    let addItemBakedUpperBound = addItem (upperBound - 1)

    numbers
    |> List.collect addItemBakedUpperBound
    |> List.distinct
    |> List.sum