/*
Выполнил: Юрьев Александр Дмитриевич
Вариант №11
  Необходимо заполнить массив размером n × m в шахматном
порядке: клетки черного цвета заполнены нулями, а белого цвета -
заполнены числами натурального ряда сверху вниз, слева направо.
*/
using System;
namespace ThirdArray
{
    class Program
    {
    static void Show (int[,] A)                      //  Массив на экран
    {   
        for (int i=0; i<A.GetUpperBound(0);i++)
        {   for (int j=0; j<A.GetUpperBound(1);j++) 
                Console.Write("{0,4}",A[i,j]);
            Console.WriteLine();
        }
    }
        static void Main()
        {   
            const int n = 8, m = 8; // массив размером n × m
            int[,] A = new int[n,m]; 
            int counter=1;           // Счётчик для заполнения
            for (int i=0;i<n;i++)    // Заполним массив за один проход,
            {                        // Чтобы не выделять лишее тепло на ЧИПах
                for (int j=0;j<m;j++)// Оптимальный вариант решения - сравнивать
                {                    // чётность итераторов строк и столбцов.   
                    if ((i%2==0) == (j%2==0)) A[i,j]=0; else A[i,j]=counter++;
                }
            }
            Show(A);
            Console.ReadKey();
        }
    }
}