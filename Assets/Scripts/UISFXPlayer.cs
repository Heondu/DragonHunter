using UnityEngine;

public class UISFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.PlaySFX(clip);
        }
    }
}
