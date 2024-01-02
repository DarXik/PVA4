using System;

namespace Metody
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program(); // nestatická, je tak potřeba vytvořit instanci
            Console.WriteLine(program.AddNumbers(1, 2));

            IncreaseNum();
            Console.WriteLine(NumC);
            int numArg = 10;

            TraditionalArg(numArg);
            Console.WriteLine(numArg); // vypíše 10

            ReferenceArg(ref numArg);
            Console.WriteLine(numArg); // vypíše 11

            int outputResult; // není inicializovaná, ale musí do nít být přirazena hodnota, jinak error (příp. = 0;)
            int inputNumber = 2;

            Out outExample = new Out();
            outExample.Double(inputNumber, out outputResult); // skrze out jako výstupní parametr
            Console.WriteLine(outputResult); // vypíše 4

            MyClass.MyMethod();

        }

        public static void TraditionalArg(int x) // klasický argument
        {
            // nemá vliv na původní hodnotu x
            x = x + 1;
        }

        public static void ReferenceArg(ref int x)
        {
            // ovlivní původní/vnější hodnotu x
            x = x + 1;
        }

        // pokud má metoda úkol, který není zavislý na konkrétní instanci třídy a
        // nepotřebuje pracovat s intančními proměnnými, měla by být statická
        // instanční proměnná -> (in. pole, atribut) je proměnná, která je asociována s konkrétní instancí třídy, každá instance má své data specifická pro danou instanci,
        // hodnoty těchto proměnných mohou být různé pro každou instanci téže třídy

        public int AddNumbers(int a, int b) // návratová, nestatická metoda vracející součet
        {
            return a + b;
        }

        // statická proměnná (public přístupná všude) a metoda ->
        // lze k nim přistoupit pomocí jména třídy a netřeba vytvářet instanci třídy Program
        // jsou přidruženy k třídě a ne k její instanci
        public static int NumC;

        public static void IncreaseNum() // nenávratová metoda, jen nastaví proměnnou
        {
            NumC = 3;
        }
    }

    public class Out
    {
        public void Double(int number, out int result)
        {
            result = number * 2;
        }
    }

    public static class MyClass
    {
        private static int classVar; // Proměnná třídy (člen třídy), private - nelze k ní přistoupit mimo třídu

        public static void MyMethod()
        {
            int localVar = 10; // Lokální proměnná, nelze k ní přistoupit odjinud
        }
    }
}
