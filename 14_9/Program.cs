/*
Для выполнения лабораторной работы необходимо написать
программу на языке C#, которая будет демонстрировать работу соз-
данных методов.
В работе необходимо описать два метода, которые реализуют
один и тот же алгоритм, описанный в списке заданий. Первый метод
для возврата результата должен использовать имя метода, а второй
должен возвращать результат через параметры.
Обязательные требования, предъявляемые к созданию методов.
1. Входные данные необходимо передавать через параметры.
2. Названия переменных, констант и методов должны быть
логически обоснованы и давать понятие о том, что в них
предполагается хранить или обрабатывать.
3. Программа должна запрашивать входные данные и выводить
итоговый результат с пояснениями.

вычисляет необходимое время для передачи файла; известна
скорость сети в Кбит/с, размер файла, который необходимо
пере-править по сети, а также известно, что каждый четвертый
пакет размером в 1 байт теряется в сети;

*/

class Program
{
    /// <summary>Возвращает необходимое время для передачи файла</summary>
    /// <remarks>Возвращается float в секундах </remarks>
    /// <param name="connectionSpeed_Kbps">Скорость соединения в Кбит/с</param>
    /// <param name="fileSize_B">Объём файла в байтах</param>
    /// <param name="lostPart">Часть потеряных пакетов</param>
    static float ReceiveTime (long connectionSpeed_Kbps
                            , long fileSize_B
                            , float lostPart = .25f)
    {
        long fileSize_b = fileSize_B * 1024;
        float time = fileSize_b / (connectionSpeed_Kbps*1000);
        return time - (lostPart * time);
    }
    /// <summary>Возвращает необходимое время для передачи файла</summary>
    /// <remarks>Возвращается float в out параметр receiveTime в секундах </remarks>
    /// <param name="connectionSpeed_Kbps">Скорость соединения в Кбит/с</param>
    /// <param name="fileSize_B">Объём файла в байтах</param>
    /// <param name="lostPart">Часть потеряных пакеток</param>
    /// <param name="receiveTime">Необходимое время для передачи файла</param>
    static void ReceiveTime (long connectionSpeed_Kbps
                           , long fileSize_B
                           , out float receiveTime
                           , float lostPart = .25f)
    {
        long fileSize_b = fileSize_B * 1024;
        float time = fileSize_b / (connectionSpeed_Kbps * 1000);
        receiveTime = time - (lostPart * time);
    }

    static void Main ()
    {
        float result; 
        Console.Clear();
        Console.Write ("Скорость соединения (Кбит/с): ");
        long connectionSpeed = long.Parse (Console.ReadLine());
        Console.Write ("Размер файла (байт): ");
        long fileSize = long.Parse (Console.ReadLine());



        result = ReceiveTime(connectionSpeed,fileSize);
        Console.WriteLine ($"возвращение результата через имя метода:\n\tВремя передачи с учётом потерь составит {result} сек. ");


        ReceiveTime(connectionSpeed,fileSize, out result);
        Console.WriteLine ($"Возвращение результата через параметры:\n\tВремя передачи с учётом потерь составит {result} сек.");
    }
}