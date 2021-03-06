/*
Выполнил: Журавлёв Евгений Александрович
Вариант №14, 

Задание - Дан массив размера n × n. Необходимо упорядочить (пере-
ставить) строки массива по возрастанию значений первых элементов
строк.
*/
using System;
namespace ArrayBubble
{
    class Program
    {
    const int n = 5;                                //  Дан массив размера n × n
    static int [,] A = new int[n,n] {{4,1,1,1,1},
                                     {9,2,3,4,2},    
                                     {3,4,2,3,4},
                                     {7,6,7,8,9},
                                     {6,9,9,4,5}};    

    static void Main()
        { 
            Console.WriteLine("Массив до сортировки:");
            ShowMeArray();        //  Показываем массив на экран
            Bubble();             //  Запускаем сортировку
            Console.WriteLine("Массив после сортировки:");
            ShowMeArray();        //  Показываем массив на экран
            Console.Read();
       }
    static void ShowMeArray ()    //  Показываем массив на экран
    {   
        for (int i=0;i<n;i++)
        {   
            for (int j=0;j<n;j++) Console.Write("{0}\t",A[i,j]); 
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    static void Bubble ()           // Начинаем итерацию сортировки элементов
    {                               // Простым, но не очень эффективным способом
        bool iscorrect = true;      // Будем считать, что массив отсортирован,
        for (int i = 1; i < n; i++) // Однако убедимся в этом, перебрав его
        {   if (A[i,0]<A[i-1,0])     // Если нашли больший элемент, то
            {   iscorrect = false;   // запоминаем, что нужна ещё одна итерация
                int swap;            // и, для перемены строк, создадим переменную,
                for (int j=0;j<n;j++)// и, наконец, поменяем местами строки.
                {   swap=A[i,j];
                    A[i,j]=A[i-1,j];
                    A[i-1,j]=swap;
                }
            }
        }
        if (!iscorrect) Bubble();    // Запускаем рекурсию, до победного, если это надо.
    }
    }
}