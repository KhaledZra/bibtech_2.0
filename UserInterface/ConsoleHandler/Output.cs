namespace UserInterface.ConsoleHandler;

public static class Output
{
    public static void StartMenu()
    {
        Console.WriteLine("Welcome to Bibtech 2.0!");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Quit");
        Console.Write("Choice: ");
    }
}