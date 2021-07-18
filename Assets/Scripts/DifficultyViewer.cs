using UnityEngine;
using UnityEngine.UI;

public class DifficultyViewer : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite[] icons;

    private void Update()
    {
        image.sprite = icons[GameManager.GetDifficulty() - 1];
    }
}
