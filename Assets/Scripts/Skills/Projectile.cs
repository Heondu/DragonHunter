using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected GameObject hitEffect;

    public SkillData skillData;
    private float speed;

    public void Init(SkillData _skillData, float _speed)
    {
        skillData = _skillData;
        speed = _speed;
    }

    private void Update()
    {
        transform.position += new Vector3(skillData.dir.x * speed * Time.deltaTime, skillData.dir.y * speed * Time.deltaTime, skillData.dir.z * speed * Time.deltaTime);

        if (ObjectPooler.CheckForDistance(Vector3.Distance(skillData.caster.transform.position, transform.position)))
        {
            ObjectPooler.ObjectInactive(ObjectPooler.skillHolder, gameObject);
        }
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
}
