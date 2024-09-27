module NucleotideCount
open System.Text.RegularExpressions

let nucleotideCounts (strand: string): Option<Map<char, int>> =
    let badStrand = Regex(@"[^ACGT]")
    let countNucl (acc: Map<char, int>) char = acc.Add(char, acc[char]+1)
    let empty = Map([('A', 0); ('C', 0); ('G', 0); ('T', 0)])
    match strand with
    | _ when badStrand.IsMatch(strand) -> None
    | _ -> (empty, strand) ||> Seq.fold countNucl |> Some