using System;

namespace Ion.Time;

/// <summary>
/// Extends <see cref="TimeSpan"/>.
/// </summary>
[Extend<TimeSpan>]
public static class XTimeSpan
{
    #region ToLeft

    public static TimeSpan ToLeft(this TimeSpan i, long current, long total)
    {
        var lines = Convert.ToDouble(current) / Convert.ToDouble(total) * 100;
        lines = lines == 0.0 ? 1.0 : lines;
        var seconds = i.TotalSeconds / lines * (100.0 - lines);
        return TimeSpan.FromSeconds(double.IsNaN(seconds) ? 0 : seconds);
    }

    #endregion

    #region ToShort

    public static string ToShort(this TimeSpan i, bool daysOnly)
    {
        if (i.TotalSeconds == 0)
            return string.Empty;

        var result = string.Empty;
        if (i.Days > 0)
            result += string.Format("{0}d", i.Days.ToString());

        if (!daysOnly || i.Days == 0)
        {
            if (i.Hours > 0)
                result += string.Format(" {0}h", i.Hours.ToString());

            if (i.Minutes > 0)
                result += string.Format(" {0}m", i.Minutes.ToString());

            if (i.Seconds > 0)
                result += string.Format(" {0}s", i.Seconds.ToString());
        }

        return result;
    }

    #endregion
}