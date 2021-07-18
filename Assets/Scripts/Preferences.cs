using UnityEngine;

public class Preferences : MonoBehaviour
{
    public void SetLanguage(int value)
    {
        SettingsManager.SetLanguage(value);
    }

    public void SetBGM(float value)
    {
        SettingsManager.setBGM(value);
    }

    public void SetSE(float value)
    {
        SettingsManager.setSE(value);
    }
}
