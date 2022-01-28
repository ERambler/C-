using System;
using System.Threading;

namespace ScanCodes
{
    class Program
    {
        public static void Main()
        {
            ConsoleKeyInfo cki;
            int i=11,j=10,length=4;
            int[,] snake=new int[40,2]; for (int c=0; c<40;c++) {snake[c,0]=10;snake[c,1]=10;} snake[0,0]=10;snake[0,1]=10;
            Random RND = new Random();
            
            bool win=false;
            int[] vector={0,1};
            bool play=true;
            Console.Clear();
            Console.SetCursorPosition (0, 0);
                                    Console.Write("╔════════════════════════════════════════╗\n");
            for (int c=0; c<15;c++) Console.Write("║                                        ║\n");
                                    Console.Write("╚════════════════════════════════════════╝\n");   
                int[] apple=newApple(snake,length);        
            do 
            {   Console.CursorVisible = false;
                if (Console.KeyAvailable) 
                {
                    cki = Console.ReadKey(true);
                //Console.Write("{0}", cki.Key);
                    switch (cki.Key)
                    {
                        case (ConsoleKey.LeftArrow) : vector[0]=-1; vector[1]=0; break;
                        case (ConsoleKey.DownArrow) : vector[0]= 0; vector[1]=1; break;
                        case (ConsoleKey.UpArrow)   : vector[0]= 0; vector[1]=-1;break;
                        case (ConsoleKey.RightArrow): vector[0]=1; vector[1]=0;  break;
                    }
                }
                    i+=vector[0];j+=vector[1];
                    if (i>40) i=1; if (i<1) i=40; if (j>15) j=1; if (j<1) j=15;
                    snake=snakeArrayMove(snake,i,j);
                    snakePaint(snake,length);
                    if (i==apple[0]&&j==apple[1]) 
                    {
                        length++; apple=newApple(snake,length);
                        Console.SetCursorPosition (20,18);
                        Console.Write("{0}  ",39-length);

                        if (length>=39) {win=true;play=false;}
                    }
                    for (int x=1;x<length;x++) if (i==snake[x,0] && j==snake[x,1]) play=false;
                    Thread.Sleep(150-3*length);
            } while(play);
            if (!win) Console.WriteLine("\nИГРА ОКОНЧЕНА. ЗМЕЙКА САМОЗАДУШИЛАСЬ");
            else Console.WriteLine("\nПОБЕДА. ЗМЕЙКА ОТЪЕЛАСЬ");
            Thread.Sleep(3000);
            Console.ReadKey();
        }
        static int[] newApple(int[,] snake,int length)
        {
            int[] apple = new int[2];
            Random RND=new Random();
            bool correct;
            do {
            correct=true;
            apple[0]=RND.Next(1,40); apple[1]=RND.Next(1,15);
            for (int x=1;x<length;x++) if (apple[0]==snake[x,0] && apple[1]==snake[x,1]) correct=false;
            }
            while (!correct);
                        Console.SetCursorPosition (apple[0],apple[1]);
                        Console.Write("◯"); 
            return apple;

        }
        static int[,] snakeArrayMove(int[,] arr,int x, int y)
        {
            for (int i=39; i>0;i--)
            {
                arr[i,0]=arr[i-1,0];
                arr[i,1]=arr[i-1,1];
            }
                arr[0,0]=x;
                arr[0,1]=y;
                return arr;
        }
        static void snakePaint (int[,] arr, int length)
        {
            /*for (int i=0;i<length;i++)
            {

                Console.SetCursorPosition (arr[i,0],arr[i,1]);
                Console.Write("■");
            }*/
                Console.SetCursorPosition (arr[0,0],arr[0,1]);
                Console.Write("█");
                Console.SetCursorPosition (arr[length,0],arr[length,1]);
                Console.Write(" ");
        }
    }
}
