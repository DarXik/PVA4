using System;
using System.IO;

namespace FileAPP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Co si přejete udělat? \n--------------------------- \nVytvořit nový soubor [v] \nUpravit soubor [u] \nPřečíst soubor [p] \nUkončit [x]");

                switch (Console.ReadLine())
                {
                    case "v":
                        CreateFile();
                        break;
                    case "u":
                        EditFile();
                        break;
                    case "p":
                        ReadFile();
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine("\nZadána neplatná operace");
                        break;
                }
            }
        }

        private static void CreateFile()
        {
            Console.WriteLine("Zadej cestu pro nový soubor: ");
            var writePath = Console.ReadLine()?.Trim();

            while (!Directory.Exists(writePath))
            {
                Console.WriteLine("Zadaná cesta neexistuje, zadej ji znovu ...");
                writePath = Console.ReadLine()?.Trim();
            }

            if (!writePath.EndsWith(@"\"))
            {
                writePath = writePath + @"\";
            }

            Console.WriteLine("Zadej název souboru (bez přípony): ");
            var fileName = Console.ReadLine()?.Trim();

            var finalPath = Path.Combine(writePath, fileName + ".txt"); // dořešit

            while (File.Exists(finalPath) || fileName == null)
            {
                Console.WriteLine("Již existuje nebo zadán neplatný název, zkus to znovu ...");
                fileName = Console.ReadLine();
                finalPath = Path.Combine(writePath, fileName + ".txt");
            }

            Console.WriteLine("Co si přejete do souboru zapsat: ");
            var input = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(finalPath))
            {
                writer.WriteLine(input);
                writer.Flush();
            }

            Console.WriteLine("Do souboru " + finalPath);
            Console.WriteLine("Bylo zapsáno: ");
            Console.WriteLine(input);
        }

        private static void EditFile()
        {
            Console.WriteLine("Zadej cestu k souboru: ");
            var readPath = Console.ReadLine();

            while (!File.Exists(readPath))
            {
                Console.WriteLine("Soubor nenalezen, zkus to znovu...");
                readPath = Console.ReadLine();
            }

            using (StreamReader reader = new StreamReader(readPath))
            {
                var content = reader.ReadToEnd();
                Console.WriteLine("V souboru je zapsáno: \n" + content);


                Console.WriteLine("Vyberte operaci: \npřepsat [p] \nupravit [u] \nzpět do menu [x]");
                var inputRead = Console.ReadLine();

                switch (inputRead)
                {
                    case "p":
                        Console.WriteLine("Co si přejete do souboru zapsat: ");
                        var inputOverwrite = Console.ReadLine();

                        using (StreamWriter writer = new StreamWriter(readPath))
                        {
                            writer.WriteLine(inputOverwrite);
                            writer.Flush();
                        }

                        Console.WriteLine("Soubor " + readPath);
                        Console.WriteLine("Byl přepsán následujícím: ");
                        Console.WriteLine(inputOverwrite);
                        break;

                    case "u":
                        Console.WriteLine("Co si přejete do souboru přidat: ");
                        var inputAppend = Console.ReadLine();

                        using (StreamWriter append = new StreamWriter(readPath, true))
                        {
                            append.WriteLine(inputAppend);
                        }

                        Console.WriteLine("Do souboru " + readPath);
                        Console.WriteLine("Bylo přidáno následující: ");
                        Console.WriteLine(inputAppend);
                        break;
                    case "x":
                        break;
                    default:
                        Console.WriteLine("Zadána neplatná operace");
                        break;
                }
            }
        }

        private static void ReadFile()
        {
            Console.WriteLine("Zadej cestu k souboru: ");
            var readPath = Console.ReadLine();

            while (!File.Exists(readPath))
            {
                Console.WriteLine("Soubor nenalezen, zkus to znovu...");
                readPath = Console.ReadLine();
            }

            using (StreamReader reader = new StreamReader(readPath))
            {
                var content = reader.ReadToEnd();
                Console.WriteLine("V souboru je zapsáno: \n" + content);
            }
        }
    }
}