namespace _6_10
{
    internal class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    class Food
    {
        protected float fat;
        protected float carbs;
        protected float protein;
        protected float weight;
        protected string name;

        public string Name  //vlastnost
        {
            get { return name; }
        }

        public Food(string name, float weight) //konstruktor
        {
            this.name = name; //proměnná třídy na proměnnou konstruktoru
            this.weight = weight;
        }

        public float GetEnergyCal() //metoda třídy
        {
            return carbs * 4 + fat * 9 + protein * 4;
        }
    }

    class Meat : Food //meat dědí od food
    {
        public Meat(float weight) : base("Meat", weight) //konstruktor meatu, který bere z konstruktoru foodu
        {
            protein = weight * 0.2f;
            fat = weight * 0.1f;
            carbs = weight * 0.05f;
        }
    }
}