using UnityEngine;
using TMPro;

public class GoldDiamondViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGold;
    [SerializeField] private TextMeshProUGUI textDiamond;

    private void Update()
    {
        textGold.text = PlayerData.gold.ToString();
        textDiamond.text = PlayerData.diamond.ToString();
    }
}