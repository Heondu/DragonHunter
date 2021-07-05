using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string path = "DB/";

    public static List<Dictionary<string, object>> localization = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> characters = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> monsters = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> traps = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> itemUpgrade = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> status = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> cards = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> specialCards = new List<Dictionary<string, object>>();
    public static List<Dictionary<string, object>> images = new List<Dictionary<string, object>>();

    private void Awake()
    {
        localization = CSVReader.Read(path + "localization");
        characters = CSVReader.Read(path + "characters");
        monsters = CSVReader.Read(path + "monsters");
        traps = CSVReader.Read(path + "traps");
        items = CSVReader.Read(path + "items");
        itemUpgrade = CSVReader.Read(path + "itemUpgrade");
        status = CSVReader.Read(path + "status");
        cards = CSVReader.Read(path + "cards");
        specialCards = CSVReader.Read(path + "specialCards");
        images = CSVReader.Read(path + "images");
    }

    public static bool Exists(List<Dictionary<string, object>> list, string key, object value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) return true;
        }
        return false;
    }

    public static string Localization(string str)
    {
        if (str == null) return "";
        string percent = "";
        if (str.Contains("%"))
        {
            str = str.Substring(0, str.Length - 1);
            percent = "%";
        }

        if (SettingsManager.GetLanguage() == Language.korean)
        {
            for (int i = 0; i < localization.Count; i++)
            {
                if (localization[i]["ID"].ToString().Equals(str)) return localization[i]["KOR"].ToString() + percent;
            }
        }
        else
        {
            return str;
        }
        return "";
    }

    public static Sprite LoadImage(string id)
    {
        string path = images.Find("ID", id, "Path").ToString();
        if (path.Contains("_"))
        {
            string[] splitStr = path.Split('_');
            return Resources.LoadAll<Sprite>(splitStr[0])[int.Parse(splitStr[1])];
        }
        else return Resources.Load<Sprite>(path);
    }
}

public static class Data
{
    public static object Find(this List<Dictionary<string, object>> list, string key, object value, string key2)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) return list[i][key2];
        }
        return null;
    }

    public static List<object> FindAll(this List<Dictionary<string, object>> list, string key, object value, string key2)
    {
        List<object> objects = new List<object>();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) objects.Add(list[i][key2]);
        }
        return objects;
    }

    public static Dictionary<string, object> FindDic(this List<Dictionary<string, object>> list, string key, object value)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i][key].Equals(value)) return list[i];
        }
        return null;
    }
}
