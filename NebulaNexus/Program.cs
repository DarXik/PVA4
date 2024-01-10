using System;
using System.Collections.Generic;

namespace NebulaNexus
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // void GetPlanetSurfaceSize(ISpaceObject p)
            // {
            //     Console.WriteLine(p.SurfaceSize);
            // }
            //
            // ISpaceObject x = new Planet("Andromeda", 11192);
            // GetPlanetSurfaceSize(x);

            var sandyPlanet = new Planet("Sandy Planet", 9748f, new SandyPlanetType(), 1000, 0.3f, 0f, true);
            Console.WriteLine(sandyPlanet.Name);
            string[] surf = ((SandyPlanetType) sandyPlanet.PlanetType).SurfaceCompositions;

            foreach (var VARIABLE in surf)
            {
                Console.WriteLine(VARIABLE);
            }
        }
    }

    public interface ISpaceObject
    {
       string Name { get; set; }
       float SurfaceSize { get; }
    }

    // public class SurfaceCompostion
    // {
    //     private readonly string[] surfArray;
    //
    //     public SurfaceCompostion(string[] array)
    //     {
    //         surfArray = array;
    //     }
    //
    //     public string[] GetArray()
    //     {
    //         return surfArray;
    //     }
    // }

    public enum PlanetTypeEnum
    {
        Sandy,
        Rocky,
        Watery,
        Gaseous,
        Icy,
        VibraniumRich
    }

    public abstract class PlanetType
    {
        public PlanetTypeEnum Type;
        public int AtmosphericPermeability;
        public float MaxTemperature;
        public float MinTemperature;
        public bool WaterAvailability;
        public readonly string[] SurfaceCompositions = new[] {"Sand", "Dirt", "Rock", "Water", "Gas", "Ice", "Vibranium"};
    }

    public class SandyPlanetType : PlanetType
    {
        public SandyPlanetType()
        {
            Type = PlanetTypeEnum.Sandy;
            AtmosphericPermeability = 1;
            MaxTemperature = 245f;
            MinTemperature = 50f;
            WaterAvailability = false;
        }
    }

    public class Planet : ISpaceObject{
        public string Name { get; set; }
        public float SurfaceSize { get; set; }
        public PlanetType PlanetType;
        public int Population;
        public float TechnologicalLevel;
        public bool IsDemocratic;
        public float MilitaryForce;



        public Planet(string name, float surfaceSize, PlanetType planetType, int population, float technologicalLevel,float militaryForce,  bool isDemocratic)
        {
            PlanetType = planetType;
            Population = population;
            TechnologicalLevel = technologicalLevel;
            IsDemocratic = isDemocratic;
            MilitaryForce = militaryForce;
            Name = name;
            SurfaceSize = surfaceSize;
        }
    }
}