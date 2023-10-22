module VigenereCypher

open System.Text

let private alphabet = [|'a'..'z'|]
let private n = alphabet.Length

// The algorithm works on a Wijener table without shifting, i.e. the first row and column start with 'a'...'z'.
// Therefore, we can calculate the required symbol without storing the whole table in memory.
let encrypt (text:string) (secret:string) =
    let lowerSecret = secret.ToLower()
    let lowerText = text.ToLower()
    let mutable keyIndex = 0
    let mutable resultString = StringBuilder()
    for symbol in lowerText do
        match Array.contains symbol alphabet with
        | true -> let index = (Array.findIndex (fun x -> x = symbol) alphabet + Array.findIndex (fun y -> y = lowerSecret[keyIndex]) alphabet) % n
                  resultString <- resultString.Append(alphabet[index])
                  keyIndex <- keyIndex + 1
                  if (keyIndex = secret.Length) then keyIndex <- 0 
        | false -> resultString <- resultString.Append(symbol)
    resultString.ToString()
    
let decrypt (text: string) (secret: string) =
    let lowerSecret = secret.ToLower()
    let lowerText = text.ToLower()
    let mutable keyIndex = 0
    let mutable resultString = StringBuilder()
    for symbol in lowerText do
        match Array.contains symbol alphabet with
        | true -> let mutable index = (Array.findIndex (fun x -> x = symbol) alphabet - Array.findIndex (fun y -> y = lowerSecret[keyIndex])alphabet) % n
                  if index < 0 then index <- n+index 
                  resultString <- resultString.Append(alphabet[index])
                  keyIndex <- keyIndex + 1
                  if (keyIndex = secret.Length) then keyIndex <- 0 
        | false -> resultString <- resultString.Append(symbol)
    resultString.ToString()