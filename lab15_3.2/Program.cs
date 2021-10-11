/*
Выполнила: Зуева Арина Руслановна
Вариант №15
Задание - найти любой корень уравнения методом __простых итераций.

уравнение: e ͯ +2x²-3
*/
using System;
namespace easyIterations
{
    class Program
    {
    static void Main()
        {   
            Console.WriteLine("уравнение: e ͯ +2x²-3");
            const float e = 0.0001f;
            const float b = 0.001f;
            float x, fx;
            Console.WriteLine("Введите начальное приближение к корню");
            x = Convert.ToSingle(Console.ReadLine());
            do {
                fx = Convert.ToSingle(Math.Exp (x) + 2 * x*x - 3);
                x = x + b * fx;
            } while (Math.Abs(fx) > e);
            Console.WriteLine("Значение корня уравнения = {0}", x);
            Console.Read();
        }
    }
}