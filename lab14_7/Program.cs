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

        public static void SimpleListener() //Прослушиваем запрос от браузера на указанный локальный порт 
        {               
            // !ОСТОРОЖНО! Метод GetContext будет останавливать выполнение кода, пока не придёт запрос.
            HttpListenerContext context = listener.GetContext(); // И вот пришёл запрос
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
            string responseString = MakeHTML();// responseString=responseString.Replace("'","\"");
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString); //Считаем число байт строки в нужной кодировке
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer,0,buffer.Length);
            output.Close();//Закрываем поток вывода
              
        }
        static void OpenUrl(string url) //Обеспечиваем кроссплатформенность запуска браузера по-умолчанию
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
                    {throw;} //А если платформа будет не Win/iOS/nix?
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

            listener.Prefixes.Add("http://*:65532/");   //Добавляем прослушываемые префиксы запросов
            listener.Start();                           //Слушаем согласно префиксам
            OpenUrl("http://localhost:65532/");    
            while (working) SimpleListener();           //Итерируем прослушивание пока не придёт working false
            listener.Stop();                            //Останавливаем прослушивание.

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
            <style>
            .tab {
                overflow: hidden;
                border: 1px solid #4CAF50;
                background-color: #C8E6C9;
            }
            .tab button {
                background-color: inherit;
                float: left;
                border: none;
                outline: none;
                cursor: pointer;
                padding: 14px 16px;
                transition: 0.3s;
            }
            .tab button:hover {
                background-color: #FFEB3B;
            }
            .tab button.active {
                background-color: #4CAF50;
            color: #fff;
            }
            .tabcontent {
                display: none;
                padding: 6px 12px;
                border: 1px solid #4CAF50;
                border-top: none;
            }
            </style>
            <script>
            function show (evt, cityName) {
                var i, tabcontent, tablinks;

                tabcontent = document.getElementsByClassName('tabcontent');
                for (i = 0; i < tabcontent.length; i++) {
                    tabcontent[i].style.display = 'none';
                }
                tablinks = document.getElementsByClassName('tablinks');
                for (i = 0; i < tablinks.length; i++) {
                    tablinks[i].className = tablinks[i].className.replace(' active', '');
                }
                document.getElementById(cityName).style.display = 'block';
                evt.currentTarget.className += ' active';
            }
            function closeWindow()   
            { 
                var xmlHttp = new XMLHttpRequest();

                xmlHttp.open(""GET"", ""exit"", false); // true for asynchronous 
                xmlHttp.send(null);
                window.close();

            }
            </script>
                <div class='tab'>
                    <button class='tablinks' onclick=""show(event, 'Главная')"">Вывод информации</button>
                    <button class='tablinks' onclick=""show(event, 'ДобавитьЗ')"">Добавить заезд</button>
                    <button class='tablinks' onclick=""show(event, 'Добавить')"">Добавить посещение</button>
                    <button class='tablinks' onclick=""show(event, 'Удалить')"">Удалить запись</button>
                    <button class='tablinks' onclick=""closeWindow()"">Выход</button>
                </div>
            <div id='Главная' class='tabcontent'>
            <table><tr><th>Дата</th><th>Музей</th><th>Цена</th></tr>";
        static string END = @"
            </div>
            <div id='ДобавитьЗ' class='tabcontent'>
            <form method='get' id='town' action='town'>
                    <br>
                    <label for='name'>Название города: </label>
                    <input name='name' value='Родной город'><br>
                    <label for='dat'>Дата заезда: </label>
                    <input name='dat' type='date' min='2000-01-01' max='2100-01-01' value='2022-01-01'><br>
                    <input type='submit' value='Добавить'><br>

            </form>  
            </div>
            <div id='Добавить' class='tabcontent'>
            <form method='get'id='museum' action='museum'>
                    <br>
                    <label for='name'>Название музея:</label>
                    <input name='name' value='Родной город'><br>
                    <label for='price'>Цена</label>
                    <input name='price' type='number' value='0' min='0' step='1'><br>
                    <label for='dat'>Дата посещения: </label>
                    <input name='dat' type='date' min='2000-01-01' max='2100-01-01' value='2022-01-01'><br>
                    <input type='submit' value='Добавить'><br>
    
            </form>
            </div>
            <div id='Удалить' class='tabcontent'>
            <form method='get' id='remove' action='remove'>
                    <br>
                    <input type='submit' value='Удаление'><br>
 
            </form>
            </div>
            </body>
            </html>"; 
    }
}
