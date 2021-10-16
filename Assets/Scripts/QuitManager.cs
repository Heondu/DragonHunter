using UnityEngine;

public class QuitManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            popup.SetActive(!popup.activeSelf);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
