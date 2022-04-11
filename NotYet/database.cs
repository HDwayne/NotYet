using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NotYet;

public class DB
{
    private static DataTable? Get_DataTable(string SqlCommande)
    {
        using (SqlConnection db = new SqlConnection(Properties.Resources.dbconnect))
        {
            try
            {
                db.Open();
                if (db is not null)
                {
                    var table = new DataTable();
                    var adapter = new SqlDataAdapter(SqlCommande, db);
                    try
                    {
                        adapter.Fill(table);
                        return table;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                
            }
        }
    }

    private static void Execute_SQL(string SqlText)
    {
        using (SqlConnection db = new SqlConnection(Properties.Resources.dbconnect))
        {
            db.Open();
            if (db is not null)
            {
                var cmdCommand = new SqlCommand(SqlText, db);
                try
                {
                    cmdCommand.ExecuteNonQuery();
                }
                finally
                {

                }
            }
        }
    }

    private static void ClasseToDb(Classes classe, string groupe)
    {
        DataTable? tbl = Get_DataTable($"SELECT TOP 1 * FROM DataTable WHERE [Id] Like '{classe.Id}'");
        if (tbl is not null)
        {
            if (tbl.Rows.Count == 0)
            {
                var sqlAdd = $"INSERT INTO DataTable ([id],[day],[start],[end],[duration],[backgroundcolor],[textcolor],[salles],[matiere],[groupes],[categorie],[gp_api]) VALUES('{classe.Id}','{classe.Day}','{classe.Start}','{classe.End}','{classe.Duration}','{classe.BackgroundColor}','{classe.TextColor}','{classe.Salles}','{classe.Matiere.Replace("'", "''")}','{classe.Groupes}','{classe.Category}','{groupe}')";
                Execute_SQL(sqlAdd);
            }
        }
    }

    public static List<Classes>? GetWeekData(DateTime day, string groupe)
    {
        var liste = new List<Classes>();
        var jsonstring = Celcat.GetFromCelcatAsync(day, groupe);
        if (jsonstring is not null)
        {
            dynamic? json = JsonConvert.DeserializeObject(jsonstring);
            if (json is not null)
            {
                foreach (var ClasseJson in json)
                {
                    if (ClasseJson["allDay"] != true)
                    {
                        string id = ClasseJson["id"];
                        var infojsonstring = Celcat.GetInfoFromClassID(id);
                        if (infojsonstring is not null)
                        {
                            dynamic? infojson = JsonConvert.DeserializeObject(infojsonstring);
                            if (json is not null)
                            {
                                var classe = Celcat.JsonDataToClass(ClasseJson, infojson);
                                if (classe is not null)
                                    liste.Add(classe);
                            }
                        }
                    }
                }
                return liste;
            }
        }
        return null;
    }

    public static void DeleteDayData(DateTime day, string groupe)
    {
        var DayOfWeek = (int)day.DayOfWeek;
        var firstday = day.AddDays(1 - DayOfWeek);
        DateOnly date;
        for (double i = 0; i < 7; i++)
        {
            date = DateOnly.FromDateTime(firstday.AddDays(i));
            Execute_SQL($"DELETE FROM DataTable WHERE day = '{date}' AND gp_api = '{groupe}'");
        }
    }

    public static int RefreshDayData(DateTime day, string groupe)
    {
        var liste = GetWeekData(day, groupe);
        if (liste is not null)
        {
            DeleteDayData(day, groupe);
            foreach (var classe in liste)
            {
                ClasseToDb(classe, groupe);
            }
            Properties.Settings.Default.LastUpdate = DateTime.Now;
        }
        else
        {
            return -1;
        }
        Properties.Settings.Default.Save();
        return 0;
    }

    public static CalendarDay DbToClass(DateTime day, string groupe)
    {
        var auj = DateOnly.FromDateTime(day);
        var DayCal = new CalendarDay();

        DataTable? table = Get_DataTable($"SELECT * FROM DataTable WHERE [day] Like '{auj}' AND [gp_api] Like '{groupe}'");
        if (table is not null)
        {
            foreach (DataRow row in table.Rows)
            {
                string id = (string)row["id"];
                DateOnly auj2 = DateOnly.FromDateTime(DateTime.Parse((string)row["start"]));
                TimeOnly start = TimeOnly.FromDateTime(DateTime.Parse((string)row["start"]));
                TimeOnly end = TimeOnly.FromDateTime(DateTime.Parse((string)row["end"]));
                string bc = (string)row["backgroundColor"];
                string tc = (string)row["textColor"];
                string salles = (string)row["salles"];
                string matiere = (string)row["matiere"];
                string groupes = (string)row["groupes"];
                string categorie = (string)row["categorie"];
     
                var obj = new Classes(id, auj2, start, end, bc, tc, salles, matiere, groupes, categorie);
                DayCal.AddClasses(obj);
            }
        }
        return DayCal;
    }

    public static void RefreshGroupeTable()
    {
        Execute_SQL($"TRUNCATE TABLE GpTable");
        var jsonstring = Celcat.GetGroupes();
        if (jsonstring is not null)
        {
            dynamic? json = JsonConvert.DeserializeObject(jsonstring);
            if (json is not null)
            {
                foreach (var groupes in json["results"])
                {
                    Execute_SQL($"INSERT INTO GpTable ([id],[text],[dept]) VALUES('{groupes["id"]}','{groupes["text"]}','{groupes["dept"]}')");
                }
            }
        }
    }

    public static bool IsGpTableEmpty()
    {
        DataTable? table = Get_DataTable($"SELECT * FROM GpTable");
        if (table is not null)
            return table.Rows.Count == 0;
        return false;
    }

    public static List<Groupe> GetGroupeTable()
    {
        List<Groupe> groupes = new();
        var table = Get_DataTable($"SELECT * FROM GpTable");
        if (table is not null)
        {
            foreach (DataRow row in table.Rows)
            {
                Groupe gp = new(row[0].ToString(), row[1].ToString(), row[2].ToString());
                groupes.Add(gp);
            }
        }
        return groupes;
    }

    public static void WipeData()
    {
        Execute_SQL("TRUNCATE TABLE DataTable");
        Execute_SQL("TRUNCATE TABLE GpTable");
    }
}