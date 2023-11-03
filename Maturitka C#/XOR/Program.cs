using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine(isPrime(15));
    }

    public static bool isPrime(int number)
    {
        if (number < 2) return false; // číslo menší než 2 není pč
        for (int i = 2; i < number; i++)
        {
            if (number % i == 0) return false; // pokud je číslo dělitelné i, není pč
        }

        return true; // ani jedna podmínka nesplněna -> pč
    }


    public void Reader()
    {
        // čtení texťáku
        string readPath = "C:\\Users\\pitra\\Desktop\\Maturitka C#\\XOR\\NewFile1.txt";
        // string readPath = "C:/Users/pitra/Desktop/Maturitka C#/XOR/NewFile1.txt";

        using (StreamReader reader = new StreamReader(readPath))
        {
            string content = reader.ReadToEnd();
            Console.WriteLine(content); // obsah souboru
        }

        // kontrola zdali soubor existuje
        if (File.Exists(readPath))
        {
        }
        else
        {
        }

        // zápis do texťáku
        string writePath = "C:\\Users\\pitra\\Desktop\\Maturitka C#\\XOR\\novytext.txt";
        using (StreamWriter writer = new StreamWriter(writePath))
        {
            writer.WriteLine("První řádek");
        }

        Console.WriteLine("text byl zapsán");

        // zápis do existujícího souboru
        using (StreamWriter appendWriter = new StreamWriter(writePath, true))
        {
            appendWriter.WriteLine("Další řádek");
        }

        Console.WriteLine("Další text byl zapsán");
    }
}