using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameManager.Pause(!popup.activeSelf);
            popup.SetActive(!popup.activeSelf);
        }
    }
}
