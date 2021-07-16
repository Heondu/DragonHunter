using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusViewer : MonoBehaviour
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private TextMeshProUGUI textStatus;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LevelUp);
    }

    private void Update()
    {
        textStatus.text = $"{DataManager.Localization(name)}\nLV.{StatusManager.GetLevel(name)}\n{StatusManager.GetStatus(name).Value}";
    }

    private void LevelUp()
    {
        StatusManager.LevelUp(name);
    }
}
