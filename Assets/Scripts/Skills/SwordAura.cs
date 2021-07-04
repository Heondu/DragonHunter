using UnityEngine;

public class SwordAura : Skill
{
    [SerializeField]
    private GameObject projectile;

    public override bool Attack(string casterTag, int damage)
    {
        Transform target = FindTarget();
        if (target != null)
        {
            GameObject clone = Instantiate(projectile, transform.position, projectile.transform.rotation);
            ProjectileData data = new ProjectileData();
            data.dir = (target.position - transform.position).normalized;
            data.casterTag = casterTag;
            data.damage = damage;
            data.speed = 5;
            clone.GetComponent<Projectile>().Init(data);
            return true;
        }
        else return false;
    }
}
