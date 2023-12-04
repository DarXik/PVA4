// void isPrime()
// {
//     Console.WriteLine("Enter minimum: ");
//     var min = Convert.ToInt16(Console.ReadLine());
//
//     Console.WriteLine("Enter maximum: ");
//     var max = Convert.ToInt16(Console.ReadLine());
//
//
//     for (var num = min; num <= max; num++) // iteruje mezi min a max
//     {
//         var ctr = 0; // počítá dělitele num
//
//         for (var j = 2; j <= num / 2; j++) // kontroluje všechny čísla od 2 do poloviny num (větší nemůžou být dělitel)
//         {
//             if (num % j == 0)
//             {
//                 // pokud je num dělitelné beze zbytku j, zvětší se ctr a break - není pč
//                 ctr++;
//                 break;
//             }
//         }
//
//         if (ctr == 0 && num != 1) // pokud ctr zůstane 0 (nic krom 1 a num samotného) a num není 1 (není pč)
//         {
//             Console.WriteLine(num); // vypíše pč
//         }
//     }
// }
//
// void fileManipulation()
// {
//     // čtení texťáku
//     string readPath = "C:\\Users\\pitra\\Desktop\\Maturitka C#\\XOR\\NewFile1.txt";
//     // string readPath = "C:/Users/pitra/Desktop/Maturitka C#/XOR/NewFile1.txt";
//
//     // using aby se souborové zdroje uvolnili po dokončení práce s nimi, také zachycují potencionální výjimky
//     using (StreamReader reader = new StreamReader(readPath))
//     {
//         string content = reader.ReadToEnd();
//         Console.WriteLine(content); // obsah souboru
//     }
//
//     // kontrola zdali soubor existuje
//     if (File.Exists(readPath))
//     {
//     }
//     else
//     {
//     }
//
//     // zápis do texťáku
//     string writePath = String.Format(@"{0}\type1.txt", Application.StartupPath);
//     using (StreamWriter writer = new StreamWriter(writePath))
//     {
//         writer.WriteLine("První řádek");
//         writer.Flush(); // takes care of emptying the buffer
//     }
//
//     Console.WriteLine("text byl zapsán");
//
//     // zápis do existujícího souboru
//     using (StreamWriter appendWriter = new StreamWriter(writePath, true))
//     {
//         appendWriter.WriteLine("Další řádek");
//     }
//
//     Console.WriteLine("Další text byl zapsán");
// }
//
// fileManipulation();



