/*
Выполнил Журавлёв Е. А. З-21-ИСТ-итпк-Б
Вариант 14
Задание 6

Создать метод, который сравнивает две даты, переданные в строковой  переменной,  
и  результат  сравнения  выдает  в  имени  функции.
Дополнительная задача: 
ввод формата пользователем
DateTime не использовать
*/
using System;
namespace lab14_6
{
    class Program
    {
        static int[] ParseDate (string date, string dateFormat)
        {   /*Метод парсинга даты по шаблону.*/
            string d="",m="",y="";
            for (int i=0; i<Math.Min(dateFormat.Length,date.Length);i++)
            {
               switch (dateFormat[i]) 
               {
                   case 'm': m=m.Insert(m.Length,date[i].ToString());break;
                   case 'd': d=d.Insert(d.Length,date[i].ToString());break;
                   case 'y': y=y.Insert(y.Length,date[i].ToString());break;
               }
            }
            int[] ret={int.Parse(d.Trim()),int.Parse(m.Trim()),int.Parse(y.Trim())};
            if (!IsCorrectDate(ret)) 
                {Console.Write("\n{0}-{1}-{2} - это некорректная дата. Заменим на 0. \nРезультат НЕКОРРЕКТЕН!\n",d,m,y);
                ret[0]=0;ret[1]=0;ret[2]=0;}
            return ret;
        }
        static bool IsCorrectDate (int[] date)
        {   /* проверка даты на корректность формате массива, где 0-день, 1-месяц,2-год*/
            /*    в этом месте можно генерировать исключительную ситуацию*/
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
            // Считаем, что отрицательный год - до н.э.       
            int[] date1=ParseDate(dateFirst,dateFormat);
            int[] date2=ParseDate(dateSecond,dateFormat);
            return GetCountOfDaysFromZero(date2)-GetCountOfDaysFromZero(date1);
        }
        static long GetCountOfDaysFromZero(int[] date)
        {   /*Вычисляем количество дней от начала нашей эры*/
            int[] months = {0,31,28,31,30,31,30,31,31,30,31,30,31};
            if (date[2]%4==0 && date[2]%100!=0 || date[2]%400==0) months[2]=29; // високосный февраль этого года
            int year = date[2]-1;                                               //Нужно учесть, что год ещё не наступил
            long countOfLeapYears=(year / 4) - (year / 100) + (year / 400);     //Считаем количество високосных лет
            long countOfDaysFromZero=(date[2]-countOfLeapYears)*365+countOfLeapYears*366;
            for (int i=1; i<date[1];i++) 
                countOfDaysFromZero+=months[i];
            countOfDaysFromZero+=date[0];
            return countOfDaysFromZero;
        }
        static void Main(string[] args)
        {   //Берём формат даты от пользователя
            Console.Write("Введите формат даты, например dd.mm.yy:"); string dateFormat = Console.ReadLine();
            if (dateFormat.Length==0) 
            {
                dateFormat="dd.mm.yyyy";
                Console.WriteLine("Используем формат dd.mm.yyyy");
            }
            
            //Берём две даты от пользователя
            Console.Write("Введите первую дату:");  
            string dateFirst = Console.ReadLine();
            
            Console.Write("Введите вторую дату:");  
            string dateSecond = Console.ReadLine();
            //Вызываем метод сравнения дат. Метод даст отрицательное количество дней, если вторая дата меньше первой
            long countOfDays = ComparingDates(dateFirst,dateSecond,dateFormat);
            if (countOfDays>0) Console.WriteLine("Событие {1} произошло позднее {0} на {2} дней."
                                                ,dateFirst,dateSecond
                                                ,Math.Abs(countOfDays));
            else if (countOfDays<0) Console.WriteLine("Событие {1} произошло раньше {0} на {2} дней."
                                                ,dateFirst,dateSecond
                                                ,Math.Abs(countOfDays));
            else if (countOfDays==0) Console.WriteLine("События {1} и {0} произошли в один день."
                                                ,dateFirst,dateSecond);
            Console.Read();
        }

    }
}
