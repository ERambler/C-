/*
Выполнила: Мифтахов Фарид Равильевич
Вариант №17
Задание - найти любой корень уравнения методом хорд.

уравнение: 2sin(3x)-1.5x
*/
using System;
namespace Chord
{
    class Program
    {
    static void Main()
        {   
            Console.WriteLine("уравнение: 2sin(3x)-1.5x");
            const float e = 0.000001f;
            float x0, x1, x2, fx0, fx1, fx2;
            Console.WriteLine("Введите начало и конец числового отрезка");
            x0 = Convert.ToSingle(Console.ReadLine());
            x1 = Convert.ToSingle(Console.ReadLine());
            do {
                fx0 = Convert.ToSingle(2*Math.Sin (3*x0) - 1.5*x0);
                fx1 = Convert.ToSingle(2*Math.Sin (3*x1) - 1.5*x1);
                x2  = Convert.ToSingle(x0-(x1-x0)/(fx1-fx0)*fx0);
                fx2 = Convert.ToSingle(2*Math.Sin (3*x2) - 1.5*x2);
                if (fx0 * fx2 < 0) x1 = x2; else x0 = x2;
            } while (Math.Abs(fx2) > e);
            Console.WriteLine("Значение корня уравнения на заданном числовом отрезке = {0}", x2);
            Console.Read();
        }
    }
}