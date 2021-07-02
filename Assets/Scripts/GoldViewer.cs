using UnityEngine;
using TMPro;

public class GoldViewer : MonoBehaviour
{
    private TextMeshProUGUI textGold;

    private void Start()
    {
        textGold = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textGold.text = $"G : {PlayerData.gold}";
    }
}