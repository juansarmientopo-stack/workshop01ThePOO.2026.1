using System;
using System.Globalization;

namespace WorkShop01.Backend;

public class Time
{
    // Fields
    private int _hour;
    private int _minute;
    private int _second;
    private int _millisecond;

    // Constructors
    public Time()
    {
        _hour = 0;
        _minute = 0;
        _second = 0;
        _millisecond = 0;
    }

    public Time(int hour)
    {
        Hour = hour;
        Minute = 0;
        Second = 0;
        Millisecond = 0;
    }

    public Time(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
        Second = 0;
        Millisecond = 0;
    }

    public Time(int hour, int minute, int second)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = 0;
    }

    public Time(int hour, int minute, int second, int millisecond)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
    }

    // Properties (usan validadores privados al estilo del profe)
    public int Hour
    {
        get => _hour;
        set => _hour = ValidateHour(value);
    }

    public int Minute
    {
        get => _minute;
        set => _minute = ValidateMinute(value);
    }

    public int Second
    {
        get => _second;
        set => _second = ValidateSecond(value);
    }

    public int Millisecond
    {
        get => _millisecond;
        set => _millisecond = ValidateMillisecond(value);
    }

    // Methods requeridos
    // Nota: en tus capturas el nombre es "ToMiliseconds" (una sola 'l'); lo dejo así para que compile con tu Main.
    public long ToMiliseconds()
    {
        return ((long)Hour * 3600 + (long)Minute * 60 + Second) * 1000 + Millisecond;
    }

    public long ToSeconds()
    {
        // Totales enteros (como en la salida)
        return ((long)Hour * 3600) + ((long)Minute * 60) + Second;
    }

    public long ToMinutes()
    {
        // Totales enteros (como en la salida)
        return ((long)Hour * 60) + Minute;
    }

    public bool IsOtherDay(Time other)
    {
        if (other is null) return false;
        const long DayMs = 24L * 3600 * 1000;
        return (this.ToMiliseconds() + other.ToMiliseconds()) >= DayMs;
    }

    // Suma teniendo en cuenta milisegundos y “wrap” al siguiente día
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

    // (Opcional) por si en tu Main usan Add(h, m, s)
    public Time Add(int hours, int minutes, int seconds)
    {
        var temp = new Time(hours, minutes, seconds, 0);
        return Add(temp);
    }

    // ToString: “HH:MM:ss.mmm tt” en NO militar (12h con AM/PM), estilo de tu captura
    public override string ToString()
    {
        // 12h con AM/PM en mayúsculas; en-US asegura “AM/PM” y separador de miles con coma en el Main
        var dt = new DateTime(1, 1, 1, Hour, Minute, Second, Millisecond);
        return dt.ToString("hh':'mm':'ss'.'fff tt", CultureInfo.GetCultureInfo("en-US")).ToUpperInvariant();
    }

    // ---------- Validadores privados (estilo clase Date del profe) ----------
    private int ValidateHour(int hour)
    {
        if (hour < 0 || hour > 23)
            throw new ArgumentException($"The hour: {hour}, is not valid.");
        return hour;
    }

    private int ValidateMinute(int minute)
    {
        if (minute < 0 || minute > 59)
            throw new ArgumentException($"The minute: {minute}, is not valid.");
        return minute;
    }

    private int ValidateSecond(int second)
    {
        if (second < 0 || second > 59)
            throw new ArgumentException($"The second: {second}, is not valid.");
        return second;
    }

    private int ValidateMillisecond(int millisecond)
    {
        if (millisecond < 0 || millisecond > 999)
            throw new ArgumentException($"The millisecond: {millisecond}, is not valid.");
        return millisecond;
    }
}