/*
Выполнила: Зуева Арина Руслановна
Вариант №15 
Задание - вычислить сумму сходящегося ряда с указанной точностью.
               (tg x+1)
заданный ряд: ――――――――――
                  2n!
*/
using System;
namespace lab3_1
{
    class Program
    {
        static void Main()
        {   
            Console.WriteLine("               (tg x+1)");
            Console.WriteLine("заданный ряд: ――――――――――");
            Console.WriteLine("                  2n!");    

            const float e = 0.00000000000001f; 
            const float x = 0.10f;
            double  F=1,    //факториал
                    n=1,    //верх факториала
                    c,      //член ряда
                    sum=0,  //сумма
                    sum2;   //сумма предыдущ
            do {
                    c=(Math.Tan(x)+1)/(2*F);
                    sum2=sum;
                    sum += c;
                    n++;
                    F*=n;

                    Console.WriteLine("{0}\tsum={1}",c,sum);
                } 
            while (Math.Abs(sum-sum2) > e);
            Console.WriteLine("\nСумма ряда -> {0}\t при количестве членов ряда = {1}",sum,n);
        }
    }
}