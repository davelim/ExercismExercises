let genericLogger before after anyFunc input =
  before input
  let result = anyFunc input
  after result
  result

let add1 input = input + 1

genericLogger
  (fun x -> printf "before=%i." x) // lambda
  (fun x -> printfn " after=%i." x)
  add1
  2

genericLogger
  (fun x -> printf "started with=%i " x)
  (fun x -> printfn " ended with=%i" x)
  add1
  2

// "bake in" before and after functions
let add1WithConsoleLogging =
    genericLogger
        (fun x -> printf "input=%i. " x)
        (fun x -> printfn " result=%i" x)
        add1
// test
add1WithConsoleLogging 2
add1WithConsoleLogging 3
add1WithConsoleLogging 4
[1..5] |> List.map add1WithConsoleLogging