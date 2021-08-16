using UnityEngine;

public class Fireball : Skill
{
    [SerializeField]
    private GameObject projectile;

    public override bool Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return false;
        Vector3 pos = new Vector3(skillData.caster.transform.position.x, projectile.transform.position.y, skillData.caster.transform.position.z);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, projectile, pos, projectile.transform.rotation);
        clone.GetComponent<FireballProjectile>().Init(skillData, 2);

        return true;
    }
}
