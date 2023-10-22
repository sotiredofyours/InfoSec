module InfoSec
    
printfn "Enter the text to encrypt"
let text = System.Console.ReadLine()

printfn "Enter the key to encrypt"
let shift = System.Console.ReadLine()
let encryptedText = VigenereCypher.encrypt text shift

printfn $"Encrypted text \n%s{encryptedText}"
let decryptedText = VigenereCypher.decrypt encryptedText shift

printfn $"Decrypted text \n%s{decryptedText}"