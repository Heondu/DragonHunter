using System.Collections.Generic;

[System.Serializable]
public class ItemData
{
    public string id;
    public int lv;
    public string itemType;
    public string statType;
    public Status amount;

    public ItemData(string _id, int _lv)
    {
        Dictionary<string, object> data = DataManager.items.FindDic("ID", _id);
        id = _id;
        lv = _lv;
        itemType = data["ItemType"].ToString();
        statType = data["StatType"].ToString();
        amount = new Status(statType, lv, (int)data["Amount"]);
        for (int i = 1; i <= lv; i++)
        {
            amount.AddModifier(new StatusModifier((int)data["Level" + i], StatusModType.Flat));
        }
    }

    public void Init()
    {
        Dictionary<string, object> data = DataManager.items.FindDic("ID", id);
        for (int i = 1; i <= lv; i++)
        {
            amount.AddModifier(new StatusModifier((int)data["Level" + i], StatusModType.Flat));
        }
    }
}
