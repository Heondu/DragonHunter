using System.Collections;
using UnityEngine;

public class AttackBounds : MonoBehaviour
{
    protected SkillData skillData;
    protected bool isOnce;

    public void Init(SkillData _skillData, bool isOnce)
    {
        skillData = _skillData;
        this.isOnce = isOnce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(skillData.casterTag)) return;

        ILivingEntity entity = other.GetComponent<ILivingEntity>();
        if (entity != null)
        {
            Attack(entity);
            if (isOnce)
            {
                ObjectPooler.ObjectInactive(ObjectPooler.skillHolder, gameObject);
            }
        }
    }

    protected virtual void Attack(ILivingEntity entity)
    {
        entity.TakeDamage(skillData.damage);
    }
}
