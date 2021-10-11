/*
Выполнил: Журавлёв Евгений Александрович
Вариант №14, 
Задание - найти любой корень уравнения методом секущих на заданном отрезке.
уравнение: e ͯ -2(x-1)²
*/
using System;
namespace Secant
{
    class Program
    {
    static float fEquation  (float x) {return Convert.ToSingle((Math.Exp (x) - 2*(x-1)*(x-1)));}
    static void Main()
        {   
            Console.WriteLine("уравнение: e ͯ -2(x-1)²");
            const float e = 0.0001f;
            float x0, x1, x2, fx0, fx1, fx2;
            Console.WriteLine("Введите начало и конец числового отрезка");
            x0 = Convert.ToSingle(Console.ReadLine());
            x1 = Convert.ToSingle(Console.ReadLine());
            do {fx0 = fEquation (x0);
                fx1 = fEquation (x1);
                x2  = x1 - fx1 * (x1 - x0) / (fx1 - fx0);
                fx2 = fEquation (x2);
                x0  = x1; x1 = x2;
            } while (Math.Abs(fx2) > e);
            Console.WriteLine("Значение корня уравнения на заданном числовом отрезке = {0}", x2);
            Console.Read();
        }
    }
}