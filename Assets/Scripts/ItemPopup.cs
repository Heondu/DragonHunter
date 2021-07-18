using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPopup : MonoBehaviour
{
    [SerializeField] private Image imageIcon;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textDesc;
    [SerializeField] private TextMeshProUGUI textEquip;
    [SerializeField] private TextMeshProUGUI textPrice;
    
    public void Display(Slot slot)
    {
        gameObject.SetActive(true);
        ItemData itemData = slot.GetItem();
        imageIcon.sprite = DataManager.LoadImage(itemData.id);
        textName.text = DataManager.Localization(itemData.id);
        textLevel.text = $"LV. {itemData.lv} / 5";
        textDesc.text = $"�Ӽ�\n{DataManager.Localization(itemData.statType)} + {itemData.amount.Value}";
        textEquip.text = slot.IsEquipSlot ? "��� ����" : "���";
        textPrice.text = $"{DataManager.itemLevelUp[Inventory.Instance.GetSelectedItem().lv]["Price"]}\n������";
    }
}
