using UnityEngine;

public class Preferences : MonoBehaviour
{
    public void SetLanguage(int value)
    {
        SettingsManager.SetLanguage(value);
    }

    public void SetBGM(float value)
    {
        SoundManager.Instance.SetBGMVolume(value);
    }

    public void SetSFX(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
    }
}
