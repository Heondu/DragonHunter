using System.Collections.Generic;

public class Wizard : Character
{
    public Wizard(string id, bool isLock) : base(id, isLock) { }

    protected override void Ability()
    {
        List<Character> list = CharacterManager.List.GetList();

        for (int i = 0; i < list.Count; i++)
        {
            list[i].list.status["skillDamage"].AddModifier(new StatusModifier(1, StatusModType.PercentAdd, this));
        }

        if (LV == 10)
        {
            this.list.status["speed"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
        }

        if (LV == 20)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].list.status["speed"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
            }
        }

        if (LV == 30)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].list.status["atk"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
            }
        }
    }

    public override void Unlock()
    {
        if (!IsLock) return;
        if (PlayerData.Diamond.Value >= 500)
        {
            PlayerData.Diamond.Set(500, ResourcesModType.Sub);
            IsLock = false;
        }
    }
}