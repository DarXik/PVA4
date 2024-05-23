
void isPrime()
{
    Console.Write("Give me range min: ");
    int min = int.Parse(Console.ReadLine());

    Console.Write("Give me range max: ");
    int max = int.Parse(Console.ReadLine());

    // int[] range = (input.Split(",")).Select(int.Parse).ToArray();
    var primes = new List<int> { };


    for (int i = min; i <= max; i++)
    {
        bool isPrime = true;
        for (int j = 2; j <= i / 2; j++)
        {
            if (i % j == 0 && i != 2 && i != 1 && i != 0)
            {
                isPrime = false;
                break;
            }
        }

        if (isPrime)
        {
            primes.Add(i);
        }
    }

    foreach (var item in primes)
    {
        Console.WriteLine(item);
    }

}


void ErathostenSieve()
{
    Console.Write("Give me max: ");
    int max = int.Parse(Console.ReadLine());

    var numbers = new int[max + 1];

    for (int i = 2; i < numbers.Length; i++)
    {
        numbers[i] = i;
    }

    int min = 2;
    while ((min * min) <= max)
    {
        Console.WriteLine(min * min);
        if (numbers[min] != 0)
        {
            for (int i = 2; i < max; i++)
            {
                if (numbers[min] * i > max)
                {
                    break;
                }
                else
                {
                    numbers[numbers[min] * i] = 0;
                }
            }
        }

        min++;
    }

    for (int i = 0; i < numbers.Length; i++)
    {
        if (numbers[i] != 0)
        {
            Console.WriteLine(i);
        }
    }
}


void Policka()
{
    int[] matrix = new int[2];

    matrix[0] = 1;
    matrix[1] = 2;

    int[,] matrix2 = new int[2, 4] { { 1, 2, 1, 3 }, { 3, 4, 3, 4 } };

    Console.WriteLine(matrix[1]);
    Console.WriteLine(matrix2[1, 0]);

    int[][] matrix3 = new int[][] { new int[2], new int[] { 103, 3030, 20 } };

    matrix3[0][0] = 10;

    Console.WriteLine(matrix3[1][0]);
}

void FrequencyAnalysis()
{
    Console.WriteLine("Zadej string: ");
    string input = Console.ReadLine();

    input = input.Replace(" ", "").ToLower();

    var chars = new List<char> { };
    var frequency = new Dictionary<char, float> { };

    foreach (var item in input)
    {
        chars.Add(item);
    }

    foreach (var item in chars)
    {
        if (frequency.ContainsKey(item))
        {
            frequency[item] += 1;
        }
        else
        {
            frequency.Add(item, 1);
        }
    }

    frequency = frequency.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

    foreach (var item in frequency)
    {
        Console.WriteLine($"{item.Key}:{item.Value}");
    }
}

FrequencyAnalysis();

Console.ReadKey();