using System.Text;
using RC5;

var key = "FUCKFUCKFUCKFUCKFUCKFUCKFUCKFUCK";
var rc = new Rc5(Encoding.ASCII.GetBytes(key));
var text = "1234567812343678"u8.ToArray();
var encryptedBuff = new byte[16];
rc.Cipher(text, encryptedBuff);
Console.WriteLine(Encoding.UTF8.GetString(encryptedBuff));
rc.Decipher(encryptedBuff, text);
Console.WriteLine(Encoding.UTF8.GetString(text));