// building blocks
let add2 x = x + 2
let mult3 x = x * 3
let square x = x * x
// test
[1..10] |> List.map add2 |> printfn "%A"
[1..10] |> List.map mult3 |> printfn "%A"
[1..10] |> List.map square |> printfn "%A"

// new functions using ">>" operator
let add2ThenMult3 = add2 >> mult3
let mult3ThenSquare = mult3 >> square
// test
add2ThenMult3 5
mult3ThenSquare 5
[1..10] |> List.map add2ThenMult3 |> printfn "%A"
[1..10] |> List.map mult3ThenSquare |> printfn "%A"

// helper functions;
let logMsg msg x = printf "%s%i" msg x; x     //without linefeed
let logMsgN msg x = printfn "%s%i" msg x; x   //with linefeed
// new composed function with new improved logging!
let mult3ThenSquareLogged =
   logMsg "before="
   >> mult3
   >> logMsg " after mult3="
   >> square
   >> logMsgN " result="
// test
mult3ThenSquareLogged 5
[1..10] |> List.map mult3ThenSquareLogged //apply to a whole list

// Create DSL: describe a set of "verbs" and "nouns"
// set up the vocabulary
type DateScale = Hour | Hours | Day | Days | Week | Weeks  // noun
type DateDirection = Ago | Hence                           // noun
// define a function that matches on the vocabulary
let getDate interval scale direction =                     // verb
    let absHours = match scale with
                   | Hour | Hours -> 1 * interval
                   | Day | Days -> 24 * interval
                   | Week | Weeks -> 24 * 7 * interval
    let signedHours = match direction with
                      | Ago -> -1 * absHours
                      | Hence ->  absHours
    System.DateTime.Now.AddHours(float signedHours)
// test some examples
printfn $"{getDate 5 Days Ago}"
printfn $"{getDate 1 Hour Hence}"

// 1. define underlying type (noun?)
type FluentShape = {
    label : string;
    color : string;
    onClick : FluentShape->FluentShape // a function type
    }
// 2. define basic functions (verbs?)
// note: for "method chaining" to work: return object that can be used next
let defaultShape =
    {label=""; color=""; onClick=fun shape->shape}
let click shape =
    shape.onClick shape
let display shape =
    printfn "My label=%s and my color=%s" shape.label shape.color
    shape
// 3. define helper functions as "mini-language" (more verbs?)
let setLabel label shape =
   {shape with FluentShape.label = label}
let setColor color shape =
   {shape with FluentShape.color = color}
// add a click action to what is already there
let appendClickAction action shape =
   {shape with FluentShape.onClick = shape.onClick >> action}
// 4. Use "mini-language" to compose more complex functions
let setRedBox = setColor "red" >> setLabel "box"
let setBlueBox = setRedBox >> setColor "blue"
let changeColorOnClick color = appendClickAction (setColor color)
// test (simple example)
let redBox = defaultShape |> setRedBox
let blueBox = defaultShape |> setBlueBox
// create a shape that changes color when clicked
redBox
    |> display
    |> changeColorOnClick "green"
    |> click
    |> display  // new version after the click
// create a shape that changes label and color when clicked
blueBox
    |> display
    |> appendClickAction (setLabel "box2" >> setColor "green")
    |> click
    |> display  // new version after the click
// test (complex example)
printfn "complex example..."
let rainbow =
    ["red";"orange";"yellow";"green";"blue";"indigo";"violet"]
let showRainbow =
    let setColorAndDisplay color = setColor color >> display
    rainbow
    |> List.map setColorAndDisplay
    |> List.reduce (>>)
// test the showRainbow function
defaultShape |> showRainbow