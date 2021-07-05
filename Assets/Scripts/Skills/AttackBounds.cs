using UnityEngine;

public class AttackBounds : MonoBehaviour
{
    protected SkillData skillData;

    public void Init(SkillData _skillData)
    {
        skillData = _skillData;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(skillData.casterTag)) return;

        ILivingEntity entity = other.GetComponent<ILivingEntity>();
        if (entity != null)
        {
            Attack(entity);
        }
    }

    protected virtual void Attack(ILivingEntity entity)
    {
        entity.TakeDamage(skillData.damage);
    }
}
