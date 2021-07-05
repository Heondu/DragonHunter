using UnityEngine;

public class MonsterRangedSkill : Skill
{
    [SerializeField]
    private GameObject projectile;

    public override void Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return;
        Vector3 pos = new Vector3(transform.position.x, projectile.transform.position.y, transform.position.z);
        GameObject clone = ObjectPooler.Instance.ObjectPool(ObjectPooler.Instance.skillHolder, projectile, pos, projectile.transform.rotation);
        clone.GetComponent<Projectile>().Init(skillData, 2);
    }
}
