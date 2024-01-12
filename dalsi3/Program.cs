using System;

namespace dalsi3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zadej počet sedadel: ");
            int pocetSedadel = Convert.ToInt32(Console.ReadLine());
            string[] sedadla = new string[pocetSedadel];
            int pocetRad = (int) Math.Sqrt(pocetSedadel);

            // Inicializace všech sedadel na volná
            for (int i = 0; i < pocetSedadel; i++)
            {
                sedadla[i] = "V";
            }

            // Vykreslení sedadel do konzole
            VykreslitSedadla(sedadla);

            // Uživatel může měnit stav jednotlivých sedadel pomocí souřadnic
            while (true)
            {
                Console.WriteLine("Zadejte souřadnice sedadla (řada, sloupec):");
                string[] souradnice = Console.ReadLine().Split(',');

                int rada = int.Parse(souradnice[0]) - 1;
                int sloupec = int.Parse(souradnice[1]) - 1;

                if (rada >= 0 && rada < pocetRad && sloupec >= 0 && sloupec < pocetRad)
                {
                    Console.WriteLine("Zadejte nový stav sedadla (O - obsazeno, R - rezervováno, V - volno):");
                    string stav = Console.ReadLine().ToUpper();

                    if (stav == "O" || stav == "R" || stav == "V")
                    {
                        sedadla[rada * pocetRad + sloupec] = stav;
                        VykreslitSedadla(sedadla);
                    }
                    else
                    {
                        Console.WriteLine("Neplatný stav sedadla!");
                    }
                }
                else
                {
                    Console.WriteLine("Neplatné souřadnice sedadla!");
                }
            }
        }

        static void VykreslitSedadla(string[] sedadla)
        {
            int pocetSedadel = sedadla.Length;
            int pocetRad = (int) Math.Sqrt(pocetSedadel);

            Console.WriteLine("Stav sedadel:");

            for (int rada = 0; rada < pocetRad; rada++)
            {
                for (int sloupec = 0; sloupec < pocetRad; sloupec++)
                {
                    Console.Write(sedadla[rada * pocetRad + sloupec] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}