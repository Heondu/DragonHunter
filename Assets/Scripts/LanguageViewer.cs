using UnityEngine;
using TMPro;

public class LanguageViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private void Update()
    {
        switch ((int)SettingsManager.GetLanguage())
        {
            case 0: text.text = "ENGLISH"; break;
            case 1: text.text = "ÇÑ±¹¾î"; break;
        }
    }
}
