using System;

namespace NotYet;

public class Classes
{
    public string Id { get; }
    public DateOnly Day { get; }
    public TimeOnly Start { get; }
    public TimeOnly End { get; }
    public TimeSpan Duration { get; }
    public string BackgroundColor { get; }
    public string TextColor { get; }
    public string Salles { get; }
    public string Matiere { get; }
    public string Groupes { get; }
    public string Category { get; }

    public Classes(string id, 
        DateOnly day, 
        TimeOnly start, 
        TimeOnly end, 
        string backgroundColor, 
        string textColor, 
        string salles, 
        string matiere, 
        string groupes, 
        string category)
    {
        Id = id;
        Day = day;
        Start = start;
        End = end;
        Duration = end - start;
        BackgroundColor = backgroundColor;
        TextColor = textColor;
        Salles = salles;
        Matiere = matiere;
        Groupes = groupes;
        Category = category;
    }
}
