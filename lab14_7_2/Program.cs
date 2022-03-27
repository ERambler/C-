/*
Вариант 14 Журавлёв
Человек путешествует по городам и посещает музеи. Человек от-
мечает посещенные города, музеи и время посещения. Программа
по указанию временного интервала распечатывает посещенные
города и музеи с возможностью получения оплаченной стоимо-
сти и длительности посещения.*/

using System;
using System.Threading;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace lab14_7_2
{
    ///<summary>Текстовой пользовательский интерфейс</summary>
    class TextUserInterface
    {
        static int TableLength = 0; //В этом поле мы сохраняем длину таблицы.
        public static int[] FieldsWidth = {10,16,25,6};
        ///<summary>Меню параметров. Главный цикл.</summary>
        public static void Loop (ref Trips travel)
        {
            Console.Clear();

            bool loop = true;
            int activeInput = 0;
            ConsoleKeyInfo cki;

            ShowTable (ref travel, activeInput);
            Console.CursorVisible = false;
            do 
            {   
                if (Console.KeyAvailable) 
                {
                    cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case (ConsoleKey.DownArrow) : activeInput++; break;
                        case (ConsoleKey.UpArrow)   : activeInput--; break;
                        case (ConsoleKey.F6)        : Filter(ref travel) ;break;
                        case (ConsoleKey.F5)        : Add(ref travel);break;
                        case (ConsoleKey.F8)        : DeleteRecord (ref travel,activeInput); break; 
                        case (ConsoleKey.Escape)    : loop=false; break;
                    }
                    if (activeInput<0) activeInput=TableLength-1; if (activeInput>TableLength-1) activeInput=0;
                ShowTable (ref travel, activeInput);
                }
                    Thread.Sleep(15);
            } while(loop);
            Console.Clear();
            return;
        }
        ///<summary>Фильтр дат</summary>
         private static void Filter (ref Trips travel)
         {  
            
            Console.SetCursorPosition(0, Console.WindowHeight-6);           //Очищаем строку для ввода
            Console.WriteLine(new String((Char)32, Console.WindowWidth));

            string label = "Вводите поочерёдно дату начала периода, дату окончания периода.";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (label.Length / 2), Console.WindowHeight-3);Console.Write(label);
            Console.CursorVisible = true;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, Console.WindowHeight-6);

            if (DateTime.TryParse(Console.ReadLine(),out DateTime newfirstdate))
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - 0, Console.WindowHeight-6);
                if (DateTime.TryParse(Console.ReadLine(),out DateTime newseconddate))
                {
                    travel.FilterDate.LowerDate = newfirstdate;
                    travel.FilterDate.HighterDate = newseconddate;
                    label = "Установлен фильтр дат с "+newfirstdate.ToString("dd.MM.yyyy"+" по "+ newseconddate.ToString("dd.MM.yyyy"));
                }
                else  
                {
                    label = "Ошибка при вводе даты! Возврат в главное меню! Фильтр дат сброшен.";
                    travel.FilterDate.LowerDate = new DateTime(1,1,1); travel.FilterDate.HighterDate = new DateTime(9999,12,31);
                }
            }
            else 
            {
                label = "Ошибка при вводе даты! Возврат в главное меню! Фильтр дат сброшен.";
                travel.FilterDate.LowerDate = new DateTime(1,1,1); travel.FilterDate.HighterDate = new DateTime(9999,12,31);
            }

            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth / 2) - (label.Length / 2), Console.WindowHeight-6);
            Console.Write(label); 
        } 
         ///<summary>Удаление записи</summary>
        private static void DeleteRecord (ref Trips travel, int activeIndex)
        {   
            if (travel.IndexToKeyArray().Length==0) return;             //Если массив пуст - выходим отсюда.
            Console.SetCursorPosition(0, Console.WindowHeight-6);       //Очищаем строку для ввода
            Console.WriteLine(new String((Char)32, Console.WindowWidth));
            string label = "Вы действительно желаете удалить запись? (Y/N)";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (label.Length / 2), Console.WindowHeight-6);Console.Write(label);
            ConsoleKeyInfo cki;
            bool loop = true;
            do 
            {   
                if (Console.KeyAvailable) 
                {
                    cki = Console.ReadKey(true);

                    if  (cki.Key == ConsoleKey.Y)
                    {
                        travel.DeleteByKey(travel.IndexToKeyArray()[activeIndex]);
                        loop=false;
                    }
                    if  (cki.Key == ConsoleKey.N) loop=false;
                }
                Thread.Sleep(15);
            } while (loop);
            Console.SetCursorPosition(0, Console.WindowHeight-6);           //Очищаем строку для ввода
            Console.WriteLine(new String((Char)32, Console.WindowWidth));            
        }

         ///<summary>Печать таблицы с подсветкой выбранной строки</summary>
        private static void ShowTable (ref Trips travel, int activeIndex)
        {
            string label = string.Empty;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, 2);
            Console.Write("┌"+new String('─', 10)+"┬"+new String('─', 16)+"┬"+new String('─', 25)+"┬"+new String('─', 6)+"┐");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, 3);
            Console.Write($"│{"Дата",10}│{"Город",16}│{"Музей",25}│{"Цена",6}│");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, 4);
            Console.Write("├"+new String('─', 10)+"┼"+new String('─', 16)+"┼"+new String('─', 25)+"┼"+new String('─', 6)+"┤");

            ConsoleColor DefaultColor = Console.BackgroundColor;
            travel.ToStringArray(out string[,] table, out decimal total);
            TableLength = table.GetLength(0);
            int i = 0;
            for (; i < TableLength; i++)
            {
                if (table[i,1]!=string.Empty) Console.BackgroundColor = ConsoleColor.DarkRed;
                if (i==activeIndex)  Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition((Console.WindowWidth / 2) - 30, i+5);
                Console.Write("│{0,10}│{1,16}│{2,25}│{3,6}│",table[i,0],
                                                             table[i,1].Substring(0,(table[i,1].Length>15) ? 16 : table[i,1].Length),
                                                             table[i,2].Substring(0,(table[i,2].Length>24) ? 25 : table[i,2].Length),
                                                             table[i,3]);
                if (i==activeIndex || table[i,1]!=string.Empty)  Console.BackgroundColor = DefaultColor;
            }
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, i+5);
            Console.Write("├"+new String('─', 10)+"┴"+new String('─', 16)+"┴"+new String('─', 25)+"┴"+new String('─', 6)+"┤");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, i+6); 

            Console.Write("│ИТОГО: {0,53}│",total);                   
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, i+7);
            Console.Write("└"+new String('─', 60)+"┘");
            
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, i+8); //Добавим пустую строку, чтобы при удалении елемента 
            Console.WriteLine(new String((Char)32, Console.WindowWidth));   //избежать задваивания последней строки в таблице

            //Возвращаем подвал на место
            Console.SetCursorPosition(0, Console.WindowHeight-3);       //Очищаем строку для ввода
            Console.WriteLine(new String((Char)32, Console.WindowWidth));
            Console.SetCursorPosition(0, Console.WindowHeight-1);       //Очищаем строку для ввода
            Console.WriteLine(new String((Char)32, Console.WindowWidth));
            label = "   ↑ ↓ - Указать запись; F8 - Удалить запись;";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (label.Length / 2), Console.WindowHeight-3);Console.Write(label);
            label = "   F5 - Добавить запись; F6 - Диапазон дат; Esc - Выход   ";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (label.Length / 2), Console.WindowHeight-1);Console.Write(label);
            Console.CursorVisible = false;
        }
        ///<summary>Добавление записи</summary>
         private static void Add (ref Trips travel)
         {  
            
            Console.SetCursorPosition(0, Console.WindowHeight-6);           //Очищаем строку для ввода
            Console.WriteLine(new String((Char)32, Console.WindowWidth));

            string label = "Вводите поочерёдно дату, название города, музея, сумму.";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (label.Length / 2), Console.WindowHeight-3);Console.Write(label);
            label = "Ввод названия музея и суммы оплаты будут доступны, если название города пусто:";
            Console.SetCursorPosition((Console.WindowWidth / 2) - (label.Length / 2), Console.WindowHeight-1);Console.Write(label);label=string.Empty;
            
            //Нарисуем табличку, чтобы пользователю порадовать глаза
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, Console.WindowHeight-8);
            Console.Write("┌"+new String('─', 10)+"┬"+new String('─', 16)+"┬"+new String('─', 25)+"┬"+new String('─', 6)+"┐");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, Console.WindowHeight-7);
            Console.Write($"│{"Дата:",10}│{"Город:",16}│{"Музей:",25}│{"Цена:",6}│");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, Console.WindowHeight-6);
            Console.Write("│"+new String(' ', 10)+"│"+new String(' ', 16)+"│"+new String(' ', 25)+"│"+new String(' ', 6)+"│");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, Console.WindowHeight-5);
            Console.Write("└"+new String('─', 10)+"┴"+new String('─', 16)+"┴"+new String('─', 25)+"┴"+new String('─', 6)+"┘");
            Console.CursorVisible = true;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 29, Console.WindowHeight-6);
            if (DateTime.TryParse(Console.ReadLine(),out DateTime newdate)) //Вводим ключ
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) - 18, Console.WindowHeight-6);
                string newcity = Console.ReadLine(); //Значение может оказаться пустым
                if (newcity==string.Empty)           //В коде мы будем использовать это
                {                                    //Для управления.
                    Console.SetCursorPosition((Console.WindowWidth / 2) - 1, Console.WindowHeight-6); //Не забываем указывать позицию курсора
                    string newmuseum = Console.ReadLine();
                    if (newmuseum!=string.Empty)
                    {
                        Console.SetCursorPosition((Console.WindowWidth / 2) + 25, Console.WindowHeight-6);
                        if (decimal.TryParse(Console.ReadLine(),out decimal newcash))
                        {
                            travel.AddOrEditByKey(newdate,newmuseum,newcash);
                        }
                        else
                        {
                            label = "Неверный ввод числового значения! Возврат в главное меню!";
                        }
                    }
                    else
                    {
                        label = "Не введены ни название музея, ни города. Возврат в главное меню";
                    }
                }
                else    //Здесь мы имеем пару: ключ и имя города. Пишем!
                {
                    travel.AddOrEditByKey(newdate,newcity);
                }
            }
            else
            {
                label = "Ошибка при вводе даты! Возврат в главное меню!";
            }
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, Console.WindowHeight-8);
            Console.WriteLine(new String((Char)32, Console.WindowWidth));
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, Console.WindowHeight-7);
            Console.WriteLine(new String((Char)32, Console.WindowWidth));
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, Console.WindowHeight-6);
            Console.WriteLine(new String((Char)32, Console.WindowWidth));
            Console.SetCursorPosition((Console.WindowWidth / 2) - 30, Console.WindowHeight-5);
            Console.WriteLine(new String((Char)32, Console.WindowWidth));
            Console.SetCursorPosition((Console.WindowWidth / 2) - (label.Length / 2), Console.WindowHeight-6);Console.Write(label);


        } 
    }
    [Serializable]    
    /// <summary>Поездки, платежи и музеи, связанные датами-ключами</summary>
    class Trips
    {
        /// <summary>Даты для фильтра</summary>
        public (DateTime LowerDate, DateTime HighterDate) FilterDate = (new DateTime (1,1,1), new DateTime (9999,12,31));
        /// <summary>Словарь посещённых городов</summary>
        public Dictionary <DateTime,string>  Cities  = new Dictionary<DateTime,string>();
        /// <summary>Словарь посещённых музеев</summary>
        public Dictionary <DateTime,string>  Museums = new Dictionary<DateTime,string>();
        /// <summary>Словарь оплаченных сумм</summary>
        public Dictionary <DateTime,decimal> Payments= new Dictionary<DateTime,decimal>();
        /// <summary>Изменение или добавление значения по ключу для словаря городов</summary>
        /// <param name="key">Ключ</param>
        /// <param name="newValueey">Название города</param>
        public void AddOrEditByKey (DateTime key,string newValue)
        {
            if (!Cities.TryAdd(key,newValue))
            {
                Cities.Remove(key);
                Cities.TryAdd(key,newValue);
            }
        }
        /// <summary>Изменение значения по ключу для словаря музеев и оплаты</summary>
        /// <param name="key">Ключ</param>
        /// <param name="newValueey">Название музея</param>
        /// <param name="cash">Цена посещения</param>
        public void AddOrEditByKey (DateTime key,string newValue, decimal cash)
        {
            if (!Museums.TryAdd(key,newValue))
            {
                Museums.Remove(key);
                Museums.Add(key,newValue);
            }
            if (!Payments.TryAdd(key,cash))
            {
                Payments.Remove(key);
                Payments.Add(key,cash);
            }
        }
        /// <summary>Удаление значнеия по ключу</summary>
        public void DeleteByKey (DateTime key)
        {
            if (!Cities.Remove(key))
            {
                Museums.Remove(key);
                Payments.Remove(key);
            }
        }        
        /// <summary>Упорядоченный массив ключей</summary>
        public DateTime[] IndexToKeyArray()
        {
            var visited = new DateTime[Cities.Count+Museums.Count];
            Cities.Keys.CopyTo (visited,0);
            Museums.Keys.CopyTo(visited,Cities.Count-1 < 0 ? 0 : Cities.Count); //При пустом словаре городов получится -1. Тернарным оператором исправим это.
            var result = from date in visited                                                  //Запрос через Linq. Это будет намного удобнее, чем то же отбирать классическим алгоритмом 
                         where date >= FilterDate.LowerDate && date <= FilterDate.HighterDate  //Немного linq облегчит нам существование:
                         select date;
                result = result.OrderBy(x=>x.Date);
            return result.ToArray();
        }
        /// <summary>Вывод данных ввиде таблицы</summary>
        public void ToStringArray(out string [,] strings_array, out decimal total)
        {   
            string[,] array = new string[IndexToKeyArray().GetLength(0),4]; //Очень неприятное и узкое место, 
            decimal sum = 0, totalsum = 0;  int lastcity = 0;               //которое выпоняется дважды первый раз тут,
            
            DateTime[] visited = IndexToKeyArray();                         //Второй раз ТУТ. Неплохо бы это переписать по-другому.

            for (int i=0; i<visited.Length;i++)
            {
                array[i,0]=visited[i].ToString("dd.MM.yyy");
                if (Cities.TryGetValue(visited[i],out string place)) 
                {
                    array[i,1]=place;
                    array[i,2] = string.Empty;          //Пустое значение явно укажем, оно позднее будет использоваться для подсветки строк с промежуточным итогом
                    array[lastcity,3]=sum.ToString();  
                    lastcity=i;
                    sum=0;
                }
                else    //Если ключа нет в словаре посещённых городов, значит это точно ключ посещённых музеев и платежа
                {
                    array[i,2] = Museums[visited[i]];
                    array[i,3] = Payments[visited[i]].ToString();
                    array[i,1] = string.Empty;
                      sum += Payments[visited[i]]; 
                      totalsum += Payments[visited[i]]; 
                };
            }
            array[lastcity,3]=sum.ToString();
            strings_array = array;
            total = totalsum;
            //return array;
        }       
    }
    class Program
    {
        static void Main(string[] args)
        { 
            Trips myTravel = new Trips();
            Load("traveler.dat", ref myTravel);
            TextUserInterface.Loop(ref myTravel);
            Save("traveler.dat", ref myTravel);
        }
        /// <summary>Загрузка из сериализованного файла</summary>
        static void Load(string FileName, ref Trips travel)  //Официально MS считает это небезопасно, так как позволяет выполнить вредоносный код
        {                                                    //Однако это настолько удобно, что я просто не могу отказаться от такой возможности.
            if (FileName.Length==0) return;
            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(FileName))                      // Если файл существует, а если нет - ничего не делаем.
                using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate)) 
                    travel = (Trips) formatter.Deserialize(fs);
        }
        /// <summary>Сохранение в сериализованный файл</summary>
        static void Save(string FileName, ref Trips travel)
        { 
            if (FileName.Length==0) return;          
            BinaryFormatter formatter = new BinaryFormatter();                      // создаем объект BinaryFormatter
            using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate)) // получаем поток, куда будем записывать сериализованный объект
                formatter.Serialize(fs, travel);
        }
    }   
}

