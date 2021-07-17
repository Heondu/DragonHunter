using UnityEngine;
using TMPro;

public class GoldDiamondViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGold;
    [SerializeField] private TextMeshProUGUI textDiamond;

    private void Update()
    {
        textGold.text = PlayerData.Gold.Value.ToString();
        textDiamond.text = PlayerData.Diamond.Value.ToString();
    }
}