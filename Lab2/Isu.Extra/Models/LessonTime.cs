using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class LessonTime
{
    private int _day;
    private int _index;

    public LessonTime(int day, int index)
    {
        ValidateClassTime(day, index);
        _day = day;
        _index = index;
    }

    public bool Compare(LessonTime other)
    {
        if (other == null)
            throw new ArgumentNullException();

        return (Day() == other.Day()) && (StartTime() == other.StartTime());
    }

    public string Day()
    {
        string str;
        str = _day < 7 ? "Odd " : "Even ";
        str += (_day % 7) switch
        {
            0 => "Monday",
            1 => "Tuesday",
            2 => "Wednesday",
            3 => "Thursday",
            4 => "Friday",
            5 => "Saturday",
            _ => "Sunday"
        };
        return str;
    }

    public string StartTime()
    {
        string str;
        str = _index switch
        {
            1 => "08:20",
            2 => "10:00",
            3 => "11:40",
            4 => "13:30",
            5 => "15:20",
            6 => "17:00",
            7 => "18:40",
            _ => "20:20"
        };
        return str;
    }

    private void ValidateClassTime(int day, int index)
    {
        if (!(day is >= 0 and < 14 && index is > 0 and < 9))
            throw LessonTimeException.CreateLessonTimeDayOrIndexInvalid();
    }
}