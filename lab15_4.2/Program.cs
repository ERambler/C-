/*
Выполнила: Зуева Арина Руслановна
Вариант №15
Задание: 
Дан массив размера n × n. Необходимо упорядочить (пере-
ставить) строки массива по убыванию сумм элементов строк.
*/
using System;
namespace Arr
{
    class Program
    {
    static void Main()
        {   const int n=5;                     // Дан массив размера n × n.
            int [,] M = {
                        {1,8,3,8,2},
                        {4,7,5,9,4},
                        {8,9,3,8,2},
                        {7,7,5,9,4},
                        {2,9,4,9,9}
                        };
            int [] A = new int[n];             // Сделаем вспомогательный массив
                                               // в него запишим суммы строк
            for (int i =0; i<n;i++)
            {   
                int sum =0;
                for (int s=0;s<n;s++) sum+=M[i,s];
                A[i]=sum;
            }
            for (int i=0;i<n;i++)
            {   for (int j=0;j<n;j++)
                    Console.Write ("{0,3}",M[i,j]);
                Console.Write ("  - {0}\n",A[i]);
            }   
            Console.WriteLine();                         
                for (int i = 0; i < A.Length; i++)      // Воспользуемся алгоритмом сортировки из следующей работы
                    for (int j = 0; j < A.Length - 1 - i; j++)
                        if (A[j] < A[j + 1])
                            {
                                int tmp = A[j];
                                A[j] = A[j + 1];
                                A[j + 1] = tmp;
                                for (int k = 0;k<n;k++) // когда будем сортировать массив, менять местами будем
                                {                       // не только вспомогательный, но и вместе с ним - исходный
                                    tmp = M[j,k];
                                    M[j,k]=M[j+1,k];
                                    M[j+1,k]=tmp;
                                }
                            }
            for (int i=0;i<n;i++)
            {   for (int j=0;j<n;j++)
                    Console.Write ("{0,3}",M[i,j]);
                Console.Write ("  - {0}\n",A[i]);
            }    
        }
    }
}