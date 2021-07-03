using System.Collections;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    private Transform player;

    public void Init(int lifetime, Transform _player)
    {
        player = _player;
        StartCoroutine("Timer", lifetime);
    }

    private IEnumerator Timer(int lifetime)
    {
        float t = 0;
        while (t < lifetime)
        {
            if (Vector3.Distance(player.position, transform.position) > 11)
            {
                t += Time.deltaTime;
            }
            yield return null;
        }

        Destroy(gameObject);
    }
}
