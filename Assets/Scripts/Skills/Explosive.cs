using System.Collections;
using UnityEngine;

public class Explosive : EffectAutoDestroyer
{
    protected SkillData skillData;
    [SerializeField]
    protected float radius = 1;

    public virtual void Explode(SkillData skillData)
    {
        this.skillData = skillData;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(skillData.casterTag)) continue;
            ILivingEntity entity = collider.GetComponent<ILivingEntity>();
            if (entity != null)
            {
                Attack(entity);
            }
        }
    }

    protected virtual void Attack(ILivingEntity entity)
    {
        entity.TakeDamage(skillData.damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
