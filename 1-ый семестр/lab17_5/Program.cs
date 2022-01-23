/*
Выполнила: Мифтахов Фарид Равильевич
Вариант №17
Задача.
Упорядочить массив случайных строчных латинских букв 
по убыванию методом обмена (пузырики)
*/
using System;
namespace l
{
    class Program
    {
    
    static void Main()
        {   
            const int n = 26; //количество букв
            Random RR = new Random(); //Случайные числа
            char[] F = new char[n];// массив с буквами длиной n
            for ( int i = 0; i < n; i++)        // зполняем
             F[i] = Convert.ToChar(RR.Next(97,122));  
             //строчные в ASCII с 97 по 122 код 
            Console.WriteLine(F);// Показываем на экран
            ChangeSort (F);//Запускаем сортировку
            Console.WriteLine(F);//Показываем на экран
            Console.ReadKey();
        }

    static void ChangeSort (char [] A)
        {   for (int i = 0; i < A.Length; i++)
             for (int j = 0; j < A.Length-1-i; j++)
              if (A[j] < A[j + 1]) // по убыванию же!
               { char tmp = A[j];
                A[j] = A[j + 1];
                A[j + 1] = tmp;}
        }
    }
}