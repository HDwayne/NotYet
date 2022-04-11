using System;
using System.Net.Http;

namespace NotYet;

public class Celcat
{
    public static string? GetFromCelcatAsync(DateTime date, string groupe)
    {
        var DayOWeek = (int)date.DayOfWeek;
        HttpContent start = new StringContent(date.AddDays(1-DayOWeek).ToString("yyyy-MM-dd"));
        HttpContent end = new StringContent(date.AddDays(7-DayOWeek).ToString("yyyy-MM-dd"));
        HttpContent resType = new StringContent("103");
        HttpContent calView = new StringContent("agendaWeek");
        HttpContent federationIds = new StringContent(groupe);
        HttpContent colourScheme = new StringContent("3");

        using (var client = new HttpClient())
        using (var formData = new MultipartFormDataContent())
        {
            formData.Add(start, "start");
            formData.Add(end, "end");
            formData.Add(resType, "resType");
            formData.Add(calView, "calView");
            formData.Add(federationIds, "federationIds[]");
            formData.Add(colourScheme, "colourScheme");
            var endpoint = new Uri(Properties.Resources.celcatEndpoint);

            try
            {
                var response = client.PostAsync(endpoint, formData);
                var result = response.Result;
                if (!result.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return result.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                client.CancelPendingRequests();
            }
        }
    }

    public static string? GetInfoFromClassID(string id)
    {
        HttpContent eventId = new StringContent(id);

        using (var client = new HttpClient())
        using (var formData = new MultipartFormDataContent())
        {
            formData.Add(eventId, "eventId");
            var endpoint = new Uri("https://edt.univ-tlse3.fr/calendar2/Home/GetSideBarEvent");
            try
            {
                var response = client.PostAsync(endpoint, formData);
                var result = response.Result;
                if (!result.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    return result.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                client.CancelPendingRequests();
            }
        }
    }

    public static string? GetGroupes()
    {
        using var client = new HttpClient();
        var endpoint = new Uri("https://edt.univ-tlse3.fr/calendar2/Home/ReadResourceListItems?myResources=false&searchTerm=___&pageSize=10000&pageNumber=1&resType=103&_=16014082595");
        try
        {
            var response = client.GetAsync(endpoint).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            client.CancelPendingRequests();
        }
    }

    public static Classes? JsonDataToClass(dynamic classe, dynamic infos)
    {
        try
        {
            string id = classe["id"];
            DateOnly day = DateOnly.FromDateTime(DateTime.Parse(classe["start"].ToString()));
            TimeOnly start = TimeOnly.FromDateTime(DateTime.Parse(classe["start"].ToString()));
            TimeOnly end = TimeOnly.FromDateTime(DateTime.Parse(classe["end"].ToString()));
            string bc = classe["backgroundColor"];
            string tc = classe["textColor"];

            string salles = "";
            string matiere = "";
            string groupes = "";
            string categorie = "";

            for (int i = 0; i < infos["elements"].Count; i++)
            {
                if (infos["elements"][i]["label"] == "Salle")
                {
                    salles = infos["elements"][i]["content"];
                    i++;
                } 
                else if (infos["elements"][i]["label"] == "Salles")
                {
                    salles = infos["elements"][i]["content"];
                    i++;
                    while ( i < infos["elements"].Count && infos["elements"][i]["label"] == null)
                    {
                        salles = salles + " " + infos["elements"][i]["content"];
                        i++;
                    }
                }

                if (infos["elements"][i]["label"] == "Matière")
                {
                    matiere = infos["elements"][i]["content"];
                    i++;
                }
                else if (infos["elements"][i]["label"] == "Matières")
                {
                    matiere = infos["elements"][i]["content"];
                    i++;
                    while (infos["elements"][i]["label"] == null)
                    {
                        matiere = matiere + " " + infos["elements"][i]["content"];
                        i++;
                    }
                }

                if (infos["elements"][i]["label"] == "Groupe")
                {
                    groupes = infos["elements"][i]["content"];
                    i++;
                }
                else if (infos["elements"][i]["label"] == "Groupes")
                {
                    groupes = infos["elements"][i]["content"];
                    i++;
                    while (infos["elements"][i]["label"] == null)
                    {
                        groupes = groupes + " " + infos["elements"][i]["content"];
                        i++;
                    }
                }

                if (infos["elements"][i]["label"] == "Catégorie")
                {
                    categorie = infos["elements"][i]["content"];
                    i++;
                }
                else if (infos["elements"][i]["label"] == "Salles")
                {
                    categorie = infos["elements"][i]["Catégories"];
                    i++;
                    while (infos["elements"][i]["label"] == null)
                    {
                        categorie = categorie + " " + infos["elements"][i]["content"];
                        i++;
                    }
                }
            }

            var obj = new Classes(id, day, start, end, bc, tc, salles, matiere, groupes, categorie);
            return obj;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
// test