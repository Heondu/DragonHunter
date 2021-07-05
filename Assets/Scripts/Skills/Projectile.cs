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
                Destroy(gameObject);
            }
        }
    }

    protected virtual void Attack(ILivingEntity entity)
    {
        entity.TakeDamage(skillData.damage);
    }

    private IEnumerator DestroyTimer(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }
}
