using System;
using System.Collections.Generic;
using System.Linq;

namespace SettleUP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Func<string, User> createUser = (name) => new User(name);
            List<User> Users = new List<User>();
            Users.Add(new User("Pitra"));
            Users.Add(new User("Židlický"));
            Users.Add(new User("Cvach"));
            Users.Add(new User("HobzaZmrd"));

            Console.WriteLine("Settle up your expenses " +
                              "\n--------------------------------------");

            Console.WriteLine("Zadej kterou operaci chceš provést: " +
                              "\n-[q] přidání nového uživatele " +
                              "\n-[w] odebrání uživatele " +
                              "\n-[e] přidat platbu " +
                              "\n-[r] zobrazit dluh \n");


            var input = Console.ReadKey();
            Console.WriteLine("\n");
            switch (input.Key)
            {
                case ConsoleKey.Q:
                    Console.WriteLine("Zadej jméno nového uživatele: ");
                    var newUser = Console.ReadLine();
                    Users.Add(new User(newUser));
                    // Console.WriteLine($"\n Byl přidán uživatel \"{newUser}\"");
                    foreach (var user in Users)
                    {
                        Console.WriteLine(user.Uid);
                        Console.WriteLine(user.Name);
                    }

                    break;

                case ConsoleKey.W:
                    // Console.WriteLine("Zadej jméno nového uživatele: ");
                    // var userToBeDeleted = Console.ReadLine();
                    // Users.Remove(userToBeDeleted);
                    // Console.WriteLine($"\n Byl přidán uživatel \"{userToBeDeleted}\"");
                    break;

                case ConsoleKey.E:

                    // Console.Write("Uživatelé: ");
                    // foreach (var user in Users)
                    // {
                    //     Console.Write($"{user.Name}, ");
                    // }

                    Console.Write("\nKdo platil: ");
                    User whoPaid = Users.Find(user => user.Name == Console.ReadLine());

                    Console.Write("Kolik platil: ");
                    var amountPaid = Convert.ToInt16(Console.ReadLine());
                    var amountSplit = amountPaid / Users.Count;
                    Console.WriteLine(amountSplit);
                    Console.WriteLine($"{whoPaid.Name} zaplatil {amountPaid}");
                    foreach (var user in Users)
                    {
                        if (user.Name != whoPaid.Name)
                        {
                            Console.WriteLine($"{user.Name} dluží {amountSplit} ");
                            whoPaid.addExpense(user.Name, amountSplit);
                        }
                    }

                    foreach (var item in whoPaid.expenses)
                    {
                        Console.WriteLine(item);
                    }
                    break;
            }
        }
    }

    class User
    {
        public string Name { get; set; }
        public int Uid { get; set; }
        private Random rnd = new Random();
        private HashSet<int> usedUIDs = new HashSet<int>();

        public User(string name)
        {
            Name = name;
            GenerateUID();
        }

        public Dictionary<string, int> expenses = new Dictionary<string, int>();

        public void addExpense(string whoOwns, int amountOwned)
        {
            expenses.Add(whoOwns, amountOwned);
        }

        private void GenerateUID()
        {
            for (int i = 0; i < 100; i++)
            {
                Uid = rnd.Next(100);

                if (!usedUIDs.Contains(Uid))
                {
                    usedUIDs.Add(Uid);
                    return;
                }
            }
        }
    }
}