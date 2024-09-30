module GradeSchool

type School = Map<int, string list>

let empty: School = Map.empty

let grade (number: int) (school: School): string list =
    school |> Map.tryFind number |> Option.defaultValue [] |> List.sort

let add (student: string) (number: int) (school: School): School =
    let foundStudent =
        school
        |> Map.values
        |> Seq.collect(fun x -> x)
        |> Seq.contains student
    if (foundStudent) then
        school
    else
        school |> Map.add number (student::(grade number school))

let roster (school: School): string list =
    school
    |> Map.values
    |> Seq.map (fun li -> li |> List.sort)
    |> Seq.collect (fun x -> x)
    |> Seq.toList