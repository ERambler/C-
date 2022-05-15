/*
Вариант 14.
Задача 8.2

Необходимо выполнить следующие операции со структурами:
1) описать структуру согласно варианту (табл. 2);
2) объявить переменную структурного типа;
3) описать конструктор, инициализирующий поля структуры;
4) описать метод PrintStruct, который выводит на экран значе-
ния полей структуры в формате: <название поля> - <значение>;
5) инициализировать поля структуры значениями, введенными
с клавиатуры;
6) вывести поля структуры на экран с помощью PrintStruct.
*/
/*
Описать структуру : Подъёмный кран
Длина вылета — дистанция между пятой стрелы и осью обоймы поворотной части подъемного блока.
Вылет крюка — это дистанция между центральной осью поворотной части крана и вертикальной осью грузоподъемных блоков, когда кран смонтирован и установлен на ровной площадке.
Грузовая характеристика — зависимость величины грузоподъемности от длины вылета стрелового крана.
Грузовой момент — произведение показателя грузоподъемности на длину вылета стрелы крана.
Высота подъема крюка — расстояние от уровня стоянки крана до размещения грузозахватного блока в высшей точке в рабочем состоянии.
Глубина опускания — расстояние от уровня стоянки крана до грузозахватного блока в низшей точке в рабочем состоянии.
Скорость перемещения груза — считается как скорость поднятия или опускания рабочего груза, максимально допустимого для грузоподъемности крана в данных условиях.
*/
static class Program
{
    public struct Crane
    {
        private string Name;
        private float Departure_length;
        private float Hook_reach;
        private float Load_characteristic;
        private float Hook_lifting_height;
        private float Lowering_depth;
        private float Cargo_movement_speed;
        /// <summary> Структура "Подъёмный кран"</summary>
        /// <remarks></remarks>
        /// <param name="Name">Название</param>
        /// <param name="Departure_length">Длина вылета</param>
        /// <param name="Hook_reach">Вылет крюка.</param>
        /// <param name="Load_characteristic">Грузовая характеристика</param>
        /// <param name="Hook_lifting_height">Высота подъема крюка</param>
        /// <param name="Lowering_depth">Глубина опускания</param>
        /// <param name="Cargo_movement_speed">Скорость перемещения груза</param>
        public Crane (  string Name,
                        float Departure_length,
                        float Hook_reach,
                        float Load_characteristic,
                        float Hook_lifting_height,
                        float Lowering_depth,
                        float Cargo_movement_speed
                    )
        {
            this.Name = Name;
            this.Departure_length = Departure_length;
            this.Hook_reach = Hook_reach;
            this.Load_characteristic = Load_characteristic;
            this.Hook_lifting_height = Hook_lifting_height;
            this.Lowering_depth = Lowering_depth;
            this.Cargo_movement_speed = Cargo_movement_speed;            
        }
        public void PrintStruct()
        {
            List<(float value, string name)> list = new List<(float value, string name)> () 
                                            { 
                                                (this.Departure_length,"Длина вылета"),
                                                (this.Hook_reach,"Вылет крюка"),
                                                (this.Load_characteristic,"Грузовая характеристика"),
                                                (this.Hook_lifting_height,"Высота подъема крюка"),
                                                (this.Lowering_depth,"Глубина опускания"),
                                                (this.Cargo_movement_speed,"Скорость перемещения груза"),
                                            };
            Console.WriteLine ($"{"Параметр", 30} - {"Значение",10}");
            Console.WriteLine ($"{"Название", 30} - {this.Name, 10}");
            foreach (var i in list) Console.WriteLine ($"{i.name, 30} - {i.value, 10}");
        }
    }
    static void input (out float inputValue, string printString)
    {
        Console.Write (printString);
        try
        {
            inputValue = float.Parse (Console.ReadLine() ?? string.Empty);
        }
        catch (FormatException)
        {
            Console.WriteLine ("Исключительная ситуация: пустое значение либо неверный символ. Повторите ввод.");
            input (out inputValue, printString);
        }
    }
    static void Main()
    {
        Console.Write ("Название: ");
            string Name = Console.ReadLine() ?? string.Empty;
        input (out float a, "Длина вылета: ");
        input (out float b, "Вылет крюка: ");
        input (out float c, "Грузовая характеристика: ");
        input (out float d, "Высота подъема крюка: ");
        input (out float e, "Глубина опускания: ");
        input (out float f, "Скорость перемещения груза: ");
        Crane crane = new Crane(Name,a,b,c,d,e,f);

        //Console.Clear();

        crane.PrintStruct();

        Console.ReadLine();
    }
}
