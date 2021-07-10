using UnityEngine;

public class Portal : Trap
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !SpawnManager.IsBossSpawn)
        {
            other.transform.position += new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            ObjectPooler.Instance.ObjectInactive(ObjectPooler.Instance.trapHolder, gameObject);
        }
    }
}
