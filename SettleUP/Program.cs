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
            var users = new List<User>
            {
                new User("A"),
                new User("B"),
                new User("C"),
                new User("D")
            };

            Console.WriteLine("Settle up your expenses " +
                              "\n--------------------------------------");

            Console.WriteLine("Choose a service: " +
                              "\n-[q] add new user " +
                              "\n-[w] delete existing user " +
                              "\n-[e] add transaction " +
                              "\n-[r] show users' debts \n");


            var inputService = Console.ReadKey();
            Console.WriteLine("\n");

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case ConsoleKey.Q:
                        // Console.WriteLine("Add name for new user: ");
                        // var newUser = Console.ReadLine();
                        // users.Add(new User(newUser));
                        // // Console.WriteLine($"\n New user was added \"{newUser}\"");
                        // foreach (var user in users)
                        // {
                        //     Console.WriteLine(user.Uid);
                        //     Console.WriteLine(user.Name);
                        // }

                        break;

                    case ConsoleKey.W:
                        break;

                    case ConsoleKey.E:
                        Console.Write("\nWho paid: ");
                        var userInput = Console.ReadLine();
                        // User whoPaid = users.Find(user => user.Name == Console.ReadLine());
                        User whoPaid = null;

                        foreach (User user in users)
                        {
                            if (user.Name == userInput)
                            {
                                whoPaid = user;
                                break;
                            }
                        }

                        Console.Write("How much was paid: ");
                        var amountPaid = Convert.ToInt16(Console.ReadLine());
                        var amountSplit = amountPaid / users.Count;


                        Console.WriteLine($"{whoPaid.Name} paid {amountPaid}");

                        foreach (var user in users)
                        {
                            if (user.Name != whoPaid.Name)
                            {
                                Console.WriteLine($"{user.Name} owns {amountSplit} ");
                                whoPaid.AddExpense(user.Name, amountSplit);
                            }
                        }

                        break;

                    case ConsoleKey.X:
                        foreach (var VARIABLE in users)
                        {
                            Console.WriteLine($"{VARIABLE} paid: ");
                            foreach (var item in VARIABLE.expenses)
                            {
                                Console.WriteLine(item);
                            }
                        }

                        return;
                }

                // Console.ReadKey();
                // Console.Clear();
            }
        }
    }

    class User
    {
        public string Name { get; set; }
        private int Uid { get; set; }
        private Random rnd = new Random();
        private HashSet<int> usedUIDs = new HashSet<int>();

        public User(string name)
        {
            Name = name;
            GenerateUID();
        }

        public Dictionary<string, int> expenses = new Dictionary<string, int>();

        public void AddExpense(string whoOwns, int amountOwned)
        {
            if (!expenses.ContainsKey(whoOwns))
            {
                expenses.Add(whoOwns, amountOwned);
            }
            else
            {
                expenses[whoOwns] += amountOwned;
            }
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