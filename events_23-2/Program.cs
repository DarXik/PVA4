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
            bulb.IsShortCircuitedEventHandler += () => Console.WriteLine("Zkrat nastal.");
            bulb.IsOnEventHandler += () => Console.WriteLine("Žárovka je zapnutá.");
            while (true)
            {
                Console.WriteLine("Zadejte příkaz (on/off):");
                var command = Console.ReadLine()?.ToLower();

                if (command == "on")
                {
                    bulb.TurnOn();
                    bulb.SimulateShortCircuit();
                }
                else if (command == "off")
                {
                    bulb.TurnOff();
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
        public event eventHandler IsShortCircuitedEventHandler;
        public event eventHandler IsOnEventHandler;

        public Lightbulb()
        {
            isOn = false;
        }

        public void TurnOn()
        {
            isOn = true;
            IsOnEventHandler?.Invoke();
        }

        public void TurnOff()
        {
            isOn = false;
            Console.WriteLine("Žárovka je vypnutá.");
        }

        public void SimulateShortCircuit()
        {
            var random = new Random();
            var randomNumber = random.Next(1, 10);

            if (randomNumber == 5)
            {
                IsShortCircuitedEventHandler?.Invoke();
            }
        }
    }
}