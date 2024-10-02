#load "EmailAddress.fs"
open EmailAddress

// use cases:
// 1. use (default) constructor as a function
"a@foo.com" |> EmailAddress // returns "EmailAddress "a@foo.com"
["a@foo.com"; "b@foo.com"; "c@foo.com"] |> List.map EmailAddress
let addresses =
    ["a@foo.com"; "b@foo.com"; "c@foo.com"]
    |> List.map EmailAddress

// 2. use inline deconstruction
let a' = "a@foo.com" |> EmailAddress
let (EmailAddress a'') = a' // a'' is assigned to "a@foo.com" string
let addresses' =
    addresses
    |> List.map (fun (EmailAddress e) -> e) // addresses' is a string list

// 3. use create w/ continuation
let success (EmailAddress s) = printfn "success creating email %s" s
let failure msg = printfn "error creating email: %s" msg
EmailAddress.createWithContinuations success failure "foo.com"
EmailAddress.createWithContinuations success failure "a@foo.com"

// 4. wrap/unwrap
let addr1 = EmailAddress.create "a@foo.com"
let addr2 = EmailAddress.create "foo.com"

let testAddr addr =
    match addr with
    | Some e -> EmailAddress.value e |> printfn "value is %s"
    | None -> printfn "None!"
testAddr addr1
testAddr addr2