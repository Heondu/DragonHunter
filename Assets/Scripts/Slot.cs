using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] protected Image image;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI textLevel;
    protected ItemData itemData;
    public bool IsEquipSlot = false;

    private void Awake()
    {
        button.onClick.AddListener(ItemPopup);
        Inventory.onItemChanged.AddListener(UpdateSlot);
    }

    public void Init()
    {
        itemData = null;
    }

    public virtual void SetItem(ItemData _itemData)
    {
        itemData = _itemData;
    }

    protected virtual void UpdateSlot()
    {
        if (itemData != null)
        {
            image.sprite = DataManager.LoadImage(itemData.id);
            textLevel.text = $"+{itemData.lv}";
        }
        else
        {
            textLevel.text = "";
        }
    }

    private void ItemPopup()
    {
        Inventory.Instance.ItemPopup(this);
    }

    public ItemData GetItem()
    {
        if (itemData != null) return itemData;
        else return null;
    }
}
