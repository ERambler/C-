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
        static bool dateComparison(string date)
        {
            //разделим даты на две строки через split и #
             string[] dates=date.Split(new char[] {'#'});
            //сравним строки через Compare и тут же вернём значение,
            //как, вероятно, подразумевается техзаданием
            // Console.WriteLine(dates[0]+"\n");
            if (dates[0].CompareTo(dates[1])==0) return true; else return false;
            //Хотелось бы, в случае различия дат, ещё и разницу подсчитать, 
            //однако, это другая совсем уж история. 
        }
        static bool isDate(string date)
        {   //Определим, является ли строка Датой.
            //Желательно предусмотреть разные разделители, а не только "."
            //Консервированный шаблон, ибо негоже обращаться с датами как попало ;-)
            return true;
        }
    }
}
