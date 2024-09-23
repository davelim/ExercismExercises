(* Function-oriented (not object-oriented): functions can be passed as arguments
- composition: build larger systems from smaller one
- factoring and refactoring
- good design
*)
(* Expressions (not statements): every chunk of code always retuns a value
*)
(* Algebraic types (for domain models): compound types are built in 2 ways
- "product" type: combination of values
- disjoint union: choice between a set of types
*)
// product type:
type IntAndBool = {intPart: int; boolPart: bool}
let x = {intPart=1; boolPart=false}
// disjoint union:
type IntOrBool =
  | IntChoice of int
  | BoolChoice of bool
let y = IntChoice 42
let z = BoolChoice true

(* Pattern matching (for control flow): pattern-matching for
  conditional expressions/control flow
*)
// if-then-else
let IfThenElse booleanExpression =
    match booleanExpression with
    | true -> printfn "true branch"
    | false -> printfn "false branch"
// loops done using recursion
let RecursionLoop aList =
    match aList with
    | [] ->
        printfn "empty case"
    | first::rest ->
        printfn "case with at least one element. Process first element then call recursively the rest of the list"

// Pattern mattching with union type
// - Note: Circle, Rectangle, etc is not an actual "type", rather different
//   cases of that type.
type Shape =
  | Circle of radius:int
  | Rectangle of height:int * width:int
  | Point of x:int * y:int
  | Polygon of pointList:(int * int) list
// "draw" function with shape param
let draw shape =
  match shape with
  | Circle radius ->
      printfn "The circle has a radius of %d" radius
  | Rectangle (height,width) ->
      printfn "The rectangle is %d high by %d wide" height width
  | Polygon points ->
      printfn "The polygon is made of these points %A" points
  | _ -> printfn "I don't recognize this shape"
let circle = Circle(10)
let rect = Rectangle(4,5)
let point = Point(2,3)
let polygon = Polygon( [(1,1); (2,2); (3,3)])
[circle; rect; polygon; point] |> List.iter draw