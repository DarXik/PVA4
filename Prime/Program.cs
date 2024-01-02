Console.WriteLine(isPrime(13)); //false

bool isPrime(int number)
{
    // if (number < 2) return false; // číslo menší než 2 není pč
    for (int i = 2; i < number / 2; i++)
    {
        if (number % i == 0) return false; // pokud je číslo dělitelné i, není pč
    }

    return true; // ani jedna podmínka nesplněna -> pč
}