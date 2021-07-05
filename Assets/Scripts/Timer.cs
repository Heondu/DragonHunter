using UnityEngine;

public class Timer
{
    private float time = 0;

    public bool GetTimer(float t)
    {
        time += Time.deltaTime;
        if (time >= t)
        {
            time = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}