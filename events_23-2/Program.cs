using System;

namespace events_23_2
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once InconsistentNaming

    public delegate void eventHandler();

    public delegate void eventHandler2(bool isOn);

    internal class Program
    {
        public static event eventHandler IsShortCircuited;
        public static bool IsOn = false;

        public static void Main(string[] args)
        {
            var bulb = new Lightbulb();
            bulb.SwitchedState += SimulateShortCircuit;
            IsShortCircuited += bulb.BulbShortCircuit;

            while (true)
            {
                Console.WriteLine("Zadejte příkaz (on/off):");
                var command = Console.ReadLine()?.ToLower();

                if (command == "on")
                {
                    if (IsOn)
                    {
                        Console.WriteLine("Žárovka svítí :)");
                    }
                    else
                    {
                        IsOn = bulb.TurnOn();
                    }
                }
                else if (command == "off")
                {
                    if (!IsOn)
                    {
                        Console.WriteLine("Žárovka nesvítí :(");
                    }
                    else
                    {
                        IsOn = bulb.TurnOff();
                    }
                }
                else
                {
                    Console.WriteLine("Neplatný příkaz.");
                }
            }
        }

        private static void SimulateShortCircuit(bool bulbState)
        {
            Console.WriteLine(bulbState ? "Žárovka je zapnutá." : "Žárovka je vypnutá.");

            var random = new Random();
            var randomNumber = random.Next(1, 10);

            if (randomNumber == 5)
            {
                IsShortCircuited?.Invoke();
            }
        }
    }

    class Lightbulb
    {
        private bool isOn;
        public event eventHandler2 SwitchedState;

        public Lightbulb()
        {
            isOn = false;
        }

        public void BulbShortCircuit()
        {
            Console.WriteLine("Žárovka praskla! ://");
            Console.WriteLine("Tak ji prosím nejdříve vypni: ");
            var input = Console.ReadLine();
            while (input != "off")
            {
                Console.WriteLine("Nejdříve ji vypni!!! [off]");
                input = Console.ReadLine();
            }

            Program.IsOn = false;
        }

        public bool TurnOn()
        {
            isOn = true;
            SwitchedState?.Invoke(isOn);
            return isOn;
        }

        public bool TurnOff()
        {
            isOn = false;
            SwitchedState?.Invoke(isOn);
            return isOn;
        }
    }
}