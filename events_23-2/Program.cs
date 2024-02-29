using System;

namespace events_23_2
{
    // ReSharper disable once ClassNeverInstantiated.Global
    // ReSharper disable once InconsistentNaming
    internal class Program
    {
        public static void Main(string[] args)
        {
            var bulb = new Lightbulb();

            while (true)
            {
                Console.WriteLine("Zadejte příkaz (on/off):");
                var command = Console.ReadLine()?.ToLower();

                if (command == "on")
                {
                    bulb.TurnOn();
                    Console.WriteLine($"Stav žárovky: {bulb.GetState()}");
                    bulb.SimulateShortCircuit();
                }
                else if (command == "off")
                {
                    bulb.TurnOff();
                    Console.WriteLine($"Stav žárovky: {bulb.GetState()}");
                    bulb.SimulateShortCircuit();
                }
                else
                {
                    Console.WriteLine("Neplatný příkaz.");
                }
            }
        }
    }

    public delegate void eventHandler();

    class Lightbulb
    {
        private bool isOn;
        private bool isShortCircuited;
        public event eventHandler IsShortCircuitedEventHandler;
        public event eventHandler IsOnEventHandler;

        public Lightbulb()
        {
            isOn = false;
            isShortCircuited = false;
        }

        public void TurnOn()
        {
            isOn = true;
            Console.WriteLine("Žárovka je zapnutá.");
        }

        public void TurnOff()
        {
            isOn = false;
            Console.WriteLine("Žárovka je vypnutá.");
        }

        public string GetState()
        {

            if (isShortCircuited)
            {
                return "zkrat";
            }
            else if (isOn)
            {
                IsOnEventHandler?.Invoke();

                return "svítí";
            }
            else
            {

                return "zhasnutá";
            }
        }

        public void ShortCircuited()
        {
            isShortCircuited = true;
            Console.WriteLine("Došlo ke zkratu!");
        }

        public void SimulateShortCircuit()
        {
            var random = new Random();
            var randomNumber = random.Next(1, 10); // Náhodné číslo od 1 do 9

            if (randomNumber == 5) // Zkrat nastane, pokud náhodné číslo je rovno 5
            {
                IsShortCircuitedEventHandler?.Invoke();
            }
            else
            {
                // dořešit zkrat, chci vyřešit, nejprve zhasnout žárovku, jinak zkrat zůstane
            }
        }
    }
}