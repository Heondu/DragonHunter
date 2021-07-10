using UnityEngine;

public class EquipSlot : Slot
{
    [SerializeField] private GameObject thumbnail;

    public override void SetItem(ItemData _itemData)
    {
        if (_itemData == itemData) return;
        if (_itemData != null)
        {
            StatusManager.GetStatus(_itemData.statType).AddModifier(new StatusModifier(_itemData.amount.Value, StatusModType.Flat, _itemData));
        }
        else
        {
            if (itemData != null)
            {
                StatusManager.GetStatus(itemData.statType).RemoveAllModifiersFromSource(itemData);
            }
        }
        base.SetItem(_itemData);
    }

    protected override void UpdateSlot()
    {
        base.UpdateSlot();
        if (itemData != null)
        {
            thumbnail.SetActive(false);
            image.gameObject.SetActive(true);
        }
        else
        {
            thumbnail.SetActive(true);
            image.gameObject.SetActive(false);
        }
    }
}
