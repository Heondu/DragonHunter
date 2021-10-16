using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject soundManager = GameObject.Find("SoundManager");
                if (soundManager == null)
                {
                    soundManager = new GameObject("SoundManager");
                    soundManager.AddComponent<SoundManager>();

                    instance = soundManager.GetComponent<SoundManager>();
                    instance.Init();
                }
            }
            return instance;
        }
    }

    private AudioSource bgmPlayer;
    private List<AudioSource> sfxPlayerList = new List<AudioSource>();

    private float bgmVolume = 1;
    private float sfxVolume = 1;

    private void Init()
    {
        GameObject audioSource = new GameObject("BGM");
        bgmPlayer = audioSource.AddComponent<AudioSource>();
        bgmPlayer.transform.parent = instance.transform;


        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;

    }

    private void AddSFXPlayer()
    {
        GameObject clone = new GameObject("SFX");
        AudioSource audioSource = clone.AddComponent<AudioSource>();
        audioSource.transform.parent = instance.transform;

        audioSource.playOnAwake = false;
        audioSource.loop = false;

        sfxPlayerList.Add(audioSource);
    }

    public void PlayBGM(AudioClip clip, float volume = 1)
    {
        bgmPlayer.volume = volume * SettingsManager.GetBGMVolume;
        bgmPlayer.clip = clip;
        bgmPlayer.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1)
    {
        int playingNum = 1;
        int notPlayingIndex = -1;
        for (int i = 0; i < sfxPlayerList.Count; i++)
        {
            if (sfxPlayerList[i].isPlaying) playingNum++;
            else if (notPlayingIndex == -1) notPlayingIndex = i;
        }

        if (notPlayingIndex == -1)
        {
            AddSFXPlayer();
            notPlayingIndex = sfxPlayerList.Count - 1;
        }

        foreach (AudioSource audioSource in sfxPlayerList)
        {
            if (audioSource.isPlaying)
                audioSource.volume = (0.3f + (0.7f / playingNum)) * SettingsManager.GetSFXVolume;
        }

        sfxPlayerList[notPlayingIndex].volume = (0.3f + (0.7f / playingNum)) * SettingsManager.GetSFXVolume;
        sfxPlayerList[notPlayingIndex].PlayOneShot(clip);
    }

    public void SetBGMVolume(float value)
    {
        SettingsManager.SetBGMVolume(value);
        bgmPlayer.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        SettingsManager.SetSFXVolume(value);
        foreach (AudioSource audioSource in sfxPlayerList)
        {
            audioSource.volume = value;
        }
    }

    public void Mute(bool value)
    {
        if (value)
        {
            bgmVolume = SettingsManager.GetBGMVolume;
            sfxVolume = SettingsManager.GetSFXVolume;
            SetBGMVolume(0);
            SetSFXVolume(0);
        }
        else
        {
            SetBGMVolume(bgmVolume);
            SetSFXVolume(sfxVolume);
        }
    }

    public void Stop(bool value)
    {
        if (value)
        {
            bgmPlayer.Pause();
            foreach (AudioSource audioSource in sfxPlayerList)
            {
                audioSource.Pause();
            }
        }
        else
        {
            bgmPlayer.UnPause();
            foreach (AudioSource audioSource in sfxPlayerList)
            {
                audioSource.UnPause();
            }
        }
    }
}