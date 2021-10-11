/*
Выполнил: Юрьев Александр Дмитриевич
Вариант №11
Задание - найти любой корень уравнения методом половинного деления.
                    7
уравнение: ln x - ――――――
                  2x + 6
*/
using System; 
namespace Bisection
{ 
    class Program    
    { 
        static float fEquation  (double x) {return Convert.ToSingle(Math.Log(x) - 7/(2*x+6));}
        static void Main()         
        {   const float e = 0.0001f; 
            float x0, x1, x2 = 0, fx0, fx2;

            Console.WriteLine ("Введите начало и конец числового промежутка");
            x0 = Convert.ToSingle(Console.ReadLine());
            x1 = Convert.ToSingle(Console.ReadLine()); 
            while (Math.Abs(x1 - x0) > e) 
            {
                x2 = (x0 + x1) / 2;
                fx0 = fEquation(x0);
                fx2 = fEquation(x2); 
                if (fx0 * fx2 < 0) x1 = x2; 
                else x0 = x2;            
            } 
        Console.WriteLine("Значение корня уравнения на заданном числовом промежутке = {0}", x2); Console.Read();
        }
    }
} 