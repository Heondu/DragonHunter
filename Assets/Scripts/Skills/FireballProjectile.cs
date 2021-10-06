using UnityEngine;

public class FireballProjectile : Projectile
{
    protected override void Attack(ILivingEntity entity)
    {
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, hitEffect, transform.position, hitEffect.transform.rotation);
        clone.GetComponent<Explosive>().Explode(skillData);
    }
}
