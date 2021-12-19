using UnityEngine;

public class TitleScene : MonoBehaviour
{
    private void Update()
    {
#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)
        if (Input.GetMouseButtonUp(0))
        {
            LoadingSceneManager.LoadScene("Main");
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            LoadingSceneManager.LoadScene("Main");
        }
#endif
    }
}
