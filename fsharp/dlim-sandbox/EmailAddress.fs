// type definitions
module EmailAddress
    type T = EmailAddress of string

    // wrap
    // create w/ option type
    let create (s:string) =
        if System.Text.RegularExpressions.Regex.IsMatch(s, @"^\S+@\S+\.\S+$")
            // then s |> EmailAddress |> Some
            then Some (EmailAddress s)
            else None

    // create w/ union type
    type CreationResult<'T> =
        | Success of 'T
        | Error of string
    let create2 (s:string) =
        if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$")
            then Success (EmailAddress s)
            else Error "Email address must contain an @ sign"

    // create w/ continuation
    let createWithContinuations success failure (s:string) =
        if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$")
            then s |> EmailAddress |> success
            else "Email address must contain an @ sign" |> failure

    // unwrap
    let value (EmailAddress e) = e
    // let value (e: EmailAddress) = e