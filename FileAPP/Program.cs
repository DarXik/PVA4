using System;
using System.Diagnostics;
using System.IO;

namespace FileAPP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Přejete si vytvořit nový [v], upravit existující [p] nebo jen přečíst [r] soubor? ");
                switch (Console.ReadLine())
                {
                    case "v":
                        CreateFile();
                        break;
                    case "p":
                        EditFile();
                        break;
                    case "r":
                        Console.WriteLine("Zadej cestu k existujícímu souboru: ");
                        string readPath = Console.ReadLine();

                        while (!File.Exists(readPath))
                        {
                            Console.WriteLine("Soubor neexistuje");
                            readPath = Console.ReadLine();
                        }

                        using (StreamReader reader = new StreamReader(readPath))
                        {
                            string content = reader.ReadToEnd();
                            Console.WriteLine("V souboru je zapsáno: " + content);
                        }
                        break;
                    default:
                        Console.WriteLine("Bylo zadán neplatný vstup");
                        break;
                }
            }
        }

        static void CreateFile()
        {
            Console.WriteLine("Zadej cestu pro nový soubor: ");
            string writePath = Console.ReadLine();
            while (!Directory.Exists(writePath))
            {
                Console.WriteLine("Zadaná cesta neexistuje, zadej ji znovu ...");
                writePath = Console.ReadLine();
            }

            Console.WriteLine("Zadej název souboru, vč. přípony: ");
            string fileName = Console.ReadLine();

            string finalPath = writePath + @"\" + fileName + @".txt"; // dořešit
            while (File.Exists(finalPath))
            {
                Console.WriteLine("Již existuje, zkus to znovu ...");
                fileName = Console.ReadLine();
                finalPath = writePath + @"\" + fileName;
            }

            Console.WriteLine(writePath);

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

        static void EditFile()
        {
            Console.WriteLine("Zadej cestu k existujícímu souboru: ");
            string readPath = Console.ReadLine();

            while (!File.Exists(readPath))
            {
                Console.WriteLine("Soubor neexistuje");
                readPath = Console.ReadLine();
            }

            using (StreamReader reader = new StreamReader(readPath))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine("V souboru je zapsáno: " + content);
            }

            Console.WriteLine("Přejete si soubor přepsat [p] nebo upravit [u]? ");
            string inputRead = Console.ReadLine();

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

                    Console.WriteLine("Soubor s cestou: " + readPath + " byl přepsán následujícím: " + inputOverwrite);
                    break;

                case "u":
                    Console.WriteLine("Co si přejete do souboru připojit: ");
                    var inputAppend = Console.ReadLine();

                    using (StreamWriter append = new StreamWriter(readPath, true))
                    {
                        append.WriteLine(inputAppend);
                    }

                    Console.WriteLine("Do souboru s cestou: " + readPath + " bylo přidáno následující: " + inputAppend);
                    break;
                default:
                    Console.WriteLine("Zadána neplatná operace");
                    break;
            }
        }
    }
}