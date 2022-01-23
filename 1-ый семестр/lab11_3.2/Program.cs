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
        static void Main()         
        {   const float e = 0.0001f; 
            float x0, x1, x2 = 0, fx0, fx2;
            Console.WriteLine ("Введите начало и конец числового промежутка");
            x0 = Convert.ToSingle(Console.ReadLine());
            x1 = Convert.ToSingle(Console.ReadLine()); 
            while (Math.Abs(x1 - x0) > e) 
            {
                x2 = (x0 + x1) / 2;
                fx0 = Convert.ToSingle(Math.Log(x0) - 7/(2*x0+6));
                fx2 = Convert.ToSingle(Math.Log(x2) - 7/(2*x2+6)); 
                if (fx0 * fx2 < 0) x1 = x2; 
                else x0 = x2;            
            } 
        Console.WriteLine("Значение корня уравнения на заданном числовом промежутке = {0}", x2); 
        Console.Read();
        }
    }
} 