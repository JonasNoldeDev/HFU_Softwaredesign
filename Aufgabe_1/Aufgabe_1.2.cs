public void main(args)
{
    static string[] subjects = { "Harry", "Hermine", "Ron", "Hagrid", "Snape", "Dumbledore" };
    static string[] verbs = { "braut", "liebt", "studiert", "hasst", "zaubert", "zerstört" };
    static string[] objects = { "Zaubertränke", "den Grimm", "Lupin", "Hogwards", "die Karte des Rumtreibers", "Dementoren" };

    string[] randomVerses = createRandomVerses(6);

    Console.WriteLine(subjects.toString());
    Console.WriteLine(verbs.toString());
    Console.WriteLine(objects.toString());

    Console.WriteLine();
    Console.WriteLine("...");
    Console.WriteLine();

    foreach (string verse in randomVerses)
    {
        Console.WriteLine(verse);
    }


}

private string[]; createRandomVerses(int n)
{
    string[] verses = new string[n];

    while (int i = 0; i < n; i++)
    {
        verses[i] = this.subjects[getRandomIndex(0, n)] + " " + this.verbs[getRandomIndex(0, n)] + " " + this.objects[getRandomIndex(0, n)];
    }
}

private int getRandomIndex(int min, int max)
{
    return random.next(min, max);
}