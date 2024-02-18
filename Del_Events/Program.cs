using System;
using System.Collections.Generic;

namespace Del_Events
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Seznam seznamObj = new Seznam();

            seznamObj.EventAdd += Pridej; //připojíme k události EventHandler (delegát nám ho přenese k události)
            seznamObj.EventRemove += Odeber; //připojíme k události EventHandler (delegát nám ho přenese k události)

            seznamObj.Add("karel"); //Protože jsme zavolali metodu Add vyvolá se událost a ta zavolá EventHandler Pridej
            seznamObj.Remove("karel"); //Protože jsme zavolali metodu Remove vyvolá se událost a ta zavolá EventHandler Odeber

            seznamObj.Add("michal");
        }

        static void Pridej(string jmeno) //EventHanlder - metoda která se stane, když se vyvolá událost
        {
            Console.WriteLine(jmeno + " bylo přídáno");
        }

        static void Odeber(string jmeno) //EventHanlder - metoda která se stane, když se vyvolá událost
        {
            Console.WriteLine(jmeno + " bylo odebráno");
        }
    }

    public delegate void AddHandler(string jmeno); //Delegát - přenáší jako argument celou metodu (Event Handler), musí mít jako argument vše co metoda ve které se spouští

    public delegate void RemoveHandler(string jmeno); //Delegát - přenáší jako argument celou metodu (Event Handler), musí mít jako argument vše co metoda ve které se spouští

    class Seznam
    {
        public event AddHandler EventAdd; //Event je nějaká událost, která se někdy stane v průběhu toku programem
        public event RemoveHandler EventRemove;

        private List<string> seznamJmen = new List<string>();

        public void Add(string jmeno)
        {
            seznamJmen.Add(jmeno);
            EventAdd?.Invoke(jmeno); //Tady se event vyvolá - musí obsahovat stejný argument jako metoda
        }

        public void Remove(string jmeno)
        {
            seznamJmen.Remove(jmeno);
            EventRemove?.Invoke(jmeno); //Tady se event vyvolá - musí obsahovat stejný argument jako metoda
        }
    }
}