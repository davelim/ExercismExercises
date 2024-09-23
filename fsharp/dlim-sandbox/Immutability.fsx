// Think about transforming the data (rather than mutating it in place)
type PersonalName = {FirstName:string; LastName:string}
// immutable person
let john = {FirstName="John"; LastName="Doe"}
// "alice" is "john" with FirstName "Alice"
let alice = {john with FirstName="Alice"}

// create an immutable list
let list1 = [1;2;3;4]
// prepend to make a new list
let list2 = 0::list1
// get the last 4 of the second list
let list3 = list2.Tail
// the two lists are the identical object in memory!
printfn "%A" (System.Object.ReferenceEquals(list1,list3)) // true