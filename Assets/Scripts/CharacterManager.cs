using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string ID;
    public int LV;
    public bool IsLock = true;
    public StatusList list = new StatusList();

    private Dictionary<string, object> data = new Dictionary<string, object>();

    public Character(string id, bool isLock)
    {
        ID = id;
        LV = 1;
        IsLock = isLock;
        data = DataManager.characters.FindDic("ID", id);
        list.Init(true);
        list.status["atk"].BaseValue = (int)data["ATK"];
        list.status["hp"].BaseValue = (int)data["HP"];
    }

    public void Init(int lv)
    {
        for (int i = 1; i < lv; i++)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if (LV >= 50) return;
        LV++;
        list.status["atk"].AddModifier(new StatusModifier((int)data["ATKAmount"], StatusModType.Flat, this));
        list.status["hp"].AddModifier(new StatusModifier((int)data["HPAmount"], StatusModType.Flat, this));
        Ability();
    }

    protected virtual void Ability()
    {

    }

    public virtual void Unlock()
    {

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
            switch (id)
            {
                case "character001": characters.Add(id, new Warrior(id, false)) ; break;
                case "character002": characters.Add(id, new Assassin(id, true)); break;
                case "character003": characters.Add(id, new Wizard(id, true)); break;
                case "character004": characters.Add(id, new Savage(id, true)); break;
                case "character005": characters.Add(id, new Onyx(id, true)); break;
                case "character006": characters.Add(id, new Violetta(id, true)); break;
            }
        }
        for (int i = 0; i < DataManager.characters.Count; i++)
        {
            string id = DataManager.characters[i]["ID"].ToString();
            characters[id].Init(1);
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

    private string selectedID = "";
    public static string currentID = "character001";

    [System.Serializable]
    private class SaveData
    {
        public string currentID;
        public List<Character> data;
    }

    public static void Save()
    {
        SaveData saveData = new SaveData();
        saveData.currentID = currentID;
        saveData.data = List.GetList();
        SaveManager.SaveToJson(saveData, SaveDataManager.saveFile[SaveFile.Characters]);
    }

    public static void Load()
    {
        SaveData saveData = SaveManager.LoadFromJson<SaveData>(SaveDataManager.saveFile[SaveFile.Characters]);
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
                switch (saveData.data[i].ID)
                {
                    case "character001": list.characters.Add(saveData.data[i].ID, new Warrior(saveData.data[i].ID, saveData.data[i].IsLock)); break;
                    case "character002": list.characters.Add(saveData.data[i].ID, new Assassin(saveData.data[i].ID, saveData.data[i].IsLock)); break;
                    case "character003": list.characters.Add(saveData.data[i].ID, new Wizard(saveData.data[i].ID, saveData.data[i].IsLock)); break;
                    case "character004": list.characters.Add(saveData.data[i].ID, new Savage(saveData.data[i].ID, saveData.data[i].IsLock)); break;
                    case "character005": list.characters.Add(saveData.data[i].ID, new Onyx(saveData.data[i].ID, saveData.data[i].IsLock)); break;
                    case "character006": list.characters.Add(saveData.data[i].ID, new Violetta(saveData.data[i].ID, saveData.data[i].IsLock)); break;
                }
            }
            for (int i = 0; i < saveData.data.Count; i++)
            {
                list.characters[saveData.data[i].ID].Init(saveData.data[i].LV);
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
        if (List.characters[id].IsLock) selectedID = "";
        else selectedID = id;
    }

    public void Accept()
    {
        if (selectedID != "")
        {
            currentID = selectedID;
            Save();
        }
    }

    public static Character GetCharacter()
    {
        return List.characters[currentID];
    }
}
