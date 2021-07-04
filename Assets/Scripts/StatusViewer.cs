using UnityEngine;
using TMPro;

public class StatusViewer : MonoBehaviour
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private TextMeshProUGUI textStatus;

    private void Update()
    {
        textStatus.text = $"{name}\nLV.{StatusManager.GetLevel(name)}";
    }
}
