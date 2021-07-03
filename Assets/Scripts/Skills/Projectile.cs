using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 dir;
    private string casterTag;
    private int damage;
    private int speed;

    public void Init(Vector3 _dir, string _casterTag, int _damage, int _speed)
    {
        dir = _dir;
        casterTag = _casterTag;
        damage = _damage;
        speed = _speed;
    }

    private void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(casterTag)) return;

        ILivingEntity entity = other.GetComponent<ILivingEntity>();
        if (entity != null)
        {
            entity.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyTimer(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
