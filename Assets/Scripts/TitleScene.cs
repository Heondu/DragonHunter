using UnityEngine;

public class TitleScene : MonoBehaviour
{
    private void Update()
    {
#if UNITY_EDITOR
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
