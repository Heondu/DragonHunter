using System.Collections.Generic;

public class Assassin : Character
{
    public Assassin(string id, bool isLock) : base(id, isLock) { }

    protected override void Ability()
    {
        List<Character> list = CharacterManager.List.GetList();

        for (int i = 0; i < list.Count; i++)
        {
            list[i].list.status["critDamage"].AddModifier(new StatusModifier(1, StatusModType.PercentAdd, this));
        }

        if (LV == 10)
        {
            this.list.status["atkSpeed"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
        }

        if (LV == 20)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].list.status["critDamage"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
            }
        }

        if (LV == 30)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].list.status["atkSpeed"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
            }
        }
    }

    public override void Unlock()
    {
        if (!IsLock) return;
        if (GameManager.GetPlayCount() > 0) IsLock = false;
    }
}
