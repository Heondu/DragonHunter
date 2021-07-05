using UnityEngine;

public class FireballProjectile : Projectile
{
    [SerializeField]
    private GameObject explosive;

    protected override void Attack(ILivingEntity entity)
    {
        GameObject clone = ObjectPooler.Instance.ObjectPool(ObjectPooler.Instance.skillHolder, explosive, transform.position, explosive.transform.rotation);
        clone.GetComponent<Explosive>().Explode(skillData, 2);
    }
}
