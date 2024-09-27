module GradeSchool

type School = Map<int, string list>

let empty: School = Map.empty<int, string list>

let add (student: string) (grade: int) (school: School): School =
    let gradeToAddStudent =
        match school.ContainsKey grade with
        | true -> school[grade]
        | _ -> []

    let foundStudent =
        school.Values
        |> Seq.collect (fun x -> x)
        |> Seq.contains student

    match foundStudent with
    | false -> school.Add (grade, student::gradeToAddStudent)
    | _ -> school

let roster (school: School): string list =
    school
    |> Map.values
    |> Seq.map (fun li -> li |> List.sort)
    |> Seq.collect (fun x -> x)
    |> Seq.toList

let grade (number: int) (school: School): string list =
    match school.ContainsKey number with
    | true -> school[number] |> List.sort
    | _ -> []