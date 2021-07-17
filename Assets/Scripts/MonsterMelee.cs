using System.Collections;
using UnityEngine;

public class MonsterMelee : Monster
{
    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            StartCoroutine("InactiveTimer", 5);
    }

    private void OnBecameVisible()
    {
        StopCoroutine("InactiveTimer");
    }

    private void OnDisable()
    {
        StopCoroutine("InactiveTimer");
    }

    private IEnumerator InactiveTimer(float t)
    {
        yield return new WaitForSeconds(t);

        ObjectPooler.ObjectInactive(ObjectPooler.skillHolder, gameObject);
    }
}