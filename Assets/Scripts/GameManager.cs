using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int difficulty;
    private int killCount;
    private float tElapsed = 0;
    private int isClear;
    private int score;
    [SerializeField]
    private SpawnManager spawnManager;

    void Update()
    {
        if (spawnManager.IsBossSpawn == false)
        {
            tElapsed += Time.deltaTime;
        }
    }

    public float GetTime()
    {
        return tElapsed;
    }
}
