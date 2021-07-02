public enum StatusList { ATK, SkillDamage, HP, Defense, CritChance, CritDamage, DungeonSpeed, Speed, ATKSpeed, Range, Score }

[System.Serializable]
public class PlayerStatus
{
    public Status atk = new Status();
    public Status skillDamage = new Status();
    public Status hp = new Status();
    public Status defense = new Status();
    public Status critChance = new Status();
    public Status critDamage = new Status();
    public Status dungeonSpeed = new Status();
    public Status speed = new Status();
    public Status atkSpeed = new Status();
    public Status range = new Status();
    public Status score = new Status();

    public void Init()
    {
        for (int i = 0; i < DataManager.status.Count; i++)
        {
            switch (DataManager.status[i]["Name"])
            {
                case "atk": atk.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "skillDamage": skillDamage.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "hp": hp.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "defense": defense.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "critChance": critChance.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "critDamage": critDamage.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "dungeonSpeed": dungeonSpeed.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "speed": speed.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "atkSpeed": atkSpeed.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "range": range.BaseValue = (float)DataManager.status[i]["Default"]; break;
                case "score": score.BaseValue = (float)DataManager.status[i]["Default"]; break;
            }
        }
    }
}