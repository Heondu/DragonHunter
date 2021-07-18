using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Difficulty : MonoBehaviour
{
    [SerializeField]
    private int difficulty;
    [SerializeField]
    private TextMeshProUGUI text;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetupDifficulty);
        text.text = $"≥≠¿Ãµµ : {difficulty}";
    }

    private void SetupDifficulty()
    {
        GameManager.SetDifficulty(difficulty);
    }
}
