/*
Выполнил: 
Вариант №15 
Задание - вычислить сумму сходящегося ряда с указанной точностью.
               (tg x+1)
заданный ряд: ――――――――――
                  2n!
*/
#include "stdio.h"
#include "math.h"
int main()
{   
  const float e = 0.00000000000001f; 
  const float x = 3.0f;
  double  F=1,    //факториал
  		n=1,    //верх факториала
  		c,      //член ряда
  		sum=0,  //сумма
  		sum2;   //сумма предыдущ
  do {
        c=(tan(x)+1)/(2*F);
        sum2=sum;
        sum += c;
        n++;
        F*=n;
      } 
  while (abs(sum-sum2) > e);
   printf ("\n%e, %f",sum,n);
}
