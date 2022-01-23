/*
Выполнила: Зуева Арина Руслановна
Вариант №15
Задание: 
Быстрая сортировка массива вещественных чисел по-возрастанию
*/
using System;
namespace Arr
{
    class Program
    {
    static void QuickSort(double[] A,int low,int high)
    {   
        int i = low;
        int j = high;
        double x = A[(low + high) / 2];
        do {
            while (A[i] < x) i++;
            while (A[j] > x) j--;
            if (i <= j)
                {
                    double temp = A[i];
                    A[i] = A[j];
                    A[j] = temp;
                    i++; j--;
                }
        } while (i < j);
            if (low < j) QuickSort(A, low, j);
            if (i < high) QuickSort(A, i, high);
    }
    static void Main()
        {   const int n = 5;                   //длина массива
            Random r = new Random();         // генератор случайных чисел
            double [] A = new double[n];         // массив вещественных указанной длины 
            for ( int i = 0; i < n; i++)        // зполняем вещественными
            {
                A[i] = r.NextDouble();
                Console.Write ("{0,20}",A[i]);
            }
            Console.WriteLine();
            QuickSort(A,0,A.Length-1);
            for ( int i = 0; i < n; i++) Console.Write ("{0,20}",A[i]);
            Console.WriteLine();
            Console.Read();
        }
    }
}