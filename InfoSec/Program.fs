module InfoSec
    
printfn "Enter the text to encrypt"
let text = System.Console.ReadLine()
let shift = int <| System.Console.ReadLine()
let encryptedText = CaesarCipher.encrypt text shift

printfn $"Encrypted text \n%s{encryptedText}"
let hacked = CaesarCipher.hack encryptedText

printfn $"Hacked text \n%s{hacked}"
let decryptedText = CaesarCipher.decrypt encryptedText shift

printfn $"Decrypted text \n%s{decryptedText}"