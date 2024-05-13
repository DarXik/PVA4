using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Maturita_prep
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // while (true)
            // {
            //     Console.WriteLine("Zadej číslo pro operaci: ");
            //     var input = Console.ReadLine();
            //     if (int.TryParse(input, out var inputInt))
            //     {
            //         switch (inputInt)
            //         {
            //             case 1:
            //                 Time_1();
            //                 break;
            //             case 2:
            //                 Contains_2();
            //                 break;
            //             case 3:
            //                 Moon_Gravity_3();
            //                 break;
            //             case 4:
            //                 Array_Check_4();
            //                 break;
            //             case 5:
            //                 String_Reverse_5();
            //                 break;
            //             case 6:
            //                 Symmetric_Array_6();
            //                 break;
            //             default:
            //                 Console.WriteLine("Špatná hodnota");
            //                 break;
            //         }
            //     }
            //     else
            //     {
            //         Console.WriteLine("Špatná hodnota");
            //     }
            // }

            // Base64_7();

            // Console.WriteLine($"Číslo je {(Methods_8.EvenNumberChecker_8(7) ? "sudé" : "liché")}.");
            // Console.WriteLine($"Rozdíl dvou čísel je {Methods_8.DecimalDifference_8(3.5, -2.5)}.");
            // Console.WriteLine($"Převod stringu na int je {Methods_8.IsConvertible_8("5f")}.");
            // Methods_8.StringSwap_8("hello", "world");
            // Methods_8.StringToArray_8(["davídek", "pitrísek", "toníček", "auto"]).ToList()
            //     .ForEach(x => Console.WriteLine(x));

            // Quiz_9();
            Quadratic_Football_10(105);
        }

        public static void Time_1()
        {
            Console.WriteLine("Úkol 1 - Čas");
            var date = DateTime.Now;

            // 1. dnešní datum + den v týdnu
            var dayOfWeek = date.ToString("dddd", CultureInfo.CreateSpecificCulture("cs-CZ"));
            var todayDate = date.ToString("dd. MMMM, yyyy", CultureInfo.CreateSpecificCulture("cs-CZ"));
            Console.WriteLine("Dnes je " + dayOfWeek + ", " + todayDate + ".");

            // 2. počet dnů do konce roku
            var daysToEndOfYear = new DateTime(date.Year, 12, 31).DayOfYear - date.DayOfYear;
            Console.WriteLine("Do konce roku zbývá  " + daysToEndOfYear + " dnů.");

            // 3. den v týdnu, na který připadá Silvestr
            var dateOfSilvestr = new DateTime(date.Year, 12, 31);
            Console.WriteLine("Silvestr připadá na " + (int) dateOfSilvestr.DayOfWeek + ". den v týdnu, tedy na " +
                              dateOfSilvestr.ToString("dddd", CultureInfo.CreateSpecificCulture("cs-CZ")) + ".");
        }

        public static void Contains_2()
        {
            // Napište program, který opakovaně načte z klávesnice dva řetězce(stringy) S a T. Program vypíše, zda je string T obsažen ve stringu S.
            //     Např.: S = "Napište program bez chyby", T = "pište"
            // Program vypíše, že "pište" je obsažen ve stringu "Napište program bez chyby", včetně uvozovek.

            while (true)
            {
                Console.WriteLine("Úkol 2 - obsaženost stringů");

                Console.WriteLine("Zadej první string: ");
                var input1 = Console.ReadLine();
                Console.WriteLine("Zadej druhý string: ");
                var input2 = Console.ReadLine();
                if (input1 == null || input2 == null)
                {
                    break;
                }

                if (input1.Contains(input2))
                {
                    Console.WriteLine("String " + $"\"{input2}\"" + " je obsažen ve stringu " + $"\"{input1}\"" + ".");
                }
                else
                {
                    Console.WriteLine("String " + $"\"{input2}\"" + " není obsažen ve stringu " + $"\"{input1}\"" +
                                      ".");
                }
            }
        }

        public static void Moon_Gravity_3()
        {
            // Gravitace na Měsíci tvoří činí asi 17% zemské gravitace. Vytvořte program, do kterého uživatel zadá svou váhu na
            // Zemi a program vypočítá jeho váhu na Měsíci.

            Console.WriteLine("Úkol 3 - váha Měsíc/Země");

            Console.WriteLine("Zadej svou zemskou váhu [kg]: ");
            var input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input) && double.TryParse(input, out var inputDouble))
                // double.TryParse(input, out _ )) // _ shorthand pro výstup, který nebude použit a potřeba
            {
                var weightOnMoon = inputDouble * 0.17;
                Console.WriteLine("Tvá váha na měsíci by byla: " + weightOnMoon + " kg.");
            }
            else
            {
                Console.WriteLine("Nezadána váha!");
            }
        }

        public static void Array_Check_4()
        {
            Console.WriteLine("Úkol 4 - Kontrola pole");
            // Vytvořte program, který načte z konzole dvě pole a zkontroluje, zda jsou pole stejná. Pole jsou stejná,
            // pokud se rovnají hodnoty prvků se stejným indexem.
            //
            //     A: Zkuste vymyslet program, který dokáže poznat jestli jsou pole stejná nezávisle na indexu.
            //     B: Zkuste porovnat pole na binární úrovni.

            Console.WriteLine("Zadej první pole [i j|i,j]: ");
            var input1 = Console.ReadLine();
            Console.WriteLine("Zadej druhé pole [i j|i,j]: ");
            var input2 = Console.ReadLine();
            if (!string.IsNullOrEmpty(input1) && !string.IsNullOrEmpty(input2))
            {
                var array1 = input1.Split(' ', ',');
                var array2 = input2.Split(' ', ',');
                bool areEqual = true;

                // způsob 1
                if (array1.Length == array2.Length)
                {
                    for (int i = 0; i < array1.Length; i++)
                    {
                        if (array1[i] != array2[i])
                        {
                            areEqual = false;
                        }
                    }
                }
                else
                {
                    areEqual = false;
                }

                Console.WriteLine("ZP1: Zadaná pole " + (areEqual ? "jsou stejná" : "nejsou stejná"));

                // způsob 2
                areEqual = array1.SequenceEqual(array2);
                Console.WriteLine("ZP2: Zadaná pole " + (areEqual ? "jsou stejná" : "nejsou stejná"));

                // způsob 3
                if (array1.Length == array2.Length)
                {
                    for (int i = 0; i < array1.Length; i++)
                    {
                        areEqual = array2.Contains(array1[i]) && array1.Contains(array2[i]);
                    }
                }
                else
                {
                    areEqual = false;
                }

                Console.WriteLine("ZP3: Zadaná pole " + (areEqual ? "jsou stejná" : "nejsou stejná"));

                // binary comparison

                Array.Sort(array1);
                Array.Sort(array2);

                foreach (var item in array1)
                {
                    if (Array.BinarySearch(array2, item) < 0)
                    {
                        areEqual = false;
                    }
                }

                Console.WriteLine("BC: Zadaná pole " + (areEqual ? "jsou stejná" : "nejsou stejná"));
            }
            else
            {
                Console.WriteLine("Nezadáno pole :(");
            }
        }

        public static void String_Reverse_5()
        {
            Console.WriteLine("Úkol 5 - Reversing string");

            Console.WriteLine("Zadej string: ");
            var input = Console.ReadLine();

            char[] charArray = input?.ToCharArray();
            Array.Reverse(charArray);

            Console.WriteLine("Reversnutý string: " + new string(charArray));
        }

        public static void Symmetric_Array_6()
        {
            Console.WriteLine("Úkol 6 - Kontrola pole pro symetričnost");

            Console.WriteLine("Zadej pole: ");
            var input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input))
            {
                var inputArray = input.Split(' ', ',', '.', ';');
                if (inputArray.Length % 2 == 0)
                {
                    var firstHalf = new List<string>();
                    var secondHalf = new List<string>();
                    for (int i = 0; i < inputArray.Length / 2; i++)
                    {
                        firstHalf.Add(inputArray[i]);
                    }

                    for (int i = inputArray.Length / 2; i < inputArray.Length; i++)
                    {
                        secondHalf.Add(inputArray[i]);
                    }

                    secondHalf.Reverse();
                    if (firstHalf.SequenceEqual(secondHalf))
                    {
                        Console.WriteLine("Pole je symetrické");
                        secondHalf.Reverse();
                        var result = firstHalf.Concat(secondHalf).ToArray();
                        foreach (var item in result)
                        {
                            Console.Write(item + " ");
                        }

                        Console.WriteLine("");
                    }
                }
            }
        }

        public static void Base64_7()
        {
            Console.WriteLine("Úkol 7 - En/Dekodování base64");

            Console.WriteLine("e - encode, d - decode: ");
            var inputOption = Console.ReadLine();
            Console.WriteLine("Zadej string: ");
            var input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(inputOption))
            {
                if (inputOption == "e")
                {
                    var plaintTextBytes = System.Text.Encoding.UTF8.GetBytes(input);
                    var encodedText = System.Convert.ToBase64String(plaintTextBytes);
                    Console.WriteLine("Enkodování: " + encodedText);
                }
                else if (inputOption == "d")
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(input);
                    var decodedText = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                    Console.WriteLine("Dekodování: " + decodedText);
                }
                else
                {
                    Console.WriteLine("Neplatná operace!");
                }
            }
        }

        public static void Quiz_9()
        {
            Console.WriteLine("Úkol 9 - Kvíz");

            Console.WriteLine("Která země je největší? \na: Rusko \nb: Kanada \nc: USA \nd: Japonsko ");
            var input = Console.ReadLine();
            Console.WriteLine(input == "a" ? "Správně! " : "Nesprávná odpověď.");

            Console.WriteLine(
                "Jakým způsobem se správně deklaruje celočíselná proměnná v C#? \na: int 1x = 10; \nb: int x = 10; \nc: float x = 10.0f; \nd: string x = \"10\";");
            input = Console.ReadLine();
            Console.WriteLine(input == "b" ? "Správně! " : "Nesprávná odpověď.");
        }

        public static float Factorial(float n)
        {
            var fact = n;
            for (int i = 1; i < n; i++)
            {
                fact *= i;
            }

            return fact;
        }

        public static void Quadratic_Football_10(int n)
        {
            // var factN = Factorial(n);
            // var factN2 = Factorial(n - 2);
            // var fact2 = Factorial(2);
            // var matches= factN / (factN2 * fact2);
            //
            // Console.WriteLine("Počet utkání: " + matches);

            // nebo
            if (n > 2)
            {
                for (int i = 2; i < 100; i++)
                {
                    var maxMatches = (i * (i - 1) / 2);
                    if (n < maxMatches)
                    {
                        Console.WriteLine("Počet týmů pro " + n + " utkání: " + (i - 1));
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("2 nebo 0");
            }

        }
    }

    static class Methods_8
    {
        public static bool EvenNumberChecker_8(int number)
        {
            return number % 2 == 0;
        }

        public static double DecimalDifference_8(double number1, double number2)
        {
            return number1 - number2;
        }

        public static int IsConvertible_8(string input)
        {
            return int.TryParse(input, out var out1) ? out1 : int.MinValue;
        }

        public static void StringSwap_8(string t, string s)
        {
            Console.WriteLine(t + s);

            (t, s) = (s, t); // swap via destruction
            Console.WriteLine("Po změně: ");
            Console.WriteLine(t + s);
        }

        public static int[] StringToArray_8(string[] s)
        {
            int[] output = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                output[i] = s[i].Length;
            }

            return output;
        }
    }
}