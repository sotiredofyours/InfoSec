module CaesarCipherTests

open NUnit.Framework

[<Test>]
let Simple_Encrypt_Test() =
    let text, shift = "hello world", 3
    let encryptedText = CaesarCipher.encrypt text shift
    Assert.AreEqual("khoor zruog", encryptedText)
    
[<Test>]
let Simple_Decrypt_Test() =
    let text, shift = "khoor zruog", 3
    let decryptedText = CaesarCipher.decrypt text shift
    Assert.AreEqual("hello world", decryptedText)
    
[<Test>]
let Encrypt_With_No_Shift() =
    let text, shift = "hello world", 0
    let encryptedText = CaesarCipher.encrypt text shift
    Assert.AreEqual("hello world", encryptedText)
    
[<Test>]
let Encrypt_Unsupported_Alphabet() =
    let text, shift = "неподдерживаемый язык", 5
    let encryptedText = CaesarCipher.encrypt text shift
    Assert.AreEqual("неподдерживаемый язык", encryptedText)

[<Test>]
let Break_Encrypted_Text() =
    let shift = 3
    let text =
        """
        When I have free time, I often listen to my favourite composer, Wolfgang Amadeus Mozart, His works fill me with new energy, help me to relax and raise my mood. He was a very talented musician. His father taught him the violin, p
iano and musical theory. Little Mozart began to write music at the age of four. He wrote his first opera when he was eleven. When Mozart became an adult, he moved to Vienna. He had been successful in this town as a child prodigy
, but as an adult he found it difficult to find a job. In Vienna he met Haydn, who became his second father. Haydn supported the young composer and helped him in his musical career. Mozart's operas became very popular in the cit
y. He did not spend much time thinking about his next composition. Musical ideas sprang from his mind and he just had to write them down. At this time he married Constance Weber and wrote one of his most famous works — C-minor c
omposition. Mozart enjoyed a successful career. He worked a lot. He visited Prague with his operas. Writing his last work Requiem, commissioned by an unusual stranger, that it was his own requiem. He did not manage to finish his
 work and died at the age of 35 from poor health. The Requiem was completed by one of his pupils, Sussmary. Mozart's 49 symphonies and 18 operas are now world-famous and are considered to have healing power.
        """.ToLower()
    let encryptedText = CaesarCipher.encrypt text shift
    let hackedText = CaesarCipher.hack encryptedText 
    Assert.AreEqual(text, hackedText)