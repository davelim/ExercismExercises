let add1 input = input + 1
let times2 input = input * 2

// Generic*
let genericLogger anyFunc input =
   printfn "input is %A" input   //log the input
   let result = anyFunc input    //evaluate the function
   printfn "result is %A" result //log the result
   result                        //return the result

let add1WithLogging = genericLogger add1
let times2WithLogging = genericLogger times2

// test
add1WithLogging 3
times2WithLogging 3
[1..5] |> List.map add1WithLogging

// Generic*
let genericTimer anyFunc input =
   let stopwatch = System.Diagnostics.Stopwatch()
   stopwatch.Start()
   let result = anyFunc input  //evaluate the function
   printfn "elapsed ms is %A" stopwatch.ElapsedMilliseconds
   result

let add1WithTimer = genericTimer add1WithLogging
// test
add1WithTimer 3
// TODO:
// - generic caching wrapper: value is only calculated once
// - generic "lazy" wrapper: inner function is only called when result is needed

// "strategy" pattern
type Animal(noiseMakingStrategy) =
   member this.MakeNoise =
      noiseMakingStrategy() |> printfn "Making noise %s"
// now create a cat
let meowing() = "Meow"
let cat = Animal(meowing)
cat.MakeNoise
// .. and a dog
let woofOrBark() = if (System.DateTime.Now.Second % 2 = 0)
                   then "Woof" else "Bark"
let dog = Animal(woofOrBark)
dog.MakeNoise
dog.MakeNoise