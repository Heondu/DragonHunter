using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f;

    private void OnEnable()
    {
        SoundManager.Instance.PlaySFX(clip, volume);
    }
}
