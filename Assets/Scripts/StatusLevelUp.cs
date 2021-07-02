using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusLevelUp : MonoBehaviour
{
    [SerializeField]
    private StatusList status;
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
        textStatus.text = $"{status}\nLV.{StatusManager.GetLevel(status)}";
    }

    private void LevelUp()
    {
        StatusManager.LevelUp(status);
    }
}
