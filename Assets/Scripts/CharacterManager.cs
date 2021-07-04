using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string ID;
    public int LV;
    public int ATK { get; private set; }
    public int HP { get; private set; }

    private Dictionary<string, object> data = new Dictionary<string, object>();

    public Character(string id, int lv)
    {
        ID = id;
        LV = lv;
        data = DataManager.characters.FindDic("ID", id);
        ATK = (int)data["ATK"] + (int)data["ATKAmount"] * (lv - 1);
        HP = (int)data["HP"] + (int)data["HPAmount"] * (lv - 1);
    }

    public void LevelUp()
    {
        if (LV >= 50) return;
        LV++;
        ATK += (int)data["ATKAmount"];
        HP += (int)data["HPAmount"];
    }
}

[System.Serializable]
public class CharacterList
{
    public Dictionary<string, Character> characters = new Dictionary<string, Character>();

    public void Init()
    {
        for (int i = 0; i < DataManager.characters.Count; i++)
        {
            string id = DataManager.characters[i]["ID"].ToString();
            characters.Add(id, new Character(id, 1));
        }
    }

    public List<Character> GetList()
    {
        List<Character> list = new List<Character>();
        foreach (string key in characters.Keys)
        {
            list.Add(characters[key]);
        }
        return list;
    }
}


public class CharacterManager : MonoBehaviour
{
    private static CharacterList list;

    public static CharacterList List
    {
        get
        {
            if (list == null) Load();
            return list;
        }
    }

    private string selectedID;
    public static string currentID = "character001";

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
        public string currentID;
        public List<Character> data;
    }

    private static void Save()
    {
        SaveData saveData = new SaveData();
        saveData.currentID = currentID;
        saveData.data = List.GetList();
        JsonIO.SaveToJson(saveData, SaveDataManager.saveFile[SaveFile.Characters]);
    }

    private static void Load()
    {
        SaveData saveData = JsonIO.LoadFromJson<SaveData>(SaveDataManager.saveFile[SaveFile.Characters]);
        list = new CharacterList();
        if (saveData == null)
        {
            currentID = "character001";
            list.Init();
        }
        else
        {
            currentID = saveData.currentID;
            for (int i = 0; i < saveData.data.Count; i++)
            {
                list.characters.Add(saveData.data[i].ID, new Character(saveData.data[i].ID, saveData.data[i].LV));
            }
        }
    }

    public void LevelUp()
    {
        List.characters[selectedID].LevelUp();
        Save();
    }

    public void Select(string id)
    {
        selectedID = id;
    }

    public void Accept()
    {
        currentID = selectedID;
    }

    public static Character GetCharacter()
    {
        return List.characters[currentID];
    }
}
