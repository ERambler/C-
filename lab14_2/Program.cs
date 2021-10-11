/*
Выполнил: Журавлёв Евгений Александрович
Вариант №14
Задача:
написать программу на языке C#, которая будет реализовывать табулирование
функции для заданной системы уравнений на числовом промежутке [a, b] 
с шагом p. Данные должны выводиться в табличной форме, где каждому 
значению аргумента соответствует подсчитанное значение функции.

Система уравнений:
        ⎧  cos²2x+7tg⁴x, если |x|<3
   f(x)=⎨
        ⎩  √|ctg3x-5|, если |x|>=3
*/
using System;
namespace lab
{
    class Program
    {
        static void Main()
        {   
            Console.WriteLine("Программа табулирвания значений системы:");
            Console.WriteLine("        ⎧  cos²2x+7tg⁴x, если |x|<3");
            Console.WriteLine("  f(x)= ⎨");
            Console.WriteLine("        ⎩  √|ctg3x-5|, если |x|>=3");
            double a, b, p, f; //переменные условий табулирования и вывода функции
            Console.Write("Нижняя граница отрезка,  нестрого: ");
                a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Верхняя граница отрезка, нестрого: ");
                b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Шаг табулирования: ");
                p = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\tx\t\tf(x)");
            for (double i = a; i <= b; i += p) {
                if (Math.Abs(i) < 3) f = Math.Pow( Math.Cos(2*i), 2) + 7 * Math.Pow( Math.Tan(i),4 );
                else f = Math.Sqrt( Math.Abs ( 1/Math.Tan(3*i)-5 ) );
            Console.WriteLine("{0}\t\t{1}",i,f);}
            
        }
    }
}
