// 1. option type (Some()/None) to handle 
let getFileInfo filePath =
   let fi = new System.IO.FileInfo(filePath)
   if fi.Exists then Some(fi) else None
let goodFileName = "good.txt"
let badFileName = "bad.txt"
let goodFileInfo = getFileInfo goodFileName // Some(fileinfo)
let badFileInfo = getFileInfo badFileName   // None
match goodFileInfo with
  | Some fileInfo ->
      printfn "the file %s exists" fileInfo.FullName
  | None ->
      printfn "the file doesn't exist"
match badFileInfo with
  | Some fileInfo ->
      printfn "the file %s exists" fileInfo.FullName
  | None ->
      printfn "the file doesn't exist"

// 2. iteration/recursion, edge cases
let rec movingAverages list =
    match list with
    // if input is empty, return an empty list
    | [] -> []
    // otherwise process pairs of items from the input
    | x::y::rest ->
        let avg = (x+y)/2.0
        //build the result by recursing the rest of the list
        avg :: movingAverages (y::rest)
    // for one item, return an empty list
    | [_] -> []
// test
printfn "%A" (movingAverages [1.0])
printfn "%A" (movingAverages [1.0; 2.0])
printfn "%A" (movingAverages [1.0; 2.0; 3.0])

// 3. union for good and bad cases (instead of throwing exceptions)
// define a "union" of different cases
type Result<'a, 'b> =
    | Success of 'a  // 'a means generic type. The actual type will be determined when it is used.
    | Failure of 'b  // generic failure type as well
// define all possible errors
type FileErrorReason =
    | FileNotFound of string
    | UnauthorizedAccess of string * System.Exception

// define a low level function in the bottom layer
let performActionOnFile action filePath =
   try
      //open file, do the action and return the result
      use sr = new System.IO.StreamReader(filePath:string)
      let result = action sr  //do the action to the reader
      Success (result)        // return a Success
   with      // catch some exceptions and convert them to errors
      | :? System.IO.FileNotFoundException as ex
          -> Failure (FileNotFound filePath)
      | :? System.Security.SecurityException as ex
          -> Failure (UnauthorizedAccess (filePath,ex))
      // other exceptions are unhandled
// a function in the middle layer
let middleLayerDo action filePath =
    let fileResult = performActionOnFile action filePath
    // do some stuff
    fileResult //return
// a function in the top layer
let topLayerDo action filePath =
    let fileResult = middleLayerDo action filePath
    // do some stuff
    fileResult //return

// get the first line of the file
let printFirstLineOfFile filePath =
    let fileResult = topLayerDo (fun fs->fs.ReadLine()) filePath
    match fileResult with
    | Success result ->
        // note type-safe string printing with %s
        printfn "first line is: '%s'" result
    | Failure reason ->
       match reason with  // must match EVERY reason
       | FileNotFound file ->
           printfn "File not found: %s" file
       | UnauthorizedAccess (file,_) ->
           printfn "You do not have access to the file: %s" file
// get the Length of the text in the file
let printLengthOfFile filePath =
   let fileResult =
     topLayerDo (fun fs->fs.ReadToEnd().Length) filePath

   match fileResult with
   | Success result ->
      // note type-safe int printing with %i
      printfn "length is: %i" result
   | Failure _ ->
      printfn "An error happened but I don't want to be specific"
// test
let writeSomeText filePath someText =
    use writer = new System.IO.StreamWriter(filePath:string)
    writer.WriteLine(someText:string)
writeSomeText goodFileName "hello"

printFirstLineOfFile "good.txt"
printLengthOfFile "bad.txt"

