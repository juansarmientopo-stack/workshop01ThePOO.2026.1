using WorkShop01Class;

try
{
    var t1 = new Time();
    var t2 = new Time(10);
    var t3 = new Time(9, 30);
    var t4 = new Time(3, 45, 50);
    var t5 = new Time(23, 12, 33);

    var times = new List<Time>() { t1, t2, t3, t4, t5 };

    foreach (var time in times)
    {
        Console.WriteLine($"Time: {time}");
        Console.WriteLine($"Time.ToMiliseconds(): {time.ToMiliseconds():0.15-NP}");
        Console.WriteLine($"Time.ToSeconds(): {time.ToSeconds():0.15-NP}");
        Console.WriteLine($"Time.ToMinutes(): {time.ToMinutes():0.15-NP}");
        Console.WriteLine($"Time.Add(3, 45, 50): {time.Add(3, 45, 50)}");
        Console.WriteLine($"Time.Add(Time t3): {time.Add(t3)}");
        Console.WriteLine($"IsOtherDay: {time.IsOtherDay(t4)}");
        Console.WriteLine();
    }

    var t6 = new Time(45, -1, 90, -87);
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
}
