using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected GameObject hitEffect;

    public SkillData skillData;
    private int speed;

    public void Init(SkillData _skillData, int _speed)
    {
        skillData = _skillData;
        speed = _speed;
    }

    private void Update()
    {
        transform.position += new Vector3(skillData.dir.x * speed * Time.deltaTime, skillData.dir.y * speed * Time.deltaTime, skillData.dir.z * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(skillData.casterTag)) return;

        ILivingEntity entity = other.GetComponent<ILivingEntity>();
        if (entity != null)
        {
            Attack(entity);
            skillData.penetrate--;
            if (skillData.penetrate <= 0)
            {
                ObjectPooler.ObjectInactive(ObjectPooler.skillHolder, gameObject);
            }
        }
    }

    protected virtual void Attack(ILivingEntity entity)
    {
        entity.TakeDamage(skillData.damage);
        if (hitEffect != null)
        {
            ObjectPooler.ObjectPool(ObjectPooler.effectHolder, hitEffect, transform.position, Quaternion.identity);
        }
    }

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
