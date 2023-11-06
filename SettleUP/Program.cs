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
                new User("B")
            };

            void showUsers()
            {
                Console.WriteLine("\nCurrent users: ");
                foreach (var user in users)
                {
                    Console.WriteLine($"-{user.Name}");
                }
            }

            Console.WriteLine("Settle up your expenses " +
                              "\n--------------------------------------");

            Console.WriteLine("Choose a service: " +
                              "\n-[q] add new user " +
                              "\n-[w] delete existing user " +
                              "\n-[e] add transaction " +
                              "\n-[r] show users' debts" +
                              "\n-[t] print current users" +
                              "\n-[x] exit");
            while (true)
            {
                Console.Write("\nService: ");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        Console.Write("\nAdd name for new user: ");
                        users.Add(new User(Console.ReadLine()));

                        showUsers();

                        break;

                    case ConsoleKey.W:
                        showUsers();

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
                        showUsers();

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
                                user.AddExpense(whoPaid.Name, amountSplit);
                            }
                        }

                        break;

                    case ConsoleKey.R:
                        Console.WriteLine();
                        var owedAmounts = new Dictionary<string, Dictionary<string, float>>();
                        foreach (var user in users)
                        {
                            var expense1 = 0f;
                            var expense2 = 0f;
                            foreach (var user2 in users)
                            {
                                if (user != user2)
                                {
                                    if (user2.expenses.ContainsKey(user.Name))
                                    {
                                        expense1 = user2.expenses[user.Name];
                                    }
                                    else
                                    {
                                        expense1 = 0; // this was crucial
                                    }

                                    if (user.expenses.ContainsKey(user2.Name))
                                    {
                                        expense2 = user.expenses[user2.Name];
                                    }
                                    else
                                    {
                                        expense2 = 0;
                                    }

                                    var debt = expense2 - expense1;
                                    if (debt > 0)
                                    {
                                        if (!owedAmounts.ContainsKey(user.Name)) // existuje dlužník?
                                        {
                                            // založí PRÁZDNOU dictionary se jménem dlužníka
                                            owedAmounts[user.Name] = new Dictionary<string, float>();
                                        }

                                        // kolik dluží user - user.Name: key pro inner dictionary -  userovi2 - key: user2 a value exp2 - exp1
                                        owedAmounts[user.Name][user2.Name] = expense2 - expense1;
                                    }
                                }
                            }
                        }

                        if (owedAmounts.Any())
                        {
                            foreach (var debtor in owedAmounts)
                            {
                                foreach (var creditor in debtor.Value)
                                {
                                    Console.WriteLine($"{debtor.Key} owes {creditor.Key} {creditor.Value}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Neither user owes another.");
                        }

                        break;

                    case ConsoleKey.T:
                        showUsers();

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