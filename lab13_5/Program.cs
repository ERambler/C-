/*
Выполнил: Дуничева Юлия Дмитриевна
Вариант №13, 
Задание: 
  написать программу на языке C#, выполняющую сортировку массива 
  прописных русских букв методом обмена по возрастанию.

*/
using System;
namespace lab5
{
    class Program
    {
    static void ChangeSort (char [] A)
    {
        for (int i = 0; i < A.Length; i++)
            for (int j = 0; j < A.Length - 1 - i; j++)
                if (A[j] > A[j + 1])
                {
                    char tmp = A[j];
                    A[j] = A[j + 1];
                    A[j + 1] = tmp;
                }
    }
    static void Main()
        {   
            const int n = 28;      //  длина массива
            Random rand = new Random();
            char[] M = new char[n];     // массив
            for ( int i = 0; i < n; i++)  // зполняем
                M[i] = Convert.ToChar(rand.Next(1040,1071));  //<<<<ЮЛЬ, ОБРАТИ ВНИМАНИЕ, ЧТО КОДЫ ПРИВЕДЕНЫ В UNICODE ЕСЛИ НАДО, А ВОЖМОЖНО НАДО, ПЕРЕВЕДИ В ANSI-1251 (192,223)!!! 
            Console.WriteLine(M);
            ChangeSort (M);
            Console.WriteLine(M);
            Console.ReadKey();
        }
    }
}