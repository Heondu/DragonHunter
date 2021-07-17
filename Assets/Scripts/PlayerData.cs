using UnityEngine;

public enum ResourcesModType { Add, Sub, Set }

public class PlayerResources
{
    private int value = 0;
    public int Value
    {
        get
        {
            return value;
        }
    }

    public PlayerResources(int n)
    {
        value = n;
    }

    public void Set(int n, ResourcesModType modType)
    {
        switch (modType)
        {
            case ResourcesModType.Add: value += n; break;
            case ResourcesModType.Sub: value -= n; break;
            case ResourcesModType.Set: value = n; break;
        }
        PlayerData.Save();
    }
}

public class PlayerData : MonoBehaviour
{
    private static PlayerResources gold;
    public static PlayerResources Gold
    {
        get
        {
            if (gold == null) Load();
            return gold;
        }
    }
    private static PlayerResources diamond;
    public static PlayerResources Diamond
    {
        get
        {
            if (diamond == null) Load();
            return diamond;
        }
    }

    [ContextMenu("Add Gold")]
    public void AddGold()
    {
        Gold.Set(10000, ResourcesModType.Add);
        Save();
    }

    [ContextMenu("Add Diamond")]
    public void AddDiamond()
    {
        Diamond.Set(10000, ResourcesModType.Add);
        Save();
    }

    private class SaveData
    {
        public int gold;
        public int diamond;

        public SaveData(int _gold, int _diamond)
        {
            gold = _gold;
            diamond = _diamond;
        }
    }

    public static void Save()
    {
        SaveData saveData = new SaveData(Gold.Value, Diamond.Value);
        SaveManager.SaveToJson(saveData, SaveDataManager.saveFile[SaveFile.Resources]);
    }

    private static void Load()
    {
        SaveData saveData = SaveManager.LoadFromJson<SaveData>(SaveDataManager.saveFile[SaveFile.Resources]);
        if (saveData == null)
        {
            gold = new PlayerResources(0);
            diamond = new PlayerResources(0);
        }
        else
        {
            gold = new PlayerResources(saveData.gold);
            diamond = new PlayerResources(saveData.diamond);
        }
    }
}