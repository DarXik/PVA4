using System;

// obyčejná třída
// var myObj = new Auto("modrá", 1998);
// myObj.Barva = "žlutá";
// Console.WriteLine(myObj.Barva);

// var myPig = new Pig(); // objekt třídy Pig
// myPig.animalSound(); // volá abs. metodu
// myPig.sleep(); // volá klasickou metodu

// var myCow = new Cow(); // objekt Cow
// myCow.animalSound();
//
// IAnimal dog = new Dog();
// dog.animalSound();

// Car1 car1 = new Car1("Bugatti");
// Car1 car2 = new Car1(car1);
// // obě by vypsaly "Bugatti"

namespace Třídy
{
    class Car1
    {
        public string brand;

        public Car1(string theBrand)
        {
            brand = theBrand;
        }

        public Car1(Car1 c1)
        {
            brand = c1.brand;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car();
            Car car2 = new Car();
            // vypíše:
            // Static Constructor
            // Default Constructor
            // Default Constructor
        }
    }

    class Car
    {
        static Car()
        {
            Console.WriteLine("Static Constructor");
        }

        public Car()
        {
            Console.WriteLine("Default Constructor");
        }
    }

    interface IAnimal
    {
        void animalSound(); // interface method (nemá tělo)
    }

    class Cow : IAnimal // "Cow" implementuje IAnimal int.
    {
        public void animalSound()
        {
            Console.WriteLine("Cow says: boo boo");
        }
    }

    class Dog : IAnimal
    {
        public void animalSound()
        {
            Console.WriteLine("Dog says: woof woof");
        }
    }

    abstract class Animal
    {
        // klasická metoda
        public void sleep()
        {
            Console.WriteLine("Zzz");
        }

        // abstraktní metoda
        public abstract void animalSound(); // nelze {} - nemůže být tělo/body
    }

    class Pig : Animal // dědí z abs. třídy
    {
        public override void animalSound()
        {
            // tělo metody animalSound() je poskytnuto až zde
            Console.WriteLine("The pig says: Wee wee");
        }
    }

    public class Auto
    {
        // datový členy třídy (proměnné)
        private int RokVyroby; // field
        private string barva; // sice private mimo třídu, ale přes properties ji přepíšeme

        // properties
        public string Barva
        {
            // stejné shorthand = { get; set; }
            get { return barva; }
            set => barva = value; // stejný zápis
        }

        // konstruktor
        public Auto(string barva, int rokVyroby)
        {
            Barva = barva;
            this.RokVyroby = rokVyroby;
        }

        // metoda
        public void PopisAutobarazu()
        {
            Console.WriteLine($"Auto barvy {Barva}, vyrobeno v roce {RokVyroby}.");
        }
    }

}

