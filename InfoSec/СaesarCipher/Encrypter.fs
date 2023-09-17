module CaesarCipher

open System
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

let private alphabet = [|'a'..'z'|]

//The index of most common letter of the Latin alphabet.
let private mostFrequencyLetterIndex = 4
let shiftSymbol (shift:int) (x:char) =
    let shiftedChar = (Array.IndexOf(alphabet, x) +  shift) % alphabet.Length
    alphabet[shiftedChar]

let private mostFrequency frequencyMap:Map<char, int> =
        frequencyMap
      
let encrypt (text:string) (shift:int) =
    text.ToLower()
    |> Seq.map (fun x ->
                if Array.contains x alphabet then
                    shiftSymbol shift x
                else x
        )
    |> String.Concat

let decrypt (text:string) (shift:int) =
    text.ToLower()
    |> Seq.map (fun x ->
            if Array.contains x alphabet then
                shiftSymbol (alphabet.Length - shift) x
            else x
        )
    |> String.Concat
    
// Algorithm is simple.
// We build a map of letter frequencies based on the text. Calculate the approximate shift using the most common letter in the alphabet.
let hack (encryptedText:string) =
    let mostFrequencyPair =
        encryptedText.ToLower()
        |> Seq.filter Char.IsLetter
        |> Seq.filter (fun ch ->  alphabet |> Array.contains  ch)
        |> Seq.groupBy id
        |> Seq.map (fun (letter, group) -> letter, Seq.length group)
        |> Map.ofSeq
        |> Seq.sortByDescending (fun x -> x.Value)
        |> Seq.head
    
    let guessedShift = Array.IndexOf(alphabet, mostFrequencyPair.Key) - mostFrequencyLetterIndex
    decrypt encryptedText guessedShift