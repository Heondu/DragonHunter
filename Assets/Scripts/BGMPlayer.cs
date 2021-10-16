using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f;

    private void OnEnable()
    {
        SoundManager.Instance.PlayBGM(clip, volume);
    }
}
