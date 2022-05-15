/*
Лабораторная работа №10
Выполнил: Журавлёв Е. А.
Задание №1.

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
        ///<summary> Регулярное выражение для определения последовательности ЦелоеОперацияЦелое </summary>
        private static Regex RegExpr_SearchPattern = new Regex(@"\d*\d[\-\+\*\/]\d\d*",RegexOptions.Compiled);
        ///<summary> Регулярное выражение для определения Операции  </summary>
        private static Regex RegExpr_SearchOperation = new Regex(@"[\-\+\*\/]",RegexOptions.Compiled);

        static string fileName = "file.text"; // Имя файла задано по-умолчанию

        public static int Main(string[] args)
        {
            if (args.Length == 1) fileName = args[0];
            int calc;                           // Посчитанное значение из текущей строки
            int? max = null;                    // Максимальное число для сравнения
            int? maxStrNum = null;              // Номер строки с максимальным числом
            try                                 
            {
                StreamReader sr = new StreamReader (fileName);// Открываем файл для чтения
                for (int c=0; sr.Peek() > -1;c++)             // Читаем файл пока не вернёт символ конца файла
                    if (calcMaxSum(sr.ReadLine(),out calc))   // Проверяем, что в строке есть выражение и можно получить из него максимальное число
                        if (calc > max || max is null)        // Выявляем максимальное число и если такое число будет первым - запишем его. 
                        {
                            max = calc; 
                            maxStrNum = c;
                        }
                sr.DiscardBufferedData();
                sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                ConsoleColor color = Console.BackgroundColor;
                for (int c=0; sr.Peek() > -1;c++)
                {
                    if (c == maxStrNum) 
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(sr.ReadLine());
                    if (c == maxStrNum) 
                        Console.BackgroundColor = color;
                }
                sr.Close();
            }
            catch (FileNotFoundException) 
            {
                Console.WriteLine ("Файл '{0}' не существует в данном каталоге.",fileName); return -1;
            }
            return 0;
        }
        ///<summary> Вычисляем максимальное значение с ''Целыми'' по ''Операции'' по символьной строке </summary>
        ///<remarks> Возвращает True, когда в строке есть некоторое значение. В параметр output возвращается результат вычислений </remarks>
        private static bool calcMaxSum (string? str, out int output) // Возвращаемое булево значение упростит работу через if.
        {
            output = 0; // Выходной параметр
            List<int> sums = new List<int>(){}; // Будем использовать на случай, если в строке попадётся несколько операций
            List <Match> matches = RegExpr_SearchPattern.Matches(str ?? string.Empty).ToList();
            foreach (var match in matches)
            {
                Match operate = RegExpr_SearchOperation.Match (match.Value);
                int first   = int.Parse(match.Value.Substring(0,operate.Index));
                int second  = int.Parse(match.Value.Substring(operate.Index+1
                                                             ,match.Value.Length-operate.Index-1));
                char operat = match.Value[operate.Index];
                sums.Add (operation (first, second, operat));
            }
            if (sums.Count > 0) 
            {
                output = sums.Max(); 
                return true;
            } else 
            {
                return false;
            }
        }
        ///<summary> Вычисляем значение с ''Целыми'' по ''Операции'' </summary>
        ///<remarks> Возвращает целочисленный результат операции предопределённый символом</remarks>

        private static int operation (int x, int y, char ch) 
        {
            int s = 0; // Результат
            switch (ch)
            {
                case '-': s = x-y; break;
                case '+': s = x+y; break;
                case '*': s = x*y; break;
                case '/': s = x/y; break;
                default: break; 
            }
            return s;
        }
    }
}