using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusList
{
    public Dictionary<string, Status> status = new Dictionary<string, Status>();

    public void Init()
    {
        for (int i = 0; i < DataManager.status.Count; i++)
        {
            string name = DataManager.status[i]["Name"].ToString();
            status.Add(name, new Status(name, 1, (float)DataManager.status[i]["Default"]));
        }
    }

    public List<Status> GetList()
    {
        List<Status> list = new List<Status>();
        foreach (string key in status.Keys)
        {
            list.Add(status[key]);
        }
        return list;
    }
}

public class StatusManager : MonoBehaviour
{
    private static StatusList list;
    public static StatusList List
    {
        get
        {
            if (list == null) Load();
            return list;
        }
    }

    private void Awake()
    {
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    [System.Serializable]
    private class SaveData
    {
        public List<Status> data;
    }

    private static void Save()
    {
        SaveData saveData = new SaveData();
        saveData.data = List.GetList();
        JsonIO.SaveToJson(saveData, SaveDataManager.saveFile[SaveFile.PlayerStatus]);
    }

    private static void Load()
    {
        SaveData saveData = JsonIO.LoadFromJson<SaveData>(SaveDataManager.saveFile[SaveFile.PlayerStatus]);
        list = new StatusList();
        if (saveData == null)
        {
            list.Init();
        }
        else
        {
            for (int i = 0; i < saveData.data.Count; i++)
            {
                string name = saveData.data[i].Name;
                Dictionary<string, object> data = DataManager.status.FindDic("Name", name);
                float value = (float)data["Default"] + (float)data["StatAmount"] * (saveData.data[i].LV - 1);
                list.status.Add(name, new Status(name, saveData.data[i].LV, value));
            }
        }
    }

    public void LevelUp(string name)
    {
        if (!List.status.ContainsKey(name)) return;
        List.status[name].LevelUp(name);
        Save();
    }

    public static int GetLevel(string name)
    {
        if (!list.status.ContainsKey(name)) return 1;
        return List.status[name].LV;
    }

    public static Status GetStatus(string name)
    {
        return List.status[name];
    }

    public static StatusList DeepCopy()
    {
        StatusList newList = new StatusList();
        foreach (string key in List.status.Keys)
        {
            string name = key;
            float value = List.status[key].Value;
            newList.status.Add(name, new Status(name, 1, value));
        }
        return newList;
    }
}
