/*
Выполнил: Зуева Арина Руслановна
Вариант №15
Изучение условных и циклических конструкций.
Протабулировать сисему.
     ⎧3cos⁵5x, если |x|<2
f(x)=⎨
     ⎩e⁵ ͯ    , если |x|>=2
*/
using System;
namespace IfFor
{
    class Program
    {
        static void Main(string[] args)
        {   
            Double a,b,p,function;
            Console.Write("[a: "); 
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("b]: ");       
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("step: ");     
            p = Convert.ToDouble(Console.ReadLine());
            for (Double x=a;x<=b;x+=p)
            {
                if (Math.Abs(x)<2) function=3*Math.Pow(Math.Cos(5*x),5);
                else function=Math.Exp(5*x);
                Console.WriteLine("f({0})         {1}",x,function);
                Console.ReadKey();
            }
        }
    }
}
