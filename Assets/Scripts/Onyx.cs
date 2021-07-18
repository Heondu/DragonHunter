﻿using System.Collections.Generic;

public class Onyx : Character
{
    public Onyx(string id, bool isLock) : base(id, isLock) { }

    protected override void Ability()
    {
        List<Character> list = CharacterManager.List.GetList();

        for (int i = 0; i < list.Count; i++)
        {
            list[i].list.status["critChance"].AddModifier(new StatusModifier(0.3f, StatusModType.PercentAdd, this));
        }

        if (LV == 10)
        {
            this.list.status["score"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
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
                list[i].list.status["critChance"].AddModifier(new StatusModifier(5, StatusModType.PercentAdd, this));
            }
        }
    }

    public override void Unlock()
    {
        if (!IsLock) return;
        if (PlayerData.Diamond.Value >= 1500)
        {
            PlayerData.Diamond.Set(1500, ResourcesModType.Sub);
            IsLock = false;
        }
    }
}