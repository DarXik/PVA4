using System.Numerics;

var rnd = new Random();
for (int i = 0; i < 1000000; i++)
{
    var big = (BigInteger) Math.Pow(10, i);
    Console.WriteLine(big);
}