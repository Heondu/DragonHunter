using UnityEngine;

public class StatusManager : MonoBehaviour
{
    private static PlayerStatus status;
    public static PlayerStatus Status
    {
        get
        {
            if (status == null) LoadStatus();
            return status;
        }
    }

    private void OnApplicationQuit()
    {
        SaveStatus();
    }

    private static void SaveStatus()
    {
        JsonIO.SaveToJson(status, SaveDataManager.saveFile[SaveFile.PlayerStatus]);
    }

    private static void LoadStatus()
    {
        status = JsonIO.LoadFromJson<PlayerStatus>(SaveDataManager.saveFile[SaveFile.PlayerStatus]);
        if (status == null)
        {
            status = new PlayerStatus();
            status.Init();
        }
    }

    public static void LevelUp(StatusList name)
    {
        switch (name)
        {
            case StatusList.ATK: status.atk.LevelUp("atk"); break;
            case StatusList.SkillDamage: status.skillDamage.LevelUp("skillDamage"); break;
            case StatusList.HP: status.hp.LevelUp("hp"); break;
            case StatusList.Defense: status.defense.LevelUp("defense"); break;
            case StatusList.CritChance: status.critChance.LevelUp("critChance"); break;
            case StatusList.CritDamage: status.critDamage.LevelUp("critDamage"); break;
            case StatusList.DungeonSpeed: status.dungeonSpeed.LevelUp("dungeonSpeed"); break;
            case StatusList.Speed: status.speed.LevelUp("speed"); break;
            case StatusList.ATKSpeed: status.atkSpeed.LevelUp("atkSpeed"); break;
            case StatusList.Range: status.range.LevelUp("range"); break;
            case StatusList.Score: status.score.LevelUp("score"); break;
        }
        SaveStatus();
    }

    public static int GetLevel(StatusList name)
    {
        switch (name)
        {
            case StatusList.ATK: return status.atk.Level;
            case StatusList.SkillDamage: return status.skillDamage.Level;
            case StatusList.HP: return status.hp.Level;
            case StatusList.Defense: return status.defense.Level;
            case StatusList.CritChance: return status.critChance.Level;
            case StatusList.CritDamage: return status.critDamage.Level;
            case StatusList.DungeonSpeed: return status.dungeonSpeed.Level;
            case StatusList.Speed: return status.speed.Level;
            case StatusList.ATKSpeed: return status.atkSpeed.Level;
            case StatusList.Range: return status.range.Level;
            case StatusList.Score: return status.score.Level;
        }
        return 1;
    }
}
