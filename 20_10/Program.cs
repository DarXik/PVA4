using System;

namespace _20_10
{
    internal class Program
    {
        // public object PolePerson(string[] names)
        // {
        //     Random rnd = new Random();
        //     object[] persons;
        //
        //     foreach (var VARIABLE in names)
        //     {
        //         int num = rnd.Next();
        //         Person person1 = new Person(VARIABLE, num);
        //         persons[VARIABLE] = person1;
        //     }
        //
        //     return person1;
        // }

        public static void Main(string[] args)
        {
            string[] names = {"David", "Tomáš", "Honza"};
            foreach (var VARIABLE in PoleOsob(names))
            {
                Console.Write(VARIABLE.ID);
                Console.Write(VARIABLE.Name);
            }
        }

        static Osoba[] PoleOsob(string[] names)
        {
            Osoba[] poleOsob = new Osoba[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                Osoba clovek = new Osoba(names[i]);
                poleOsob[i] = clovek;
            }

            return poleOsob;
        }
    }


    class Osoba
    {
        private Random rnd = new Random();
        public string Name { get; set; }
        public int ID { get; set; }

        public Osoba(string name)
        {
            Name = name;
            ID = rnd.Next(0, 100000);
        }
    }
    // public class Person
    // {
    //     private string name;
    //     private int id;
    //
    //     public Person(string name, int id)
    //     {
    //         this.name = name;
    //         this.id = id;
    //     }
    // }
}