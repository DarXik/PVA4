Car car1 = new Car();
car1.Model = "Porsche 911";
car1.Year = 1979;
car1.NumberOfDoors = 3;

Motorycycle bike1 = new Motorycycle();
bike1.Model = "Suzuki";
bike1.Year = 2004;
bike1.HasSideCar = false;

car1.StartEngine();
bike1.DisplayInfo();

abstract class Vehicle
{
    public string Model;
    public int Year;

    public abstract void DisplayInfo();
    public virtual void StartEngine()
    {
        Console.WriteLine("Starting...");
    }
}

interface IDriveable
{
    void Drive();
    void Stop();
}

class Car : Vehicle, IDriveable
{
    public int NumberOfDoors;

    public override void DisplayInfo()
    {
        Console.WriteLine($"This car is {Model} made in {Year} with {NumberOfDoors} doors.");
    }

    public void Drive()
    {
        Console.WriteLine("You started driving a car.");
    }

    public void Stop()
    {
        Console.WriteLine("The car is stoped.");
    }

    public override void StartEngine()
    {
        base.StartEngine();
        Console.WriteLine("Starting the 6-litre.");
    }
}

class Motorycycle : Vehicle, IDriveable
{
    public bool HasSideCar;

    public override void DisplayInfo()
    {
        Console.WriteLine($"This bike is {Model} made in {Year} {(HasSideCar ? "and has sidecar" : "and does not have sidecar")}.");
    }

    public void Drive()
    {
        Console.WriteLine("You started driving a bike.");
    }

    public void Stop()
    {
        Console.WriteLine("The bike is stoped.");
    }
}