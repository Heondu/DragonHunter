using UnityEngine;

public class StatusManager : MonoBehaviour
{
    private static PlayerStatus status;
    public static PlayerStatus Status
    {
        get
        {
            if (status == null) Load();
            return status;
        }
    }


    private void OnApplicationQuit()
    {
        Save();
    }

    private static void Save()
    {
        JsonIO.SaveToJson(Status, SaveDataManager.saveFile[SaveFile.PlayerStatus]);
    }

    private static void Load()
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
            case StatusList.ATK: Status.atk.LevelUp("atk"); break;
            case StatusList.SkillDamage: Status.skillDamage.LevelUp("skillDamage"); break;
            case StatusList.HP: Status.hp.LevelUp("hp"); break;
            case StatusList.Defense: Status.defense.LevelUp("defense"); break;
            case StatusList.CritChance: Status.critChance.LevelUp("critChance"); break;
            case StatusList.CritDamage: Status.critDamage.LevelUp("critDamage"); break;
            case StatusList.DungeonSpeed: Status.dungeonSpeed.LevelUp("dungeonSpeed"); break;
            case StatusList.Speed: Status.speed.LevelUp("speed"); break;
            case StatusList.ATKSpeed: Status.atkSpeed.LevelUp("atkSpeed"); break;
            case StatusList.Range: Status.range.LevelUp("range"); break;
            case StatusList.Score: Status.score.LevelUp("score"); break;
        }
        Save();
    }

    public static int GetLevel(StatusList name)
    {
        switch (name)
        {
            case StatusList.ATK: return Status.atk.Level;
            case StatusList.SkillDamage: return Status.skillDamage.Level;
            case StatusList.HP: return Status.hp.Level;
            case StatusList.Defense: return Status.defense.Level;
            case StatusList.CritChance: return Status.critChance.Level;
            case StatusList.CritDamage: return Status.critDamage.Level;
            case StatusList.DungeonSpeed: return Status.dungeonSpeed.Level;
            case StatusList.Speed: return Status.speed.Level;
            case StatusList.ATKSpeed: return Status.atkSpeed.Level;
            case StatusList.Range: return Status.range.Level;
            case StatusList.Score: return Status.score.Level;
        }
        return 1;
    }
}
