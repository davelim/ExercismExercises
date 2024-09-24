module TisburyTreasureHunt

let getCoordinate (line: string * string): string = snd line

// TODO: function currently only handles happy case. What error cases should
//   function account for?
let convertCoordinate (coordinate: string): int * char =
    // https://stackoverflow.com/questions/42820232/f-convert-a-char-to-int
    int coordinate[0] - int '0', coordinate[1]

let compareRecords (azarasData: string * string) (ruisData: string * (int * char) * string) : bool =
    let azarasCoordinate = azarasData |> getCoordinate |> convertCoordinate
    let (_, ruisCoordinate, _) = ruisData
    ruisCoordinate = azarasCoordinate

let createRecord (azarasData: string * string) (ruisData: string * (int * char) * string) : (string * string * string * string) =
    failwith "Please implement the 'createRecord' function"
