/*
Выполнил: Дуничева Юлия Дмитриевна
Вариант №13, 
Задание: 
  Дан массив произвольного размера и два числа p и q (p < q).
Определить, сколько элементов массива лежит между p и q.
*/
using System;
namespace ArrayFinding
{
    class Program
    {
    static void Main()
        {   const int p=2, q=7;                 // два числа p и q (p < q)
            int [] M = {1,2,3,2,2,6,7,8,9,4};   // массив произвольного размера
            int count=0;                        // Счётчик
            foreach (int m in M)
            {   if (m>p && m<q) 
                {   Console.Write ("{0} ",m);
                    count++;
                }
            }
            Console.WriteLine("\nКоличество элементов массива между {0} и {1} : {2}",p,q,count);
            Console.ReadKey();
            // Есть такой метод в Array: int[] matchedItems = Array.FindAll(M, x => x >= p && x < q);
            // https://docs.microsoft.com/ru-ru/dotnet/api/system.array.findall?view=netcore-3.1
            // Однако согласно контексту, вероятнее всего нужно выполнить поиск руками.
        }
    }
}