using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Spectre.Console;

namespace FileApp_SecondTerm
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Program
    {
        private static List<Book> AddedBooks = new List<Book>();
        private static List<string> CreatedLibraries = new List<string>();

        public static void Main(string[] args)
        {
            Setup();
        }

        private static void Setup()
        {
            while (true)
            {
                Console.Clear();
                AnsiConsole.WriteLine("-- Vítejte v digitální knihovně --");
                Console.WriteLine("-----------------------------------------\n");

                var intro = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("K dispozici jsou následující možnosti: ")
                        .PageSize(3)
                        .HighlightStyle("bold purple")
                        .AddChoices(
                            "vytvoření nové knihovny",
                            "přidání nové knihy",
                            "konec"
                        ));

                switch (intro)
                {
                    case "vytvoření nové knihovny":
                        LibraryManager.CreateLibrary(CreatedLibraries);
                        Console.ReadKey();
                        break;
                    case "přidání nové knihy":
                        AnsiConsole.Markup("[bold olive]Vlož název knihy: [/]");
                        var bookName = Console.ReadLine();

                        AnsiConsole.Markup("[bold dodgerblue3]Vlož jméno autora: [/]");
                        var bookAuthor = Console.ReadLine();

                        DateTime bookReleaseDate;
                        while (true)
                        {
                            AnsiConsole.Markup("[bold darkgreen]Vlož datum uvedení {yyyy-MM-dd}: [/]");
                            var userInput = Console.ReadLine();
                            if (!DateTime.TryParseExact(userInput, "yyyy-MM-dd", null, DateTimeStyles.None, out bookReleaseDate))
                            {
                                AnsiConsole.MarkupLine("[underline red]Zadán chybný formát![/]");
                            }
                            else
                            {
                                break;
                            }
                        }

                        var bookLanPrompt = new SelectionPrompt<Language>()
                            .Title("[bold darkorange3]Vyber z podporovaných jazyků: [/]")
                            .PageSize(6)
                            .HighlightStyle("darkorange3")
                            .MoreChoicesText("[grey](Posun nahoru a dolu pro více možností)[/]");

                        foreach (Language lang in Enum.GetValues(typeof(Language)))
                        {
                            bookLanPrompt.AddChoice(lang);
                        }

                        var bookLanguage = AnsiConsole.Prompt(bookLanPrompt);
                        var book = new Book(bookName, bookAuthor, bookReleaseDate, bookLanguage);

                        AddedBooks.Add(book);

                        Console.WriteLine("");
                        AnsiConsole.WriteLine("Přidána kniha: ");
                        var table = new Table()
                            .Border(TableBorder.Rounded)
                            .AddColumn(new TableColumn("[bold olive]Název[/]"))
                            .AddColumn(new TableColumn("[bold dodgerblue3]Autor[/]"))
                            .AddColumn(new TableColumn("[bold darkgreen]Datum uvedení[/]"))
                            .AddColumn(new TableColumn("[bold darkorange3]Jazyk[/]"));

                        table
                            .AddRow(book.Name, book.Author, $"{bookReleaseDate.ToString("dd. MMMM yyyy", new CultureInfo("cs-CZ"))}", book.Language.ToString());

                        AnsiConsole.Write(table);

                        Console.WriteLine("");
                        var libraryPrompt = new SelectionPrompt<string>()
                            .Title("[bold mediumpurple1]Vyber knihovnu pro knihu [/]")
                            .HighlightStyle("bold purple");

                        foreach (var library in CreatedLibraries)
                        {
                            libraryPrompt.AddChoice(library);
                        }

                        var chosenLibrary = AnsiConsole.Prompt(libraryPrompt);
                        var bookNameFinal = $"{bookName}-{bookAuthor}-{bookReleaseDate.ToString()}-{bookLanguage}.txt";
                        var bookPathFinal = $"{chosenLibrary}/{bookNameFinal}";

                        Console.WriteLine(bookNameFinal); // vypisuje 2024/12/12 00:00:00
                        Console.WriteLine(bookPathFinal);


                        // if (Directory.Exists(chosenLibrary))
                        // {
                        //     if (!File.Exists(bookPathFinal))
                        //     {
                        //         using (var writer = new StreamWriter(bookPathFinal))
                        //         {
                        //             writer.Flush();
                        //         }
                        //     }
                        //     else
                        //     {
                        //         AnsiConsole.MarkupLine("[underline red]Kniha již existuje![/]");
                        //     }
                        // }

                        Console.ReadKey();
                        break;
                    case "konec":
                        return;
                    default:
                        Console.WriteLine("Neznámá klávesa..");
                        break;
                }
            }
        }
    }

    public class Book
    {
        public string Name { get; }
        public string Author { get; }
        public DateTime ReleaseDate { get; }
        public Language Language { get; }

        public Book(string name, string author, DateTime releaseDate, Language language)
        {
            Name = name;
            Author = author;
            ReleaseDate = releaseDate;
            Language = language;
        }
    }

    public enum Language
    {
        cz, // Czech
        en, // English
        de, // German
        fr, // French
        es, // Spanish
        it, // Italian
        pt, // Portuguese
        ru, // Russian
        zh, // Chinese
        ja, // Japanese
        ko, // Korean
        ar, // Arabic
        hi, // Hindi
        nl, // Dutch
        sv, // Swedish
        fi, // Finnish
        pl, // Polish
        el, // Greek
        tr, // Turkish
        ro, // Romanian
        hu, // Hungarian
        da, // Danish
        no, // Norwegian
        bg, // Bulgarian
        sk, // Slovak
    }

    public static class LibraryManager
    {
        public static void CreateLibrary(List<string> createdLibraries)
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
            }

            createdLibraries.Add(pathToC);
            var path = new TextPath(pathToC)
            {
                SeparatorStyle = new Style(foreground: Color.Green)
            };

            AnsiConsole.Write("Vytvořená knihovna: ");
            AnsiConsole.Write(path);
            Console.WriteLine("");
        }
    }
}