using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int difficulty;
    private int killCount;
    private float tElapsed = 0;
    private int isClear;
    private int score;

    void Update()
    {
        tElapsed += Time.deltaTime;
    }

    public float GetTime()
    {
        return tElapsed;
    }
}
