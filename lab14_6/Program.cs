/*
Вариант 14 Журавлёв
Создать метод, который сравнивает две даты, переданные в стро-
ковой  переменной,  и  результат  сравнения  выдает  в  имени  функ-
ции.(??? не совсем понятно, что это означает)

Дополнительная задача: 
ввод формата пользователем
DateTime не использовать
*/
using System;
using System.Linq;
namespace lab14_6
{
    class Program
    {
        static int[] ParseDate (string date, string dateFormat)
        {   /*Метод парсинга даты по шаблону.*/
            string d="",m="",y=""; 
            for (int i=0; i<dateFormat.Length;i++)
            {
               switch (dateFormat[i]) 
               {
                   case 'm': m=m.Insert(m.Length,date[i].ToString());break;
                   case 'd': d=d.Insert(d.Length,date[i].ToString());break;
                   case 'y': y=y.Insert(y.Length,date[i].ToString());break;
               }
            }
            int[] ret={int.Parse(d),int.Parse(m),int.Parse(y)};
            if (!IsCorrectDate(ret)) 
                {Console.Write("\n{0}-{1}-{2} - это некорректная дата. Заменяю на 0.",d,m,y);
                ret[0]=0;ret[1]=0;ret[2]=0;}
            return ret;
        }
        static bool IsCorrectDate (int[] date)
        {   /* проверка даты на корректность формате массива, где 0-день, 1-месяц,2-год*/
            /*    в этом месте можно генерировать исключительную ситуацию по-идее*/
            bool result=false;
            int[] months = {0,31,28,31,30,31,30,31,31,30,31,30,31};
            int year = date[2]+1;
            if (year%4==0 && year%100!=0 || year%400==0) months[2]=29; // високосный февраль
            if (date[1] >0 && date[1]<13) //Чтобы не было ошибки вызова несуществующего элемента массива
                result = date[0]>0 && date[0]<=months[date[1]];//Диапазон значений дней в месяце.
            return result;
        }
        static long ComparingDates (string dateFirst, string dateSecond, string dateFormat)
        {   /* Принимаем даты в виде строк и вычисляем их разность*/    
            // Считаю, что отрицательный год - до н.э.       
            int[] date1=ParseDate(dateFirst,dateFormat);
            int[] date2=ParseDate(dateSecond,dateFormat);
            return GetCountOfDaysFromZero(date2)-GetCountOfDaysFromZero(date1);
        }
        static long GetCountOfDaysFromZero(int[] date)
        {   /*Вычисляю количество дней от начала нашей эры*/
            int[] months = {0,31,28,31,30,31,30,31,31,30,31,30,31};
            if (date[2]%4==0 && date[2]%100!=0 || date[2]%400==0) months[2]=29; // високосный февраль этого года
            int year = date[2]-1;//Нужно учесть, что год ещё не наступил
            long countOfLeapYears=(year / 4) - (year / 100) + (year / 400);//Считаем количество високосных лет
            long countOfDaysFromZero=(date[2]-countOfLeapYears)*365+countOfLeapYears*366;
            for (int i=1; i<date[1];i++) countOfDaysFromZero+=months[i];
            countOfDaysFromZero+=date[0];
            return countOfDaysFromZero;
        }
        static void Main(string[] args)
        {   //Берём формат даты от пользователя
            Console.Write("Введи формат даты, например dd.mm.yy:"); string dateFormat = Console.ReadLine();
            //Берём две даты от пользователя
            Console.Write("Введи первую дату:");  string dateFirst = Console.ReadLine();
            Console.Write("Введи вторую дату:");  string dateSecond = Console.ReadLine();
            long countOfDays = ComparingDates(dateFirst,dateSecond,dateFormat);
            Console.WriteLine("{0}",countOfDays);
            if (countOfDays>0) Console.WriteLine("{1} > {0}",dateFirst,dateSecond);
            else if (countOfDays<0) Console.WriteLine("{0} > {1}",dateFirst,dateSecond);
            else if (countOfDays==0) Console.WriteLine("{1} = {0}",dateFirst,dateSecond);
        }

    }
}
