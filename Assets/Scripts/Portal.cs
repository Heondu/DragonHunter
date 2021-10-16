using UnityEngine;

public class Portal : Trap
{
    [SerializeField] private AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.spawnManager.IsBossSpawn)
        {
            other.transform.position += new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            ObjectPooler.ObjectInactive(ObjectPooler.trapHolder, gameObject);
            SoundManager.Instance.PlaySFX(clip);
        }
    }
}
