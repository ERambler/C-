/*
Выполнил: Юрьев Александр Дмитриевич
Вариант №11
Задание - табулирование системы с указанным шагом на указанном отрезке
        ⎧ctg²4x   , если |x| < 2 ;
   f(x)=⎨
        ⎩e² ͯ       , если |x|>=2;
*/
using System;
namespace ifelse
{
    class Program
    {
        static void Main(string[] args)
        {   
            float a,b,p,f; //[a,b] p - шаг f - функция
            Console.WriteLine("Введите нижнюю и верхнюю границы отрезка: ");
            a = Convert.ToSingle(Console.ReadLine());
            b = Convert.ToSingle(Console.ReadLine());
            Console.Write("Введите шаг: ");
            p = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("    x    f(x)");
            for (float i=a;i<=b;i=i+p)
            {
                if (Math.Abs(i)<2) f=Convert.ToSingle(Math.Pow(1/Math.Tan(4*i),2));
                else f=Convert.ToSingle(Math.Exp(2*i));
                Console.WriteLine("{0,4}    {1,10}",i,f);
                
            }
            Console.ReadKey();
        }
    }
}
