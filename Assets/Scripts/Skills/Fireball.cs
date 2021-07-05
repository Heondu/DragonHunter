using UnityEngine;

public class Fireball : Skill
{
    [SerializeField]
    private GameObject projectile;

    public override void Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return;
        Vector3 pos = new Vector3(transform.position.x, projectile.transform.position.y, transform.position.z);
        GameObject clone = Instantiate(projectile, pos, projectile.transform.rotation);
        clone.GetComponent<FireballProjectile>().Init(skillData, 2);
    }
}
