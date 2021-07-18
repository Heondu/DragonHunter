using System.Collections.Generic;

public class Warrior : Character
{
    public Warrior(string id, bool isLock) : base(id, isLock) { }

    protected override void Ability()
    {
        List<Character> list = CharacterManager.List.GetList();
        for (int i = 0; i < list.Count; i++)
        {
            list[i].list.status["atk"].AddModifier(new StatusModifier(1, StatusModType.Flat, this));
        }

        if (LV == 10)
        {
            this.list.status["def"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
        }

        if (LV == 20)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].list.status["critChance"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
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
        IsLock = false;
    }
}