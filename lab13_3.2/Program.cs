/*
Выполнил: Дуничева Юлия Дмитриевна
Вариант №13, 
Задание - найти любой корень уравнения методом __Ньютона__.

уравнение: e ͯ +x²-2
*/
using System;
namespace Secant
{
    class Program
    {
    static void Main()
        {   
            Console.WriteLine("уравнение: e ͯ +x²-2");
            const double e = 0.0001f;
            double x, xLast, fx, _fx;
            Console.WriteLine("Введите начальное приближение к корню");
            x = Convert.ToSingle(Console.ReadLine());
            do {
                 fx = Math.Exp (x) + x*x - 2;   //Считаем функцию
                _fx = Math.Exp (x) + 2*x;       //Считаем производную
                xLast = x;                      //Запоминаем предыдущее значение
                x = xLast - fx/_fx;             //считаем новое значение
                
            } while (Math.Abs(fx) > e);
            Console.WriteLine("Значение корня уравнения на заданном числовом отрезке = {0}", x);
            Console.Read();
        }
    }
}