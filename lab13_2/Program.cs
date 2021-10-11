/*
Выполнил: Дуничева Юлия Дмитриевна
Вариант №13
Задача:
написать программу которая будет реализовывать табулирование функции 
для заданной системы уравнений на числовом промежутке [a, b] 
с шагом p. Данные должны выводиться в табличной форме, где каждому 
значению аргумента соответствует подсчитанное значение функции.
Система:
        ⎧ sin2x-√(x⁵ +4), если x<0
   f(x)=⎨ 10, если x=0;
        ⎩ |8-x³|-10x, если x>0;
*/
using System;
namespace KR2
{
    class Program
    {
        static void Main(string[] args)
        {   
            double a, b, p, fx=0; //переменные отрезка, шага и значения функции
            Console.Write("Нижняя граница: ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Верхняя граница: ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Шаг: ");
            p = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("    x\tf(x)");
            for (double x = a; x <= b; x += p)
            {
                if (x<0) 
                  fx=Math.Sin(2*x)-Math.Sqrt(Math.Pow(x,5)+4);
                else if (x==0)
                  fx=10;
                else if (x>0)
                  fx=Math.Abs(8-Math.Pow(x,3)-10*x);
                Console.WriteLine("{0}\t{1}",x,fx);
            }
        }
    }
}
