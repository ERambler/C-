/*
Вариант 14 Журавлёв

Человек путешествует по городам и посещает музеи. Человек от-
мечает посещенные города, музеи и время посещения. Программа 
по  указанию  временного  интервала  распечатывает  посещенные 
города  и  музеи  с  возможностью  получения  оплаченной  стоимо-
сти и длительности посещения. 
*/

using System;

namespace lab14_6
{
    class Program
    {
        static void Main(string[] args)
        {   //Берём две даты от пользователя
            Console.WriteLine("Введи первую дату [dd.mm.yy]:");
            string? date1 = Console.ReadLine();
            Console.WriteLine("Введи вторую дату [dd.mm.yy]:");
            string? date2 = Console.ReadLine();
            //добавим разделитель и слепим строки как в техзадании
            string date=date1.Insert(date1.Length,"#");
            date=date.Insert(date.Length,date2);
            //Передаём строку методу в рработу
            Console.WriteLine("\nThe Rezult is {0}\n",dateComparison(date));

        }
    }
}
