/*
Выполнил: Журавлёв Евгений Александрович
Вариант №14, 
Задание - вычислить сумму сходящегося ряда с указанной точностью.
               2     2     2     2
заданный ряд: ――― - ――― + ――― - ―――...
               1!    2!    4!    7!
*/
#include "stdio.h"
#include "math.h"
int main()
{   
  const float e = 0.000000001f; 
  int i=0, l=1, s=1; 
  double  nFact=1,//факториал
  		n=1,    //верх факториала
  		t,      //член ряда
  		sum=0,  //сумма
  		diff;   //сумма предыдущ
  do {
      nFact=1;
      for (int k=1;k<=n;k++) nFact*=k;
      if (i % 2) l=-1; else l=1;
      t = l * (2/nFact); 
      printf("nFact=%f\nn=%f\nt=%f\n",
       		nFact,
       		n,
       		t
         	);
         		   diff=sum;
                    sum += t; 
                    i++;
                    n+=i;     
     } 
  while (abs(sum-diff) > e);
   printf ("\n%e, %e",sum,t);
}
