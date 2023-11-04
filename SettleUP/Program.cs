using System;
using System.Collections.Generic;
using System.Linq;

namespace SettleUP
{
    internal class Program
    {
        public Program()
        {
        }

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
                              "\n-[r] show users' debts" +
                              "\n-[x] exit");
            while (true)
            {
                Console.Write("\nService: ");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        Console.Write("\nAdd name for new user: ");
                        users.Add(new User(Console.ReadLine()));

                        Console.WriteLine("Current users: ");
                        foreach (var user in users)
                        {
                            Console.WriteLine($"-{user.Name}");
                        }

                        break;

                    case ConsoleKey.W:
                        Console.WriteLine("\nCurrent users: ");
                        foreach (var user in users)
                        {
                            Console.WriteLine($"-{user.Name}");
                        }

                        Console.Write("\nType name of user to be deleted: ");
                        var userToBeDeleted = Console.ReadLine();
                        foreach (var user in users)
                        {
                            if (user.Name == userToBeDeleted)
                            {
                                users.Remove(user);
                                Console.WriteLine($"User \"{user.Name}\" was removed.");
                                break;
                            }
                        }

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

                        Console.Write("Amount: ");
                        var amountPaid = Convert.ToSingle(Console.ReadLine());
                        var amountSplit = amountPaid / users.Count;

                        Console.WriteLine($"{whoPaid.Name} paid {amountPaid} ({amountSplit})");

                        foreach (var user in users)
                        {
                            if (user.Name != whoPaid.Name)
                            {
                                Console.WriteLine($"{user.Name} owes {amountSplit} ");
                                whoPaid.AddExpense(user.Name, amountSplit);
                            }
                        }

                        break;

                    case ConsoleKey.R:
                        Console.WriteLine();
                        foreach (var user in users)
                        {
                            // Console.WriteLine($"\n{user.Name}:");
                            //
                            // if (user.expenses.Any())
                            // {
                            //     foreach (var expense in user.expenses)
                            //     {
                            //         Console.WriteLine(expense);
                            //     }
                            // }
                            // else
                            // {
                            //     Console.WriteLine($"User {user.Name} didn't pay for anything.");
                            // }

                            if (user.expenses.Any())
                            {
                                foreach (var user2 in users)
                                {
                                    if (user != user2 && user.expenses.ContainsKey(user2.Name) &&
                                        user2.expenses.ContainsKey(user.Name))
                                    {
                                        var expense1 = user.expenses[user2.Name];
                                        var expense2 = user2.expenses[user.Name];
                                        // Console.WriteLine(Math.Abs(expense1 - expense2));
                                        if (expense1 > expense2)
                                        {
                                            Console.WriteLine(
                                                $"1: {user2.Name} owes {user.Name} {expense1 - expense2}");
                                        }
                                        else if (expense1 < expense2)
                                        {
                                            Console.WriteLine(
                                                $"2: {user.Name} owes {user2.Name} {expense2 - expense1}");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{user.Name} and {user2.Name} are squared.");
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }

                        break;

                    case ConsoleKey.X:
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
        // private int Uid { get; set; }
        // private Random rnd = new Random();
        // private HashSet<int> usedUIDs = new HashSet<int>();

        public User(string name)
        {
            Name = name;
            // GenerateUID();
        }

        public readonly Dictionary<string, float> expenses = new Dictionary<string, float>();

        public void AddExpense(string whoOwns, float amountOwned)
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

        // private void GenerateUID()
        // {
        //     for (int i = 0; i < 100; i++)
        //     {
        //         Uid = rnd.Next(100);
        //
        //         if (!usedUIDs.Contains(Uid))
        //         {
        //             usedUIDs.Add(Uid);
        //             return;
        //         }
        //     }
        // }
    }
}