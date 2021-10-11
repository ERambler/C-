/*
Выполнил: Юрьев Александр Дмитриевич
Вариант №11
требуется написать программу, выполняющую
задачу сортировки массива прописных латинских букв методом вставок по-возрастанию


*/
using System;
namespace InstertionSort
{
    class Program
    {

        static void InsertSort(char[] A)
        {
            for (int i = 1; i < A.Length; i++)
            {
                char elem = A[i];
                int pos = 0;
                while (elem > A[pos]) pos++;
                    for (int j = i - 1; j >= pos; j--) A[j + 1] = A[j];
                        A[pos] = elem;
            }
        }   


        static void Main()
        {   
            const int n = 28; 
            char[] A = new char[n]; //выделение памяти под массив;
            Random RND = new Random(); //заполнение массива неупорядоченными данными;
            for ( int i = 0; i < n; i++) A[i] = Convert.ToChar(RND.Next(97,123));
            Console.WriteLine(A); //вывод неупорядоченных данных массива на экран;
            InsertSort(A); //сортировка массива указанным методом;
            Console.WriteLine(A); //вывод упорядоченных данных массива на экран.

            Console.ReadKey();
        }
    }
}