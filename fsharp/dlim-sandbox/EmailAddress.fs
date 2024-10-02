// type definitions
module EmailAddress
    type T = EmailAddress of string

    // wrap
    // create w/ continuation
    let createWithCont success failure (s:string) =
        if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$")
            then s |> EmailAddress |> success
            else "Email address must contain an @ sign" |> failure

    // create w/ option type
    let create s =
        let success e = Some e
        let failure _ = None
        createWithCont success failure s

    // // create w/ union type
    // type CreationResult<'T> =
    //     | Success of 'T
    //     | Error of string
    // let create2 (s:string) =
    //     if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$")
    //         then Success (EmailAddress s)
    //         else Error "Email address must contain an @ sign"

    // unwrap with continuation
    let apply func (EmailAddress e) = func e

    // unwrap
    let value e = apply id e