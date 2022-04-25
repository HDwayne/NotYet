using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NotYet;

public class CalendarDay
{
    private readonly List<Classes> AllClasses = new();
    private TimeOnly FirstHourClasses;
    private TimeOnly LastHourClasses;

    public void AddClasses(Classes classe)
    {
        if (classe.Start.CompareTo(FirstHourClasses) < 0)
        {
            FirstHourClasses = classe.Start;
        }
        if (classe.End.CompareTo(LastHourClasses) > 0)
        {
            LastHourClasses = classe.End;
        }
        AllClasses.Add(classe);
    }

    public TimeOnly GetLastHourClasses()
    {
        return LastHourClasses;
    }

    public List<Classes> GetAllClasses()
    {
        AllClasses.Sort((x, y) => x.Start.CompareTo(y.Start));
        return AllClasses;
    }

    public CalendarDay()
    {
        FirstHourClasses = TimeOnly.MaxValue;
        LastHourClasses = TimeOnly.MinValue;
    }
}

public class CalendarDayInterface : ObservableCollection<Classes>
{
    public CalendarDayInterface(CalendarDay calendar)
    {
        foreach (Classes c in calendar.GetAllClasses())
        {
            Add(c);
        }
    }
}