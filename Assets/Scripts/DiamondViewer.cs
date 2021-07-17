using UnityEngine;
using TMPro;

public class DiamondViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDiamond;

    private void Update()
    {
        textDiamond.text = PlayerData.Diamond.Value.ToString();
    }
}