// double[] pole = new double[4];
// double arrMean = 0;
// double product = 0;
// double counter = 0;
// int j = 0;
//
// for (int i = 0; i < pole.Length; i++)
// {
//     Console.WriteLine($"Zadej {i + 1} číslo:");
//     pole[i] = double.Parse(Console.ReadLine());
//     product *= pole[i];
// }
//
// while (j < pole.Length)
// {
//     counter += pole[j];
//     j++;
// }
//
// Console.WriteLine(product);
// Console.WriteLine(counter / pole.Length);


// do
// {
//     int n = int.Parse(Console.ReadLine());
//     for (int i = n; i <= n * 20; i += n)
//     {
//         if (i % n == 0)
//         {
//             Console.WriteLine(i);
//         }
//     }
// } while (true);

class MyClass
{
    public int MyMetod(int a, int b, char c)
    {
        return a + b;
    }
}