module TisburyTreasureHunt

let getCoordinate (line: string * string): string = snd line

let convertCoordinate (coordinate: string): int * char =
    // https://stackoverflow.com/questions/42820232/f-convert-a-char-to-int
    int coordinate[0] - int '0', coordinate[1]

let compareRecords (azarasData: string * string) (ruisData: string * (int * char) * string) : bool = 
    failwith "Please implement the 'compareRecords' function"

let createRecord (azarasData: string * string) (ruisData: string * (int * char) * string) : (string * string * string * string) =
    failwith "Please implement the 'createRecord' function"
