

namespace ConsoleProjectCodeAcademy.Helpers;

public static class Helper
{
    public static void Print(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
