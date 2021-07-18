using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string id;
    public int lv;
    public string itemType;
    public string statType;
    public Status amount;
    private Dictionary<string, object> data;

    public ItemData(string _id, int _lv)
    {
        data = DataManager.items.FindDic("ID", _id);
        id = _id;
        lv = _lv;
        itemType = data["ItemType"].ToString();
        statType = data["StatType"].ToString();
        amount = new Status(statType, lv, (int)data["Amount"]);
        for (int i = 1; i <= lv; i++)
        {
            amount.AddModifier(new StatusModifier((int)data["Level" + i], StatusModType.Flat, this));
        }
    }

    public void Init()
    {
        data = DataManager.items.FindDic("ID", id);
        amount.RemoveAllModifiersFromSource(this);
        for (int i = 1; i <= lv; i++)
        {
            amount.AddModifier(new StatusModifier((int)data["Level" + i], StatusModType.Flat, this));
        }
    }

    public void LevelUp()
    {
        int prob = (int)DataManager.itemLevelUp[lv]["Prob"];
        int price = (int)DataManager.itemLevelUp[lv]["Price"];

        int rand = Random.Range(0, 100);
        if (lv < 5)
        {
            if (PlayerData.Gold.Value > price)
            {
                PlayerData.Gold.Set(price, ResourcesModType.Sub);
                if (rand < prob)
                {
                    lv++;
                    Init();
                }
                else
                {
                    lv--;
                    Init();
                }
            }
        }
    }
}
