/*
Создать метод, который сравнивает две даты, переданные в стро-
ковой  переменной,  и  результат  сравнения  выдает  в  имени  функ-
ции.
*/

using System;

namespace lab14_6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введи первую дату [dd.mm.yy]:");
            string? date1 = Console.ReadLine();
            Console.WriteLine("Введи вторую дату [dd.mm.yy]:");
            string? date2 = Console.ReadLine();
            Console.WriteLine("main{0}\n",dateComparison(date1));

        }
        static bool dateComparison(string date)
        {
             Console.WriteLine(date+"\n");
             return true;
        }
        static bool isDate(string date)
        {
             return true;
        }
    }
}
