module RnaTranscription

let toRna (dna: string): string =
    let mapping ch =
        match ch with
        | 'G' -> 'C'
        | 'C' -> 'G'
        | 'T' -> 'A'
        | 'A' -> 'U'
        | _ -> '_'
    dna |> String.map mapping