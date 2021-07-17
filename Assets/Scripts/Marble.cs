using System.Collections;
using UnityEngine;

public enum MarbleType { Blue, Yellow }

public class Marble : MonoBehaviour
{
    [SerializeField]
    private MarbleType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == MarbleType.Blue) CardManager.Instance.ShowCard();
            else if (type == MarbleType.Yellow) CardManager.Instance.ShowSpecialCard();
            ObjectPooler.ObjectInactive(ObjectPooler.itemHolder, gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            StartCoroutine("InactiveTimer", 20);
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

        ObjectPooler.ObjectInactive(ObjectPooler.itemHolder, gameObject);
    }
}
