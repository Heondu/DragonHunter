using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<Inventory>();
            return instance;
        }
    }

    [SerializeField] private Transform holder;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Slot weaponSlot;
    [SerializeField] private Slot armorSlot;
    [SerializeField] private ItemPopup popup;

    private static ItemData weapon;
    private static ItemData armor;
    private static List<ItemData> itemList = new List<ItemData>();
    private List<Slot> slotList = new List<Slot>();
    public static UnityEvent onItemChanged = new UnityEvent();

    private Slot selectedSlot;

    private void Awake()
    {
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    [System.Serializable]
    private class SaveData
    {
        public ItemData weapon;
        public ItemData armor;
        public List<ItemData> itemList;

        public SaveData(ItemData _weapon, ItemData _armor, List<ItemData> _itemList)
        {
            weapon = _weapon;
            armor = _armor;
            itemList = _itemList;
        }
    }

    private static void Save()
    {
        SaveData saveData = new SaveData(weapon, armor, itemList);
        SaveManager.SaveToJson(saveData, SaveDataManager.saveFile[SaveFile.Inventory]);
    }

    private static void Load()
    {
        SaveData saveData = SaveManager.LoadFromJson<SaveData>(SaveDataManager.saveFile[SaveFile.Inventory]);
        if (saveData != null)
        {
            itemList = saveData.itemList;
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].Init();
            }
            if (saveData.weapon.id != "")
            {
                weapon = saveData.weapon;
                weapon.Init();
            }
            if (saveData.armor.id != "")
            {
                armor = saveData.armor;
                armor.Init();
            }
        }
    }

    public static void AddItem(ItemData itemData)
    {
        itemList.Add(itemData);
        for (int i = 0; i < itemList.Count; i++)
        {
            Debug.Log(itemList[i].id);
        }    
        Save();
    }

    public void UpdateInventory()
    {
        int i = 0;
        for (; i < itemList.Count; i++)
        {
            if (i >= slotList.Count)
            {
                Slot newSlot = ObjectPooler.ObjectPool(holder, slotPrefab, Vector3.zero, Quaternion.identity, holder).GetComponent<Slot>();
                newSlot.Init();
                slotList.Add(newSlot);
            }
            slotList[i].SetItem(itemList[i]);
        }
        for (; i < slotList.Count; i++)
        {
            slotList[i].gameObject.SetActive(false);
            slotList.RemoveAt(i);
        }
        weaponSlot.SetItem(weapon);
        armorSlot.SetItem(armor);
        onItemChanged.Invoke();
    }

    public void ItemPopup(Slot _selectedSlot)
    {
        if (_selectedSlot.GetItem() == null) return;

        selectedSlot = _selectedSlot;

        popup.Display(selectedSlot);
    }

    public void LevelUp()
    {
        selectedSlot.GetItem().LevelUp();
        popup.Display(selectedSlot);
    }

    public ItemData GetSelectedItem()
    {
        return selectedSlot.GetItem();
    }

    public void Equip()
    {
        if (selectedSlot.IsEquipSlot)
        {
            ItemData itemData = selectedSlot.GetItem();
            if (itemData != null)
            {
                itemList.Add(itemData);
                switch (itemData.itemType)
                {
                    case "weapon": weapon = null; break;
                    case "armor": armor = null; break;
                }
            }
        }
        else
        {
            switch (selectedSlot.GetItem().itemType)
            {
                case "weapon":
                    ItemData itemData = weaponSlot.GetItem();
                    if (itemData != null)
                    {
                        itemList.Add(itemData);
                        weaponSlot.SetItem(null);
                    }
                    itemList.Remove(selectedSlot.GetItem());
                    weapon = selectedSlot.GetItem();
                    break;
                case "armor":
                    itemData = armorSlot.GetItem();
                    if (itemData != null)
                    {
                        itemList.Add(itemData);
                        armorSlot.SetItem(null);
                    }
                    itemList.Remove(selectedSlot.GetItem());
                    armor = selectedSlot.GetItem();
                    break;
            }
        }
        UpdateInventory();
        Save();
    }
}
