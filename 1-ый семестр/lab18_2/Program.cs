/*
Выполнил: Шпиля
Вариант №18
Задача:
Протабулировать функцию по х:
        ⎧ sin³5x+2cos²x, если |x|<2
   f(x)=⎨
        ⎩ сtg2x+1,       если |x|≥2
*/
using System;
namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {   
            float a,b,p,f;
            Console.WriteLine("От: ");
            a = Convert.ToSingle ( Console.ReadLine() );
            Console.WriteLine("До: ");
            b = Convert.ToSingle ( Console.ReadLine() );
            Console.WriteLine("Шаг: ");
            p = Convert.ToSingle(Console.ReadLine());
            for (float i=a;i<=b;i=i+p)
            {
                if (Math.Abs(i)<2) f=Convert.ToSingle(Math.Pow(Math.Sin(5*i),3)+2*Math.Pow(Math.Cos(i),2));
                else f=Convert.ToSingle((1/Math.Tan(2*i))+1);
                Console.WriteLine("X={0,6}|   F(X)={1}",i,f);
            }
            Console.ReadKey();
        }
    }
}
