/*
Выполнил: Юрьев Александр Дмитриевич
Вариант №11

Задание - Даны массивы А и В. Требуется создать массив, содержащий
элементы, которые есть только в массиве А и только в массиве В.
*/
using System;
namespace ThirdArray
{
    class Program
    {
        static void Show (int[] C)                      //  Вывод массива
        {   
            for (int j=0;j<C.Length;j++) Console.Write("{0} ",C[j]); Console.WriteLine();
        }
        static void Main()
        {   int [] A = {1,2,3,2,2,2,2,8,9,0};
            int [] B = {1,2,2,4,2,2,7,2,9,2};
            int [] C = {};
            Console.Write("Массив А:");  Show(A);
            Console.Write("Массив B:");  Show(B);
            foreach (int a in A)        //ForEach       https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/arrays/using-foreach-with-arrays |Big_O(Length)
            {                           //Array.Exisis  https://docs.microsoft.com/ru-ru/dotnet/api/system.array.exists?view=netcore-3.1                  |Big_O(Length)
                if (!(Array.Exists(C,element => element == a)))      //  Проверяем, если ли в B элемент a
                {
                    if  (!(Array.Exists(B, element => element == a)))//  Проверяем, если ли в С такой же  
                    {                                                //и если его там нет, дописываем.
                        int count = C.Length;
                        Array.Resize(ref C, count + 1);              // Array.Resize https://docs.microsoft.com/ru-ru/dotnet/api/system.array.resize?view=netcore-3.1 |Big_O(NewSize)
                        C[count]=a;                                  // Добавляем новый элемент
                    }
                }
            }
            foreach (int b in B)       // Повторяем то же самое с Массивом B
            {                           
                if (!(Array.Exists(C,element => element == b)))      
                {
                    if  (!(Array.Exists(A, element => element == b)))
                    {                                             
                        int count = C.Length;
                        Array.Resize(ref C, count + 1);                
                        C[count]=b;                                  
                    }
                }
            }
            Console.Write("Массив C:");
            Show(C);
            Console.Read();
        }
    }
}