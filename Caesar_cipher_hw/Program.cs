// Vytvořte funkci encrypt_caesar(text, shift), vezme vstupní text a posune každý znak v textu o zadaný počet pozic podle klíče (posunu)
// Při šifrování počítejte pouze se zadaním znaků bez diakritiky, mezery nechte nezměněné, abeceda anglická (26písmen), nerozlišujeme malá a velká (toLower),
// při dosažení konce šifrování se vracíme od začátku abecedy
// vytvořte funkci decrypt_caesar(text, shift), vezme zašifrovaný text a dešifruje ho pomocí zpětného posunu o zadaný počet pozic  podle šifrovacího klíče,
// stejně jako při šifrování ignorujte neznáme znaky
// rozšíření:
// metoda pro bruteforce
// automatické rozpoznaní šifrovacího klíče pomocí techniky frekvenční analýzy písmen v textu

using System;
using System.Linq;
using System.Text;

namespace Caesar_cipher_hw
{
    internal class Program
    {
        static string[] alphabet = new string[]
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
            "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };

        public static void Main(string[] args)
        {
            // char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            while (true)
            {
                Console.WriteLine("[1] encrypt \n[2] decrypt \n[3] bruteforce");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Enter plain text: ");
                        var input = Console.ReadLine()?.ToUpper();

                        Console.WriteLine("Enter shift: ");
                        var shift = int.Parse(Console.ReadLine());

                        EncryptMessage(input, shift);
                        break;

                    case "2":
                        Console.WriteLine("Enter cipher text: ");
                        input = Console.ReadLine()?.ToUpper();

                        Console.WriteLine("Enter shift: ");
                        shift = int.Parse(Console.ReadLine());
                        Console.WriteLine("Decrypted text: " + DecryptMessage(input, shift));
                        break;
                    case "3":
                        Console.WriteLine("Enter cipher text: ");
                        input = Console.ReadLine()?.ToUpper();

                        Bruteforce(input);
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
        }

        private static void Bruteforce(string input)
        {
            try
            {
                for (int i = 0; i < 26; i++)
                {
                    Console.WriteLine($"{DecryptMessage(input, i)} : {i}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void EncryptMessage(string text, int shift)
        {
            var cipherText = new StringBuilder();

            foreach (var t in text)
            {
                if (alphabet.Contains(t.ToString()) || t.ToString() == " ")
                {
                    if (t.ToString() != " ")
                    {
                        var pos = (Array.IndexOf(alphabet, t.ToString()) + shift) % 26;
                        cipherText.Append(alphabet[pos]);
                    }
                    else
                    {
                        cipherText.Append(" ");
                    }
                }
                else
                {
                    Console.WriteLine(text);
                }
            }

            if (text.ToUpper() == cipherText.ToString().ToUpper())
            {
                Console.WriteLine("your shift won't encrypt the plain text... try different one");
            }

            Console.WriteLine("Decrypted message: " + cipherText);
        }

        private static string DecryptMessage(string text, int shift)
        {
            var decipherText = new StringBuilder();

            foreach (var t in text)
            {
                if (alphabet.Contains(t.ToString()) || t.ToString() == " ")
                {
                    if (t.ToString() != " ")
                    {
                        var pos = Array.IndexOf(alphabet, t.ToString()) - shift;
                        while (pos < 0)
                        {
                            pos = pos + 26;
                        }

                        decipherText.Append(alphabet[pos]);
                    }
                    else
                    {
                        decipherText.Append(" ");
                    }
                }
                else
                {
                    return text;
                }
            }

            return decipherText.ToString();
        }
    }
}