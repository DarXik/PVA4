using System.Text.RegularExpressions;

string readPath = "C:\\Users\\pitra\\Desktop\\Regex.txt";



if (!File.Exists(readPath))
{
    Console.WriteLine("Soubor neexistuje");
}

MatchCollection match;

using (StreamReader reader  = new StreamReader(readPath))
{
    string regex = reader.ReadToEnd();
    Console.WriteLine($"původní text: {regex}");
    Console.WriteLine("-------------------------------------");

    string email = @"([A-Za-z0-9-]*?\@[A-Za-z0-9-]*?\.[a-z]{2,10})|(\d+\.[0-9]{0,2})|(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})|((https|http)\:\/\/([A-Za-z]*|[w]{3})\.[A-Za-z]*\.[A-Za-z]{2,})|(\#+?[a-z]*)";
    string digits = @"\d+\.[0-9]{0,2}";
    string ip = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";
    string phone = @"(\d{1,3})(\s|\-)(\d{1,3})(\s|\-)(\d{1,3})";
    string url = @"(https|http)\:\/\/([A-Za-z]*|[w]{3})\.[A-Za-z]*\.[A-Za-z]{2,}";
    string hastag = @"\#+?[a-z]*";
    string endingLy = @"\b[A-Z]{1}\w*ly\b";

    Regex rx = new Regex(email);

    match = rx.Matches(regex);

    foreach (Match item in match)
    {
        for (int i = 0; i < item.Groups.Count; i++)
        {
            Console.WriteLine(item.Groups[i]);
        }

        Console.WriteLine(item);
    }

    // Console.WriteLine(Regex.Replace(regex, ip, "xxx.xxx.xxx.xxx"));
}

using (StreamWriter writer = new StreamWriter("C:\\Users\\pitra\\Desktop\\Regex1.txt"))
{
    writer.WriteLine(match[0]);
    writer.Flush();
}

Console.ReadKey();