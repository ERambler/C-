/*
Выполнил: Журавлёв Евгений Александрович
Вариант №14, 
Задание - вычислить сумму сходящегося ряда с указанной точностью.
               2     2     2     2
заданный ряд: ――― - ――― + ――― - ――― ...
               1!    2!    4!    7!
*/
using System;
namespace lab3_1
{
    class Program
    {
        static void Main()
        {   
            Console.WriteLine("               2     2     2     2");
            Console.WriteLine("заданный ряд: ――― - ――― + ――― - ――― ...");
            Console.WriteLine("               1!    2!    4!    7!");    

            const float e = 0.0000000001f; 
            int i=0, l; 
            double  nFact=1,//факториал
                    n=1,    //факториала
                    t,      //член ряда
                    sum=0,  //сумма полученная
                    diff;   //сумма предыдущая
            do {
                nFact=1;
                for (int k=1;k<=n;k++) nFact*=k; //Расчёт факториала от n
                if (i%2!=0) l=-1; else l=1;         //Определяем знак перед чётными членами ряда
                t = l * (2/nFact); 
                            diff=sum;
                                sum += t; 
                                i++;
                                n+=i;   
                } 
            while (Math.Abs(sum-diff) > e);
            Console.WriteLine("\nСумма ряда -> {0}\t при количестве членов ряда = {1}\nс точностью {2}",sum,n,e);
            Console.Read();
        }
    }
}