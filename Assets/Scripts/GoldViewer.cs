using UnityEngine;
using TMPro;

public class GoldViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGold;

    private void Update()
    {
        textGold.text = PlayerData.Gold.Value.ToString();
    }
}