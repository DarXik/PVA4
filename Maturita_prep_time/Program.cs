using System.Security.Cryptography;
using System.Text;

namespace Maturita_prep_time
{
    class CustomException : Exception
    {
        public CustomException(string message)
        {
            Console.WriteLine("chyba chyba");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime time = DateTime.Now;

            Console.WriteLine(time.AddYears(12));

            var mytime = new DateTime(2022, 01, 10, 1, 1, 1);
            TimeSpan ts = new TimeSpan(5, 4, 2);

            Console.WriteLine(ts);

            string plaintext = "Toto je plaintext, který lze bez prolbému šifrovat jak se komu zlíbí";
            string key = "key 123, tímto prosím šifruj";

            using (var rsa = new RSACryptoServiceProvider())
            {
                var encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(plaintext), false);

                Console.WriteLine(Convert.ToBase64String(encrypted));
            }

            using (Aes aes = Aes.Create())
            {
                byte[] keys = aes.Key;
                byte[] iv = aes.IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(keys, iv);
                byte[] encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plaintext), 0, Encoding.UTF8.GetBytes(plaintext).Length);

                Console.WriteLine(Convert.ToBase64String(encrypted));
            }
        }
    }
}
