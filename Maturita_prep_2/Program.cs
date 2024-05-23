using System;
using System.Collections.Generic;
using System.Linq;

namespace Maturita_prep_2
{
    public enum Relation
    {
        Family,
        Friend,
        Colleague
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var Contacts = new List<Contact> { };

            var person1 = new Contact("David", "730584006", Relation.Family);
            var person2 = new Contact("Zbyněk", "702556190", (Relation)1);

            int a = 5;
            Console.WriteLine($"{a >= 10}");
            a++;
            a *= 5;

            Console.WriteLine(a);
            bool x = true;
            bool y = false;

            Console.WriteLine($"{x && y}");
            Console.WriteLine($"{x || y}");
            Console.WriteLine($"{x && !y}");

            int p = 100;
            int q = 1;

            Console.WriteLine($"{p & q}");
            Console.WriteLine($"{Convert.ToString(q, 2)}{Convert.ToString(q & p, 2)}");

            p >>= 2;
            Console.WriteLine(p);

            Console.WriteLine(x? "Je pravda" : "Není pravda");

            Func<int, int, int> add = (num1, num2) => num1 + num2;
            Console.WriteLine(add(5, 10));

            List<int> numbers = new List<int> { 1, 2, 3 };
            Console.WriteLine(string.Join(", ", numbers.Where(b => b % 2 == 0)));

            Action greet = () => Console.WriteLine("Ahoj");
            greet();

            Contacts.Add(person1);
            Contacts.Add(person2);

            Contacts.Add(AddContact());
            ShowContacts(Contacts);

            SortContacts(Contacts);
            ShowContacts(Contacts);
        }
        static void ShowContacts(List<Contact> myList)
        {
            Console.WriteLine("Your contacts: \n");
            foreach (var item in myList)
            {
                Console.WriteLine($"{item.Name} - {item.RelationShip}: {item.TelephoneNumber} ");
            }

            Console.ReadKey();
        }

        static Contact AddContact()
        {
            Console.Write("Name: ");
            string inputName = Console.ReadLine();

            Console.Write("Tel. no.: ");
            string inputPhone = Console.ReadLine();

            Console.Write("Relation ['Friend', 'Family', 'Colleague']: ");

            Relation inputRelation;
            bool isRelation = Enum.TryParse(Console.ReadLine(), out inputRelation);

            if (!isRelation)
            {
                Console.WriteLine("Relation not found");
            }

            return new Contact(inputName, inputPhone, inputRelation);
        }

        static void SortContacts(List<Contact> myList)
        {
            Console.WriteLine("Sort by ['name', 'phone', 'relation']: ");
            string inputSort = Console.ReadLine();

            if (!String.IsNullOrEmpty(inputSort))
            {
                switch (inputSort)
                {
                    case "name":

                        myList.Sort((x, y) => x.Name.CompareTo(y.Name));
                        break;

                    case "phone":

                        for (int i = 0; i < myList.Count - 1; i++)
                        {

                            for (int j = 0; j < myList.Count - 1; j++)
                            {
                                if (int.Parse(myList[j].TelephoneNumber) > int.Parse(myList[j + 1].TelephoneNumber))
                                {
                                    var temp = myList[j];
                                    myList[j] = myList[j + 1];
                                    myList[j + 1] = temp;
                                }
                            }
                        }

                        break;
                }

            }
        }

    }

    class Contact
    {
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }
        public Relation RelationShip { get; set; }

        public Contact(string name, string telephoneNumber, Relation relationShip)
        {
            Name = name;
            TelephoneNumber = telephoneNumber;
            RelationShip = relationShip;
        }
    }
}
