using System.Globalization;
using WorkShop01.Backend;

class Program
{
    static void Main()
    {
       
        CultureInfo enUS = CultureInfo.GetCultureInfo("en-US");
        Thread.CurrentThread.CurrentCulture = enUS;
        Thread.CurrentThread.CurrentUICulture = enUS;

        try
        {
            Time t1 = new Time();
            Time t2 = new Time(14, 0, 0, 0);
            Time t3 = new Time(9, 34, 0, 0);
            Time t4 = new Time(19, 45, 56, 0);
            Time t5 = new Time(23, 3, 45, 678);

            List<Time> times = new List<Time>() { t1, t2, t3, t4, t5 };

            foreach (var t in times)
            {
                Console.WriteLine($"Time: {t}\n");

                Console.WriteLine($"Milliseconds:       {t.ToMiliseconds():N0}");
                Console.WriteLine($"Seconds     :       {t.ToSeconds():N0}");
                Console.WriteLine($"Minutes     :       {t.ToMinutes():N0}");
                Console.WriteLine($"Add         :       {t.Add(t3)}");
                Console.WriteLine($"Is Other day:       {t.IsOtherDay(t3)}");
                Console.WriteLine();
            }

            // Debe fallar
            var t6 = new Time(45, -1, 90, -87);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n{ex.Message}");
        }
    }
}
