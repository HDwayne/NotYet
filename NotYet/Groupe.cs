using System.Collections.ObjectModel;

namespace NotYet;

public class Groupe
{
    public string Id { get; }
    public string Texte { get; }
    public string Dept { get; }

    public Groupe(string id, string texte, string dept)
    {
        this.Dept = dept;
        this.Id = id;
        this.Texte = texte;
    }
}

public class GroupeInterface : ObservableCollection<Groupe>
{
    public GroupeInterface()
    {
        foreach (Groupe c in DB.GetGroupeTable())
        {
            this.Add(c);
        }
    }
}