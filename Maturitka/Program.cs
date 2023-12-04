﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Maturitka
{
    internal class Program
    {


        public static void Main(string[] args)
        {
            // IsPrime();
            // FileManipulation();
            // RndWithoutRep();
            RndString();
        }

        private static void IsPrime()
        {
            Console.WriteLine("Enter minimum: ");
            var min = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Enter maximum: ");
            var max = Convert.ToInt16(Console.ReadLine());


            for (var num = min; num <= max; num++) // iteruje mezi min a max
            {
                var ctr = 0; // počítá dělitele num

                for (var j = 2; j <= num / 2; j++) // kontroluje všechny čísla od 2 do poloviny num (větší nemůžou být dělitel)
                {
                    if (num % j == 0)
                    {
                        // pokud je num dělitelné beze zbytku j, zvětší se ctr a break - není pč
                        ctr++;
                        break;
                    }
                }

                if (ctr == 0 && num != 1) // pokud ctr zůstane 0 (nic krom 1 a num samotného) a num není 1 (není pč)
                {
                    Console.WriteLine(num); // vypíše pč
                }
            }
        }

        private static void FileManipulation()
        {
            // čtení texťáku
            string readPath = "C:\\Users\\pitra\\Desktop\\PVA4\\Maturitka\\NewFile1.txt";
            // // string readPath = "C:/Users/pitra/Desktop/Maturitka C#/XOR/NewFile1.txt";

            // using aby se souborové zdroje uvolnili po dokončení práce s nimi, také zachycují potencionální výjimky
            using (StreamReader reader = new StreamReader(readPath))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine(content); // obsah souboru
            }

            // kontrola zdali soubor existuje
            if (File.Exists(readPath))
            {
            }
            else
            {
            }

            // zápis do texťáku
            string writePath = "C:\\Users\\pitra\\Desktop\\PVA4\\Maturitka\\NewFile1.txt";
            using (StreamWriter writer = new StreamWriter(writePath))
            {
                writer.WriteLine("První řádek");
                writer.Flush(); // takes care of emptying the buffer
            }

            Console.WriteLine("text byl zapsán");

            // zápis do existujícího souboru
            using (StreamWriter appendWriter = new StreamWriter(writePath, true))
            {
                appendWriter.WriteLine("Další řádek");
            }

            Console.WriteLine("Další text byl zapsán");
        }

        private static void RndWithoutRep()
        {
            // Random random = new Random();
            // List<int> usedNumbers = new List<int>(); // seznam vygen. čísel
            // int min = 1;
            // int max = 100;
            //
            // // Generování náhodných čísel bez opakování
            // for (int i = 0; i < 50; i++)
            // {
            //     int randomNumber;
            //
            //     do
            //     {
            //         randomNumber = random.Next(min, max + 1);
            //     }
            //     while (usedNumbers.Contains(randomNumber));
            //
            //     usedNumbers.Add(randomNumber);
            // }
            //
            // foreach (var VARIABLE in usedNumbers)
            // {
            //     Console.WriteLine(VARIABLE);
            // }

            Random random = new Random();
            var pole = new int[10];

            for (int i = 0; i < pole.Length;)
            {
                int randomNumber = random.Next(1, 11);

                if (!pole.Contains(randomNumber))
                {
                    pole[i] = randomNumber;
                    i++;
                }
            }

            foreach (var VARIABLE in pole)
            {
                Console.WriteLine(VARIABLE);
            }
        }

        private static void RndString()
        {
            var str = new StringBuilder();
            var rnd = new Random();

            char letter;

            for (int i = 0; i < 7; i++)
            {
                double flt = rnd.NextDouble(); // od 0.0 do 1.0
                int shift = Convert.ToInt32(Math.Floor(25 * flt)); // menší nebo rovno

                letter = Convert.ToChar(shift + 65);
                str.Append(letter);
            }

            Console.WriteLine(str.ToString());
        }
    }
}