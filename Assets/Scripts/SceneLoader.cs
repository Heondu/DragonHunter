using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string name)
    {
        LoadingSceneManager.LoadScene(name);
    }
}
