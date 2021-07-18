using System.Collections.Generic;

public class Savage : Character
{
    public Savage(string id, bool isLock) : base(id, isLock) { }

    protected override void Ability()
    {
        List<Character> list = CharacterManager.List.GetList();

        for (int i = 0; i < list.Count; i++)
        {
            list[i].list.status["def"].AddModifier(new StatusModifier(0.3f, StatusModType.PercentAdd, this));
        }

        if (LV == 10)
        {
            this.list.status["def"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
        }

        if (LV == 20)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].list.status["hp"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
            }
        }

        if (LV == 30)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].list.status["hp"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
            }
        }
    }

    public override void Unlock()
    {
        if (!IsLock) return;
        if (GameManager.GetPlayTime() >= 600) IsLock = false;
    }
}