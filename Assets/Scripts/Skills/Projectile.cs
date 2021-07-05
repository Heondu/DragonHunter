using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SkillData skillData;
    private int speed;

    public void Init(SkillData _skillData, int _speed)
    {
        skillData = _skillData;
        speed = _speed;
    }

    private void Update()
    {
        transform.position += skillData.dir * speed * Time.deltaTime;
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
                ObjectPooler.Instance.ObjectInactive(ObjectPooler.Instance.skillHolder, gameObject);
            }
        }
    }

    protected virtual void Attack(ILivingEntity entity)
    {
        entity.TakeDamage(skillData.damage);
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

        ObjectPooler.Instance.ObjectInactive(ObjectPooler.Instance.skillHolder, gameObject);
    }
}
