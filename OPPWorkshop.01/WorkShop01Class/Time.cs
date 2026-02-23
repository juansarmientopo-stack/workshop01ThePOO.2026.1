using System;
using System.Globalization;

namespace WorkShop01Class;

public class Time
{
    private int _hour;
    private int _millisecond;
    private int _minute;
    private int _second;

    // 1) sin parámetros
    public Time() : this(0, 0, 0, 0) { }

    // 2) con horas
    public Time(int hour) : this(hour, 0, 0, 0) { }

    // 3) con horas y minutos
    public Time(int hour, int minute) : this(hour, minute, 0, 0) { }

    // 4) con horas, minutos y segundos
    public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }

    // 5) con horas, minutos, segundos y milisegundos
    public Time(int hour, int minute, int second, int millisecond)
    {
        ValidateRange(hour, minute, second, millisecond);
        _hour = hour;
        _minute = minute;
        _second = second;
        _millisecond = millisecond;
    }

    // Validaciones con mensajes que coinciden con el ejemplo de la profe
    private static void ValidateRange(int hour, int minute, int second, int millisecond)
    {
        if (hour < 0 || hour > 23)
            throw new ArgumentException($"The hour: {hour}, is not valid.");
        if (minute < 0 || minute > 59)
            throw new ArgumentException($"The minute: {minute}, is not valid.");
        if (second < 0 || second > 59)
            throw new ArgumentException($"The second: {second}, is not valid.");
        if (millisecond < 0 || millisecond > 999)
            throw new ArgumentException($"The millisecond: {millisecond}, is not valid.");
    }

    public int Hour
    {
        get => _hour;
        set
        {
            if (value < 0 || value > 23)
                throw new ArgumentException($"The hour: {value}, is not valid.");
            _hour = value;
        }
    }

    public int Millisecond
    {
        get => _millisecond;
        set
        {
            if (value < 0 || value > 999)
                throw new ArgumentException($"The millisecond: {value}, is not valid.");
            _millisecond = value;
        }
    }

    public int Minute
    {
        get => _minute;
        set
        {
            if (value < 0 || value > 59)
                throw new ArgumentException($"The minute: {value}, is not valid.");
            _minute = value;
        }
    }

    public int Second
    {
        get => _second;
        set
        {
            if (value < 0 || value > 59)
                throw new ArgumentException($"The second: {value}, is not valid.");
            _second = value;
        }
    }

    // ---------- Métodos requeridos ----------
    // ¡OJO! El nombre de tu Main usa "ToMiliseconds" (una sola 'l').
    public long ToMiliseconds()
    {
        return ((long)_hour * 3600 + (long)_minute * 60 + _second) * 1000 + _millisecond;
    }

    public long ToSeconds()
    {
        // Totales, sin decimales (como se ve en tu consola)
        return ((long)_hour * 3600) + ((long)_minute * 60) + _second;
    }

    public long ToMinutes()
    {
        return ((long)_hour * 60) + _minute;
    }

    // ¿Pasa al siguiente día al sumar con 'other'?
    public bool IsOtherDay(Time other)
    {
        if (other is null) return false;
        const long DayMs = 24L * 3600 * 1000;
        return (this.ToMiliseconds() + other.ToMiliseconds()) >= DayMs;
    }

    // Suma con otro Time (incluyendo milisegundos) y hace wrap al día siguiente
    public Time Add(Time other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));
        const long DayMs = 24L * 3600 * 1000;
        long total = (this.ToMiliseconds() + other.ToMiliseconds()) % DayMs;

        int h = (int)(total / 3_600_000L);
        total %= 3_600_000L;
        int m = (int)(total / 60_000L);
        total %= 60_000L;
        int s = (int)(total / 1_000L);
        int ms = (int)(total % 1_000L);

        return new Time(h, m, s, ms);
    }

    // (Opcional) Suma h:m:s (sin ms), útil si tu Program llama Add(h,m,s)
    public Time Add(int hoursToAdd, int minutesToAdd, int secondsToAdd)
    {
        var temp = new Time(hoursToAdd, minutesToAdd, secondsToAdd, 0);
        return Add(temp);
    }

    // Formato pedido: HH:MM:ss.mmm tt en NO militar (12h con AM/PM)
    public override string ToString()
    {
        // Usamos un DateTime "ficticio" para formatear a 12h
        var dt = new DateTime(1, 1, 1, Hour, Minute, Second, Millisecond);
        // "hh" = 12h, "tt" = AM/PM; en-US lo pone en mayúsculas
        return dt.ToString("hh':'mm':'ss'.'fff tt", CultureInfo.GetCultureInfo("en-US")).ToUpperInvariant();
    }
}
``