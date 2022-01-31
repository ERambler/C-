/*
Вариант 14 Журавлёв

Человек путешествует по городам и посещает музеи. Человек от-
мечает посещенные города, музеи и время посещения. Программа 
по  указанию  временного  интервала  распечатывает  посещенные 
города  и  музеи  с  возможностью  получения  оплаченной  стоимо-
сти и длительности посещения. 
*/

using System;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace lab14_6
{
    class Program
    {
        static SortedDictionary<DateTime, string> towns = new SortedDictionary<DateTime, string>();
        static SortedDictionary<DateTime, string> museums = new SortedDictionary<DateTime, string>();
        static SortedDictionary<DateTime, int> pays = new SortedDictionary<DateTime, int>();
        static Dictionary<string,string> GET = new Dictionary<string, string>();
        static bool working = true; // Переменная для главного цикла.

        static HttpListener listener = new HttpListener();

        public static void SimpleListener() //Прослушивание и разбор запроса от браузера
        {               
            // Внимание. Метод GetContext будет останавливать выполнение кода, пока не придёт запрос.
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            // Обрабатываю запрос.
                    string lastPart = "";
                    foreach (string a in request.Url.Segments) lastPart=a;
                    switch (lastPart) 
                    {
                        case "exit"  : working=false; break;
                        case "town"  : towns.TryAdd(DateTime.Parse(request.QueryString.Get(1)),request.QueryString.Get(0)); break;//Console.WriteLine($"-=={request.QueryString.Get(1)}=--");
                        case "museum": museums.TryAdd(DateTime.Parse(request.QueryString.Get(2)),request.QueryString.Get(0));
                                       pays.TryAdd(DateTime.Parse(request.QueryString.Get(2)),int.Parse(request.QueryString.Get(1)));break;
                        case "remove": break;
                    }
                    
                    //GET.Clear();
                    for (int c=0;c<request.QueryString.AllKeys.Length;c++)
                    {
                    //    if (request.QueryString.Get(c)!=null)
                    //        GET.TryAdd(request.QueryString.GetKey(c),request.QueryString.Get(c));
                        Console.WriteLine($"{request.QueryString.GetKey(c)} == {request.QueryString.Get(c)}");
                    }
                    
                    /*foreach(KeyValuePair<string, string> req in GET )
                    {
                        Console.WriteLine($"{req.Key}\t= {req.Value}");
                    }*/
            // Формирую ответ.
            HttpListenerResponse response = context.Response;
            string responseString = MakeHTML(); responseString=responseString.Replace("'","\"");
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString); //Считет число байт строки в нужной кодировке
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer,0,buffer.Length);
            output.Close();//Закрываю поток вывода
              
        }
        static void OpenUrl(string url) //Нужно для открытия браузера на разных платформах.
            {
                try
                {Process.Start(url);}
                catch
                {// hack because of this: https://github.com/dotnet/corefx/issues/10361
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {url = url.Replace("&", "^&");
                        Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });}
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {Process.Start("xdg-open", url);}
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {Process.Start("open", url);}
                    else
                    {throw;}
                }
            }
        static void Main(string[] args)
        {   

            //towns.Add(DateTime.Parse("1/1/1",System.Globalization.CultureInfo.InvariantCulture),"");
            towns.Add(DateTime.Parse("5/1/2008 12:25",System.Globalization.CultureInfo.InvariantCulture),"York");
            towns.Add(DateTime.Parse("5/1/2010",System.Globalization.CultureInfo.InvariantCulture),"Work");
            towns.Add(DateTime.Parse("5/1/2009",System.Globalization.CultureInfo.InvariantCulture),"Kork");
            museums.Add(DateTime.Parse("5/2/2008 13:25",System.Globalization.CultureInfo.InvariantCulture),"YorkMus");
            museums.Add(DateTime.Parse("5/3/2009",System.Globalization.CultureInfo.InvariantCulture),"WorkMus");
            museums.Add(DateTime.Parse("5/5/2009",System.Globalization.CultureInfo.InvariantCulture),"KorkMus");
            pays.Add(DateTime.Parse("5/2/2008 13:25",System.Globalization.CultureInfo.InvariantCulture),125);
            pays.Add(DateTime.Parse("5/3/2009",System.Globalization.CultureInfo.InvariantCulture),275);
            pays.Add(DateTime.Parse("5/5/2009",System.Globalization.CultureInfo.InvariantCulture),600);

            listener.Prefixes.Add("http://*:65532/");   // Добавляю прослушываемые префиксы запросов
            listener.Start();                           //Слушаю согласно префиксам
            OpenUrl("http://localhost:65532/");    
            while (working) SimpleListener();           //Итерирую прослушивание пока не придёт working false
            listener.Stop();                            //Останавливаю прослушивание.

        }
        static string MakeHTML ()                       //Формирование string для страницы. Тут же и обработка словарей и прочие вычисления. Метод нарушает хороший тон, но тут так будет удобнее.
        {
            string HTML_OUT=""; HTML_OUT+=HEAD;         // Шапка страницы

            DateTime lowerDate = new DateTime(2000,1,1),// По-умолчанию сформирую ответ по всему возможному диапазону.
                     higherDate= new DateTime(3000,1,1);
            int townPay=0, totalPay=0,counter=0;
            string town="";
            DateTime[] visits = new DateTime[towns.Count];
            towns.Keys.CopyTo(visits,0);
            DateTime oldVisit=visits[0].AddDays(-1);
            foreach(KeyValuePair<DateTime, string> kvp in museums )

            {
                if (kvp.Key >= lowerDate && 
                    kvp.Key <= higherDate)
                {   
                    foreach (DateTime newVisit in visits) 
                    {if (newVisit > oldVisit && newVisit < kvp.Key)
                        {
                            totalPay+=townPay; town=""; counter++;
                            towns.TryGetValue(oldVisit,out town);
                            Console.WriteLine($"\n{town} -- {townPay} -- {counter}");
                                if (town!=null) 
                                        HTML_OUT+=$"\n<tr><td>{town} Сумма:{townPay}</tr></td>"; //<<<<<<
                            townPay=0;oldVisit=newVisit;
                            break;
                        }
                    }
                    townPay+=pays[kvp.Key];
                    Console.Write($"\nKey = {kvp.Key}, Value = {kvp.Value}");
                    Console.Write($", pay = {pays[kvp.Key]}");
                        HTML_OUT+=$"\n<tr><td>{kvp.Key:d}</td><td>{kvp.Value}</td><td>{pays[kvp.Key]}</td></tr>";//<<<<<<
                }
            }
            totalPay+=townPay; town=""; 
            towns.TryGetValue(visits[counter],out town);
            Console.WriteLine($"\n{town} -- {townPay} -- {counter}");
            Console.WriteLine($"\nTOTAL:{totalPay}");
                        HTML_OUT+=$"\n<tr><td>{town} Сумма:{townPay}</tr></td>"; //<<<<<<
                        HTML_OUT+=$"</table><tr><td>ИТОГО:{totalPay}</tr></td>";
                        HTML_OUT+=END;
            return HTML_OUT;
            
        }
        static string HEAD = @"
        <!DOCTYPE HTML>
        <html> 
            <head> <meta charset='utf-8'>  <title>Программа: Музейный турист</title></head>
            <body>
            <table><tr><th>Дата</th><th>Музей</th><th>Цена</th></tr>";
        static string END = @"
        <form method='get' id='town' action='town'>
            <fieldset>
            <legend>Добавить заезд</legend>
                <label for='name'>Название города: </label>
                <input name='name' value='Родной город'>
                <label for='dat'>Дата заезда: </label>
                <input name='dat' type='date' min='2000-01-01' max='2100-01-01' value='2022-01-01'>
                <input type='submit' value='Добавить'><br>
            </fieldset>
        </form>    
        
        <form method='get'id='museum' action='museum'>
            <fieldset>
            <legend>Добавить посещение музея</legend>
                <label for='name'>Название музея:</label>
                <input name='name' value='Родной город'>
                <label for='price'>Цена</label>
                <input name='price' type='number' value='0' min='0' step='0.01'>
                <label for='dat'>Дата посещения: </label>
                <input name='dat' type='date' min='2000-01-01' max='2100-01-01' value='2022-01-01'>
                <input type='submit' value='Добавить'><br>
            </fieldset>        
        </form>
        <form method='get' id='remove' action='remove'>
            <fieldset>
            <legend>Удаление</legend>
                <input type='submit' value='Удаление'><br>
            </fieldset>        
        </form>
        <form action='exit' id='exit'>
            <fieldset>
            <legend>Выход</legend>
                <input type='submit' value='Выход'><br>
            </fieldset>        
        </form>
        <p></p>
        </body>
        </html>"; 
    }
}
