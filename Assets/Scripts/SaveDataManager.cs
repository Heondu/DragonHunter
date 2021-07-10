using System.Collections.Generic;
using UnityEngine;

public enum SaveFile
{
    PlayerStatus,
    Characters,
    Inventory
}

public class SaveDataManager : MonoBehaviour
{
    public static Dictionary<SaveFile, string> saveFile = new Dictionary<SaveFile, string>();

    private void Awake()
    {
        saveFile[SaveFile.PlayerStatus] = "Player Status";
        saveFile[SaveFile.Characters] = "Characters";
        saveFile[SaveFile.Inventory] = "Inventory";
    }
}
