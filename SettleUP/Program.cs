using System;
using System.Collections.Generic;
using System.Linq;

namespace SettleUP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var users = new List<User>
            {
                new User("A"),
                new User("B")
            };

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

                        removeUser();

                        break;

                    case ConsoleKey.E:
                        showUsers();

                        addTransaction();

                        break;

                    case ConsoleKey.R:
                        Console.WriteLine();
                        sortDebts();

                        break;

                    case ConsoleKey.T:
                        showUsers();

                        break;

                    case ConsoleKey.X:
                        return;
                }
            }

            void showUsers()
            {
                Console.WriteLine("\nCurrent users: ");
                foreach (var user in users)
                {
                    Console.WriteLine($"-{user.Name}");
                }
            }

            void addTransaction()
            {
                Console.Write("\nWho paid: ");
                var userInput = Console.ReadLine();
                User whoPaid = users.Find(user => user.Name == userInput);

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
            }

            void removeUser()
            {
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
            }

            void sortDebts()
            {
                var owedAmounts = new Dictionary<string, Dictionary<string, float>>();

                foreach (var user in users)
                {
                    var expense1 = 0f;
                    var expense2 = 0f;
                    foreach (var user2 in users)
                    {
                        if (user != user2)
                        {
                            if (user2.expenses.TryGetValue(user.Name, out var expense))
                            {
                                expense1 = expense;
                            }

                            if (user.expenses.TryGetValue(user2.Name, out var userExpense))
                            {
                                expense2 = userExpense;
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
            }
        }
    }

    class User
    {
        public string Name { get; }

        public User(string name)
        {
            Name = name;
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
    }
}