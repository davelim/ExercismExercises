// type definitions
type EmailAddress = EmailAddress of string
// create w/ option type
let CreateEmailAddress (s:string) =
    if System.Text.RegularExpressions.Regex.IsMatch(s, @"^\S+@\S+\.\S+$")
        then s |> EmailAddress |> Some
        else None
// create w/ union type
type CreationResult<'T> =
    | Success of 'T
    | Error of string
let CreateEmailAddress2 (s:string) =
    if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$")
        then Success (EmailAddress s)
        else Error "Email address must contain an @ sign"
// create w/ continuation
let CreateEmailAddressWithContinuations success failure (s:string) =
    if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$")
        then s |> EmailAddress |> success
        else "Email address must contain an @ sign" |> failure

// use cases:
// use (default) constructor as a function
"a@foo.com" |> EmailAddress // returns "EmailAddress "a@foo.com"
["a@foo.com"; "b@foo.com"; "c@foo.com"] |> List.map EmailAddress
let addresses =
    ["a@foo.com"; "b@foo.com"; "c@foo.com"]
    |> List.map EmailAddress

// use inline deconstruction
let a' = "a@foo.com" |> EmailAddress
let (EmailAddress a'') = a' // a'' is assigned to "a@foo.com" string
let addresses' =
    addresses
    |> List.map (fun (EmailAddress e) -> e) // addresses' is a string list

// use create w/ continuation
let success (EmailAddress s) = printfn "success creating email %s" s
let failure msg = printfn "error creating email: %s" msg
CreateEmailAddressWithContinuations success failure "foo.com"
CreateEmailAddressWithContinuations success failure "a@foo.com"