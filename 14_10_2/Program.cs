/*
Лабораторная работа №10
Выполнил: Журавлёв Е. А.
Задание №2.

1. Текст программы представляется в электронном виде и дол-
жен включать постановку задачи, сведения об авторе и подробные
комментарии.
2. Названия переменных и констант должны быть логически
обоснованы и давать понятие о том, какая информация в них пред-
ставлена, при создании метода его имя должно отражать его функци-
ональное назначение.
3. Программа должна запрашивать входные данные и выводить
итоговый результат с пояснениями.

Обеспечить выполнение поиска автора по произведению и
наоборот.
*/
using System.Runtime.Serialization.Formatters.Binary;
class Program
{
    public static void Main(string[] argv)
    {
        Bibliography b = new Bibliography();
        
        if (argv.Length == 1) LoadFromCSV (argv[0], ref b); //Дополняем словари из CSV файла
        Load("bibliography.dat", ref b);
        Dialog (ref b);
        Save("bibliography.dat", ref b);
    }
    ///<summary>Загрузка из типизированного файла</summary>
    static void Load(string fileName, ref Bibliography b)  
    {                                                   
        if (fileName.Length==0) return;
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(fileName))                      
            using (FileStream fs = new FileStream(fileName, FileMode.Open)) 
                b = (Bibliography) formatter.Deserialize(fs);
    }
    /// <summary>Сохранение в типизированный файл</summary>
    static void Save(string fileName, ref Bibliography b)
    { 
        if (fileName.Length==0) 
            return;          
        BinaryFormatter formatter = new BinaryFormatter();                      
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) 
            formatter.Serialize(fs, b);
    }
    /// <summary>Заполнение словарей из CSV - файла. разденение полей через ";"</summary>
    static void LoadFromCSV(string fileName, ref Bibliography b)
    {
        string line = string.Empty; // Буфер для подгружаемой строки
        string[] sublines;          // Буфер для разделённой по ";" строки
        try
        {
        StreamReader sr = new StreamReader (fileName);
        while (sr.Peek() > -1)
        {
            line = sr.ReadLine() ?? string.Empty;
            sublines = line.Split(';');
            if (sublines.Length==2) 
                b.Add(sublines[0],sublines[1]);
        }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл {0} не существует",fileName);
        }
    }
    static void Dialog(ref Bibliography b)
    {
        Console.WriteLine("Всего {0} авторов и {1} книг.",b.Count().AutorsCount,b.Count().BooksCount);
        string st = string.Empty; // Команда для пользовательского интерфейса
        string? autor; // Используется для ввода в поиск автора
        string? book;  // Используется для ввода в поиск названия книги
        while (st!="Х")
        {
            Console.Write("\n\t\"А\" - Поиск по автору\n\t\"К\" - Поиск по книге\n\t\"В\" - Показать всё\n\t\"Х\" - Выход\n\tВвод в русской раскладке.\n>");
            st = Console.ReadLine() ?? string.Empty;
            switch (st)
            {
                case "А":   Console.Write("Введите полное имя автора:");
                            autor = Console.ReadLine() ?? string.Empty;
                            if (b.GetBooksByAutor (autor,out List<string>? books))
                                {
                                    Console.WriteLine("\nОбнаружены следующие книги автора {0}:",autor);
                                    foreach (var s in books ?? new List<string> (){})
                                        Console.WriteLine("\t{0}",s ?? string.Empty);
                                }
                            else
                                Console.WriteLine ("\nИскомая информация не найдена.\n");
                            break;
                case "К":   Console.Write("Введите полное название книги:");
                            book = Console.ReadLine() ?? string.Empty;
                            if (b.GetAutorByBook (book,out autor))
                                Console.WriteLine("\nОбнаружен автор книги {1}: {0}",autor, book);
                            else
                                Console.WriteLine ("\nИскомая информация не найдена.\n");
                            break;
                case "В":   foreach (var a in b.ToList())
                                Console.WriteLine ("{0,30} - {1}",a.autor,a.book);
                            break;
            }
        }
    }
}

/// <summary>Класс организует работу со словарями авторов и книг</summary>
[Serializable]
class Bibliography
{
    /// <summary>словарь с ключом - авторами</summary>
    private Dictionary<string,List<string>> Autors = new Dictionary<string, List<string>>(){};
    /// <summary>словарь с ключом - названиями книг</summary>
    private Dictionary<string,string> Books  = new Dictionary<string, string>(){};
    /// <summary>Добавление новой записи об авторе и его книге</summary>
    public void Add (string autor, string book)
    {
        // Проверим наличие таких книг, или авторов в словарях
        if (!Autors.ContainsKey(autor))
        //Добавим отсутствующего автора
            Autors.Add(autor,new List<string>(){});
        if (!Books.ContainsKey(book)) 
        //Добавим отсутствующую книгу
            Books.Add(book, string.Empty);
        //Название книги теперь точно есть, добавим автора
        Books[book] = autor;
        //В словарь с авторами-ключами добавим список книг, если её ещё там нет.
        if (!Autors[autor].Contains(book)) 
            Autors[autor].Add(book);
    }
    /// <summary>Вывод в коллекцию полного перечня книг и авторов</summary>
    public List<(string autor,string book)> ToList()
    {
        List<(string,string)> list = new List<(string, string)> (){};
        foreach (var b in Books)
            list.Add((b.Value,b.Key));
        return list;
    }
    /// <summary>Найти автора по книге</summary>
    public bool GetAutorByBook (string book,out string? autor)
    {
        return Books.TryGetValue(book, out autor);
    }
    /// <summary>Найти коллекцию книг по автору</summary>
    public bool GetBooksByAutor (string autor,out List<string>? book)
    {
        return Autors.TryGetValue(autor, out book);
    }
    public (int AutorsCount,int BooksCount) Count()
    {
        return (Autors.Count, Books.Count);
    }
} 