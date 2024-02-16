using System;
using System.Collections.Generic;

namespace Del_Events
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Seznam iSeznam = new Seznam();

            iSeznam.EventAdd += new AddHandler(Pridej); //připojíme k události EventHandler (delegát nám ho přenese k události)
            iSeznam.EventRemove += new RemoveHandler(Odeber); //připojíme k události EventHandler (delegát nám ho přenese k události)

            iSeznam.Add("karel"); //Protože jsme zavolali metodu Add vyvolá se událost a ta zavolá EventHandler Pridej
            iSeznam.Remove("karel"); //Protože jsme zavolali metodu Remove vyvolá se událost a ta zavolá EventHandler Odeber

            ///


            iSeznam.Add("michal");
        }

        static void Pridej(string jmeno)//EventHanlder - metoda která se stane, když se vyvolá událost
        {
            Console.WriteLine(jmeno + " bylo přídáno");
        }
        static void Odeber(string jmeno)//EventHanlder - metoda která se stane, když se vyvolá událost
        {
            Console.WriteLine(jmeno + " bylo odebráno");
        }
    }
    public delegate void AddHandler(string jmeno); //Delegát - přenáší jako argument celou metodu (Event Handler), musí mít jako argument vše co metoda ve které se spouští
    public delegate void RemoveHandler(string jmeno);  //Delegát - přenáší jako argument celou metodu (Event Handler), musí mít jako argument vše co metoda ve které se spouští
    class Seznam
    {
        public event AddHandler EventAdd; //Event je nějaká událost, která se někdy stane v průběhu toku programem
        public event RemoveHandler EventRemove;

        private List<string> seznam = new List<string>();
        public void Add(string jmeno)
        {
            seznam.Add(jmeno);
            EventAdd(jmeno); //Tady se event vyvolá - musí obsahovat stejný argument jako metoda
        }
        public void Remove(string jmeno)
        {
            seznam.Remove(jmeno);
            EventRemove(jmeno); //Tady se event vyvolá - musí obsahovat stejný argument jako metoda
        }
    }
}