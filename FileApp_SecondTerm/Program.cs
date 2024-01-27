using System;
using System.IO;

namespace FileApp_SecondTerm
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Setup();
        }

        private static void Setup()
        {
            Console.Clear();
            Console.WriteLine("-- Vítejte ve digitální knihovně --");
            Console.WriteLine("K dispozici jsou následující možnosti: ");
            Console.WriteLine("[q] - vytvoření nové knihovny");
            Console.WriteLine("[w] - přidání nové knihy");
            Console.WriteLine("[x] - konec");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("");

            while (true)
            {
                Console.Write("Zadej klávesu: ");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        LibraryManager.CreateLibrary();
                        break;
                    case ConsoleKey.W:
                        Console.WriteLine("zmáčknuto w");
                        break;
                    case ConsoleKey.X:
                        return;
                    default:
                        Console.WriteLine("Neznámá klávesa..");
                        break;
                }
            }
        }
    }

    public static class LibraryManager
    {
        public static void CreateLibrary()
        {
            Console.WriteLine("");

            // Název složky
            Console.Write("Zadej jméno pro novou knihovnu: ");
            string folderName = Console.ReadLine();

            // Cesta na disk C
            string pathToC = Path.Combine("C:\\", folderName);

            // Zkontrolujte, zda složka neexistuje, a pokud ano, vytvořte ji s číslem v závorkách
            if (!Directory.Exists(pathToC))
            {
                Directory.CreateDirectory(pathToC);
                Console.WriteLine("Nová složka byla vytvořena na disku C.");
            }
            else
            {
                int suffix = 2; // Začneme od (2), protože (1) již existuje

                while (Directory.Exists(pathToC))
                {
                    folderName = $"{folderName}({suffix})";
                    pathToC = Path.Combine("C:\\", folderName);
                    suffix++;
                }

                Directory.CreateDirectory(pathToC);

                // Odstranění historie z názvu před výpisem
                Console.WriteLine(pathToC);
            }
        }
    }
}