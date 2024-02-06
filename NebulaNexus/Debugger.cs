using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace NebulaNexus
{
    public abstract class Debugger
    {
        protected static readonly List<int> AssignedPlanetsModifier = new List<int>();
        protected static int GeneratedPlanets;
        protected static int GeneratedStars;
        protected static int GeneratedSystems;

        protected static void ShowDebug()
        {
            Console.WriteLine();

            Console.WriteLine("Used modifier for assigning planets: ");
            foreach (var modifier in AssignedPlanetsModifier)
            {
                Console.Write(modifier + " ");
            }

            Console.WriteLine("");
            Console.WriteLine($"Number of generated planets: {GeneratedPlanets}");
            Console.WriteLine($"Number of generated stars: {GeneratedStars}");
            Console.WriteLine($"Number of generated systems: {GeneratedSystems}");

            var localIDs = UniqueId.Ids.OrderBy(x => x);
            Console.WriteLine("ids: " + localIDs.Count());
            foreach (var id in localIDs)
            {
                AnsiConsole.Markup($"[yellow4]{id}[/]|");
            }
        }
    }
}