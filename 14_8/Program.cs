/*
Вариант 14.
Задача 8.1

Необходимо выполнить следующие операции с перечислениями:
1) описать перечисление согласно варианту (табл. 1);
2) объявить переменную перечисляемого типа данных;
3) инициализировать переменную значением с клавиатуры;
4) вывести все значения перечисляемого типа данных на экран
при этом введенное с клавиатуры значение подсветить другим цветом.*/
static class Program
{
    private enum Geners
    {
        Боевик = 1,
        Вестерн,
        Гангстерский,
        Детектив,
        Драма,
        Исторический,
        Комедия,
        Сказка,
        Трагедия,
        Мелодрама,
        Музыкальный,
        Нуар,
        Политический,
        Приключенческий,
        Трагикомедия        
    }
    private static void DisplayEnum (Geners gen)
    {
        ConsoleColor oldColor = Console.BackgroundColor;
        foreach (int i in Enum.GetValues(typeof(Geners))) 
            {
                if ((int) gen == i) Console.BackgroundColor=ConsoleColor.DarkGray;
                Console.Write($"{i} - {Enum.GetName(typeof(Geners), i)}");
                if ((int) gen == i) Console.BackgroundColor=oldColor;
                Console.Write("\n");
            }
    }
    public static void Main ()
    {
        Geners gen;
        Console.Clear();
        Console.WriteLine("Имеются следующие жанры:");
        DisplayEnum (0);
        Console.Write("\nНеобходимо выбраь один из них по-номеру: ");

        try
        {
            gen = (Geners) int.Parse (Console.ReadLine() ?? string.Empty);
            if ((int) gen < 0 || (int) gen > Enum.GetValues(typeof(Geners)).Length) throw  new IndexOutOfRangeException();
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine ("Исключительная ситуация: значение вне допустимого диапазона.");
            return;
        }
        catch (FormatException)
        {
            Console.WriteLine ("Исключительная ситуация: введена пустая строка, либо нераспознаваемые символы.");
            return;            
        }
    
        Console.Clear();
        DisplayEnum (gen);

        Console.ReadLine();

    }
}
