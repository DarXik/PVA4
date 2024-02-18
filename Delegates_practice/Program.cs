using System;
using System.Collections.Generic;
using System.Threading;

namespace Delegates_practice
{
    public delegate void myDelegate();
    public delegate int MathOperator(int x, int y); // odkazuje na metody které berou 2 int parametry a vracejí int

    public delegate void methodHolder();

    Action<object> david;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var obj1 = new MathOp();
            obj1.myEvent += ShowDiff;
            // obj1.myEvent += () => Console.WriteLine("Diff has changed");

            // for (int i = 0; i < 4; i++)
            // {
            //     Console.WriteLine("Result: " + obj1.Add(1, i));
            //     Thread.Sleep(1000);
            // }

            Console.WriteLine("Substract: " + obj1.Subtract(1, 100));

            MathOperator multiplyDelegate = Multiply; // vytvoří instance s odkazem na delegáty

            int result = multiplyDelegate(1, 2); // použije delegáta pro zavolaní dané metody Add(1, 2)

            Console.WriteLine("Result3: " + result);

            PerformOperation(multiplyDelegate, 10, 4);


            List<methodHolder> list = new List<methodHolder>();
            methodHolder method1 = Method1;
            methodHolder method2 = Method2;
            methodHolder method3 = Method3;
            methodHolder method4 = Method4;

            list.Add(method1);
            list.Add(method2);
            list.Add(method3);
            list.Add(method4);

            int input = Convert.ToInt32(Console.ReadLine()) - 1;

            list[input].Invoke();
        }

        private static void Method1()
        {
            Console.WriteLine("Method1 has been called");
        }
        private static void Method2()
        {
            Console.WriteLine("Method2 has been called");
        }
        private static void Method3()
        {
            Console.WriteLine("Method3 has been called");
        }
        private static void Method4()
        {
            Console.WriteLine("Method4 has been called");
        }


        private static void PerformOperation(MathOperator operation, int x, int y)
        {
            int result = operation(x, y);
            Console.WriteLine("Result4: " + result);
        }

        private static void ShowDiff()
        {
            Console.WriteLine("--Event has happened--");
        }

        private static int Multiply(int x, int y)
        {
            return x * y;
        }
    }

    class MathOp
    {
        public event myDelegate myEvent;

        public int Add(int x, int y)
        {
            myEvent?.Invoke();
            return x + y;
        }

        public int Subtract(int x, int y)
        {
            myEvent?.Invoke();
            return x - y;
        }
    }
}