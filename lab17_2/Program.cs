/*
Выполнил: Мифтахов Фарид Равильевич
Вариант №17
Задача:
Протабулировать выражение по х:
        ⎧ sin²2x-6cos³x, если |x|<5
   f(x)=⎨
        ⎩ tg2x+1,       если |x|>=5
*/
using System;
namespace lab
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
                if (Math.Abs(i)<5) f=Convert.ToSingle(Math.Pow(Math.Sin(2*i),2)-6*Math.Pow(Math.Cos(i),3));
                else f=Convert.ToSingle(Math.Tan(2*i)+1);
                Console.WriteLine("X={0,6}|   F(X)={1}",i,f);
            }
            Console.ReadKey();
        }
    }
}
