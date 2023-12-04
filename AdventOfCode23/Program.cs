using System.Text.RegularExpressions;

// Day1A();
// Day1B();
Day2A();

void Day1A()
{
    string readPath = "C:\\Users\\pitra\\Desktop\\PVA4\\AdventOfCode23\\Day1B.txt";

    // using (StreamReader reader = new StreamReader(readPath))
    // {
    //     string content = reader.ReadToEnd();
    //     Console.WriteLine(content);
    // }

    var lines = File.ReadAllLines(readPath);
    var numbers = new List<int>();

    foreach (var line in lines)
    {
        string str2 = string.Empty;
        var matches = Regex.Matches(line, @"\d+");

        foreach (var match in matches)
        {
            str2 += match;
        }

        string toBeAdded = str2[0].ToString();
        // v C#8.0 -> ^1 odpovídá poslednímu elementu - reprezentuje indexy odzadu, ^2 předposlední
        toBeAdded += str2[^1].ToString();
        Console.WriteLine(toBeAdded);

        numbers.Add(Convert.ToInt32(toBeAdded));
    }

    int counter = 0;
    foreach (var item in numbers) counter += item;

    Console.WriteLine($"Day 1 A: {counter}");
}

void Day1B()
{
    string readPath = "C:\\Users\\pitra\\Desktop\\PVA4\\AdventOfCode23\\Day1B.txt";

    var lines = File.ReadAllLines(readPath);
    string[] numbers = {"1", "2", "3", "4", "5", "6", "7", "8", "9"};
    string[] words = {"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

    // var finals = new List<dynamic>();

    foreach (var line in lines)
    {
        string str1 = string.Empty;

        Console.WriteLine(line);

        if (numbers.Length == words.Length)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                string el1 = numbers[i];
                string el2 = words[i];

                if (line.Contains(el1) || line.Contains(el2))
                {
                    if (line.IndexOf(el1, StringComparison.Ordinal) != -1)
                    {
                        // if (line.IndexOf(el1) < line.IndexOf(el2))
                        // {
                        //     Console.WriteLine(el1                        );
                        // }
                    }
                    else
                    {
                        Console.WriteLine(el2);
                    }
                }
            }
        }
    }

    // int counter = 0;
    // foreach (var item in finals) counter += item;

    // Console.WriteLine($"Day 2 B: {counter}");
}

void Day2A()
{
    var readPath = "C:\\Users\\pitra\\Desktop\\PVA4\\AdventOfCode23\\Day1A.txt";

    // ReSharper disable once UnusedVariable
    var lines = File.ReadAllLines(readPath);
    var split = new Dictionary<dynamic, string[]>();

    for (var i = 0; i < lines.Length; i++)
    {


            var match = Regex.Match(lines[i], @"Game \d+:");
            if (match.Success)
            {
                int gameNum = int.Parse(match.Groups[1].Value);
                var a = lines[i].Substring(8);
                var b = Regex.Replace(a, @"\s", "");

                Console.WriteLine(gameNum);
                split.Add(gameNum, b.Split(";"));
            }

    }

    var notAllowedIDs = new List<char>();
    var allowedIDs = new List<char>();
    int gameID = 0;
    // Console.WriteLine("12 red, 13 green, 14 blue");
    foreach (var el in split)
    {
        // Console.WriteLine("\n" + el.Key + ": ");
        bool gameFound = false;
        while (!gameFound)
        {
            foreach (var value in el.Value)
            {
                // Console.WriteLine(value);
                var temp = new List<dynamic>(Regex.Matches(value, @"\d+"));

                foreach (var match in temp)
                {
                    int index = value.IndexOf(match.Value);
                    var result = value[index + 1];

                    if (int.TryParse(match.Value, out int numericValue))
                    {
                        // Console.WriteLine(numericValue);
                        if ((!(numericValue > 12) && result.ToString() != "r") ||
                            (!(numericValue > 13) && result.ToString() != "g") ||
                            (!(numericValue > 14) && result.ToString() != "b"))
                        {
                            allowedIDs.Add(el.Key);
                            gameFound = true;
                        }
                        else
                        {
                            notAllowedIDs.Add(el.Key);
                            // Console.WriteLine(value[index] + " " + value[index + 1]);
                        }
                    }
                }
            }
        }
    }

    allowedIDs.RemoveAll(item => notAllowedIDs.Contains(item));

    var hashSet = new HashSet<char>(allowedIDs);
    var finalID = 0;
    foreach (var VARIABLE in hashSet)
    {
        string temp = VARIABLE.ToString();
        finalID += Convert.ToInt32(temp);
        Console.WriteLine(VARIABLE);
    }

    Console.WriteLine(finalID);
}