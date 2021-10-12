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
    
    static float fEquation  (double x) {return Convert.ToSingle(2*Math.Sin (3*x) - 1.5*x);}
    static float fChord (double x1,double x0, double fx1, double fx0) 
    {return Convert.ToSingle(x0-(x1-x0)/(fx1-fx0)*fx0);}
    
    static void Main()
        {   
            Console.WriteLine("уравнение: 2sin(3x)-1.5x");
            const float e = 0.0001f;
            float x0, x1, x2, fx0, fx1, fx2;
            Console.WriteLine("Введите начало и конец числового отрезка");
            x0 = Convert.ToSingle(Console.ReadLine());
            x1 = Convert.ToSingle(Console.ReadLine());
            do {
                fx0 = fEquation (x0);
                fx1 = fEquation (x1);
                x2  = fChord    (x1,x0,fx1,fx0);
                fx2 = fEquation (x2);
                if (fx0 * fx2 < 0) x1 = x2; else x0 = x2;
            } while (Math.Abs(fx2) > e);
            Console.WriteLine("Значение корня уравнения на заданном числовом отрезке = {0}", x2);
            Console.Read();
        }
    }
}