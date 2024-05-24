using System.Security.Cryptography;

var randomNumber = new byte[4];

// using - po skončení se zavolá Dispose - uvolnění zdroje
using (var rng = RandomNumberGenerator.Create()) // nová instance třídy
{
    // generuje krypto. bezpečným způsobem náhodné byty a ukládá do pole
    rng.GetBytes(randomNumber);
}

// Převedení celé byte pole na int, začne od prvního indexu v poli (0)
// převede náhodné byty na náhohdné celé číslo
int randomInt = BitConverter.ToInt32(randomNumber, 0);

Console.WriteLine("Náhodné číslo: " + randomInt);