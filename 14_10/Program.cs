/*
В некоторых строках текстового файла имеются выражения, со-
стоящие из двух целых чисел, разделенных знаком арифметиче-
ской операции ( '+', '-', '*', '/' ). В строке перед выражением и после
него могут находиться произвольные символы. Требуется выде-
лить строку, в которой значение выражения максимально.
*/

using System.Text.RegularExpressions; //https://docs.microsoft.com/ru-ru/dotnet/api/system.text.regularexpressions.regex?view=net-6.0

namespace _14_10
{
    class Program
    {
        private static Regex RegExpr_SearchPattern = new Regex(@"\d*\d[\-\+\*\/]\d\d*",RegexOptions.Compiled);
        private static Regex RegExpr_SearchOperation = new Regex(@"[\-\+\*\/]",RegexOptions.Compiled);

        internal static void Main(string[] args)
        {
            int calc;
            int max = -100500;
            int? maxStrNum = null;
            StreamReader sr = new StreamReader ("file.text");
            for (int c=0; sr.Peek() > -1;c++)
                if (calcMaxSum(sr.ReadLine(),out calc))
                    if (max < calc) 
                    {max = calc; maxStrNum = c;}
                    
            sr.Close();
            //sr.BaseStream.Position=0;
            StreamReader sr1 = new StreamReader ("file.text");
            ConsoleColor color = Console.BackgroundColor;
            for (int c=0; sr1.Peek() > -1;c++)
            {
                if (c == maxStrNum) 
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(sr1.ReadLine());
                if (c == maxStrNum) 
                    Console.BackgroundColor = color;
            }
            sr1.Close();
        }
        private static bool calcMaxSum (string str, out int output)
        {
            List<int> sums = new List<int>(){};
            List <Match> matches = RegExpr_SearchPattern.Matches(str).ToList();
            foreach (var match in matches)
            {
                //Console.WriteLine (match.Value);
                Match operate = RegExpr_SearchOperation.Match (match.Value);
                int first  = int.Parse(match.Value.Substring(0,operate.Index));
                int second = int.Parse(match.Value.Substring(operate.Index+1,match.Value.Length-operate.Index-1));
                char   operat = match.Value[operate.Index];
                sums.Add(sum(first,second,operat));
            }
            if (sums.Count > 0) {output = sums.Max(); return true;} else {output = 0; return false;}
        }
        private static int sum (int x, int y, char ch) 
        {
            int s = 0;
            switch (ch)
            {
                case '-': s = x-y; break;
                case '+': s = x+y; break;
                case '*': s = x*y; break;
                case '/': s = x/y; break;
            }
            return s;
        }
    }
}