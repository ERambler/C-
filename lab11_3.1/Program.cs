/*
Выполнил: Юрьев Александр Дмитриевич
Вариант №11
Задание - вычислить сумму сходящегося ряда с указанной точностью.
              (5x-1)
заданный ряд: ――――――
              (1-n!)
*/
using System;
namespace lab3_1
{
    class Program
    {
        static void Main()
        {   
            const float e = 0.00000001f; 
            const float x = 1f;
            double  F=2,    //факториал
                    n=2,    //верх факториала
                    c,      //член ряда
                    sum=0,  //сумма
                    sum2;   //сумма предыдущ
            do {
                    c=(5*x-1)/(1-F);
                    sum2=sum;
                    sum += c;
                    n++;
                    F*=n;
                    Console.WriteLine("{0}\tsum={1}",c,sum);
                } 
            while (Math.Abs(sum-sum2) > e);
            Console.WriteLine("\nСумма ряда -> {0}\t при количестве членов ряда = {1}",sum,n);
            Console.ReadKey();
        }
    }
}