using UnityEngine;

public class PoisonExplosive : Explosive
{
    public override void Explode(SkillData skillData)
    {
        this.skillData = skillData;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player")) continue;
            if (collider.gameObject == skillData.caster) continue;
            ILivingEntity entity = collider.GetComponent<ILivingEntity>();
            if (entity != null)
            {
                Attack(entity);
            }
        }
    }
}
