module VigenereCipher.Tests

open NUnit.Framework

let secret = "LEMON"

[<Test>]
let Simple_Encrypt_Test() =
    let text = "ATTACK AT DOWN"
    let encryptedText = VigenereCypher.encrypt text secret
    Assert.AreEqual("lxfopv ef rbhr", encryptedText) 


[<Test>]
let Simple_Decrypt_Test() =
    let text = "lxfopv ef rbhr"
    let decryptedText = VigenereCypher.decrypt text secret
    Assert.AreEqual("attack at down", decryptedText)