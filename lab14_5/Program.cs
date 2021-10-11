/*
Выполнил: Журавлёв Евгений Александрович
Вариант №14, 
Лабораторное задание: написать программу, выполняющую задачу сортировки массива строчных русских букв методом слияния по возрастанию.
При выполнении задания необходимо предусмотреть следующие этапы работы программы:
1) выделение памяти под массив;
2) заполнение массива неупорядоченными данными;
3) вывод неупорядоченных данных массива на экран;
4) сортировка массива указанным методом;
5) вывод упорядоченных данных массива на экран.
*/
using System;
namespace ArrayBubble
{
    class Program
    {
        static void Main()
        {   
            const int n = 80;                   //  длина массива
            Random rnd = new Random();
            char[] arr = new char[n];           // 1) выделение памяти под массив;
            for ( int i = 0; i < n; i++)        // 2) заполнение массива неупорядоченными данными;
                arr[i] = Convert.ToChar(rnd.Next(1072,1103));  //https://docs.microsoft.com/ru-ru/dotnet/api/system.random.next?view=net-5.0 
                                                            //https://docs.microsoft.com/ru-ru/dotnet/api/system.convert.tochar?view=net-5.0#System_Convert_ToChar_System_UInt16_
            Console.WriteLine(arr);             // 3) вывод неупорядоченных данных массива на экран;
            MergeSort (arr,0,arr.Length-1);     // 4) сортировка массива указанным методом;
            Console.WriteLine(arr);             // 5) вывод упорядоченных данных массива на экран.
            Console.ReadKey();
        }
        static void MergeSort(char[] A, int low, int high) // Взято из решения, предложенного в методическом пособии.
        {                                                  // Изменен тип получаемого массива с int на char
            if (low < high)
            {
                int center = (low + high) / 2;
                MergeSort(A, low, center);
                MergeSort(A, center + 1, high);
                Merge(A, low, center, high);
            }
        }
        static void Merge(char[] A, int low, int center, int high)
        {
            int i = low, j = center + 1, tmpPos = 0;
            char[] tmp = new char[high - low + 1];
            while (i <= center && j <= high)
            {
                if (A[i] < A[j]) tmp[tmpPos++] = A[i++];
                else tmp[tmpPos++] = A[j++];
            }
            while (j <= high) tmp[tmpPos++] = A[j++];
            while (i <= center) tmp[tmpPos++] = A[i++];
            for (tmpPos = 0; tmpPos < tmp.Length; tmpPos++) A[low + tmpPos] = tmp[tmpPos];
        }   
    }
}