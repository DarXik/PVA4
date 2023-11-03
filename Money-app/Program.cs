using System;
using System.Collections.Generic;

namespace Money_app
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<string> Users = new List<string>();
            Users.Add("Pitra");
            Users.Add("Židlický");
            Users.Add("Novák");

            Console.WriteLine("Your personal Money app " +
                              "\n --------------------------------------");
            Console.WriteLine("Zadej kterou operaci chceš provést: " +
                              "\n [q] přidání nového uživatele " +
                              "\n [w] odebrání uživatele " +
                              "\n [e] přidat platbu " +
                              "\n [r] zobrazit dluh");

            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.Q:

                    break;
                case ConsoleKey.W:
                    break;
            }
        }
    }
}