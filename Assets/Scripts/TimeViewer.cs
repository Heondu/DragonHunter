using UnityEngine;
using TMPro;

public class TimeViewer : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private TextMeshProUGUI timeText;

    private void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        int time = (int)gameManager.GetTime();

        int min = time / 60;
        int hour = min / 60;
        int sec = time % 60;
        min = min % 60;

        timeText.text = hour == 0 ? $"{min} : {sec}" : $"{hour} : {min} : {sec}";
    }
}
