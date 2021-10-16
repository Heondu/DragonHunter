using UnityEngine;

public class Fireball : Skill
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float speed = 2;

    public override bool Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return false;
        Vector3 pos = new Vector3(skillData.caster.transform.position.x, projectile.transform.position.y, skillData.caster.transform.position.z);
        Quaternion rotation = Quaternion.Euler(projectile.transform.eulerAngles.x, 0, Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg + projectile.transform.eulerAngles.y);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, projectile, pos, rotation);
        clone.GetComponent<FireballProjectile>().Init(skillData, speed);

        return true;
    }
}
