/*
Выполнил: Журавлёв Евгений Александрович
Вариант №14, 
Задание - Дан массив размера n. Найти количество участков, на кото-
рых его элементы возрастают (участком считать последовательность
от 3-х элементов).
*/
using System;
namespace Subsequences
{
    class Program
    {
    static void Main()
        {
        const int n = 30;           //  Размер массива
        int counter=0,              //  счётчик возрастающих элементов
            quantity=0;             //  счётчик последовательностей, 
                                    //удовлетворяющих условию задачи
        int [] A = new int[n]  {1,1,1,1,1,1,2,3,4,2,    
                                3,4,2,3,4,5,6,7,8,9,
                                9,9,9,4,5,6,7,8,9,9};
        for (int i = 1; i < A.Length; i++) // Перебор массива со второго элемента
        {
            if (A[i]>A[i-1])               
            {
                if (counter==0) Console.Write("{0} ",A[i-1]); //  Выводим первый элемент
                counter++;                  //  Засчитываем элемент
                Console.Write("{0} ",A[i]); //  Вывод элемента
            } else
            {
                if (counter >=2)            //  Условие длины последовательности
                {
                    counter=0;              //  Сброс счётчика
                    quantity++;             //  Засчитываем последовательность
                    Console.WriteLine();    //  Возврат строки
                }
            }
        }
        if (counter >=2) quantity++;        //  Засчитываем последовательность, которая может
                                            //оказаться в конце массива
        Console.WriteLine("Количество возрастающих последовательностей массива: {0}",quantity);
            Console.Read();        
    }
}}