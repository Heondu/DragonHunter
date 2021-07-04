using System.Collections;
using UnityEngine;

public struct ProjectileData
{
    public Vector3 dir;
    public string casterTag;
    public int damage;
    public int speed;
}

public class Projectile : MonoBehaviour
{
    private ProjectileData data;

    public void Init(ProjectileData _data)
    {
        data.dir = _data.dir;
        data.casterTag = _data.casterTag;
        data.damage = _data.damage;
        data.speed = _data.speed;
    }

    private void Update()
    {
        transform.position += data.dir * data.speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(data.casterTag)) return;

        ILivingEntity entity = other.GetComponent<ILivingEntity>();
        if (entity != null)
        {
            entity.TakeDamage(data.damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyTimer(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
