using UnityEngine;

public class CircularPattern : Skill
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int projectileNum = 8;
    [SerializeField] private int projectileSpeed;

    public override bool Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return false;

        for (int i = 0; i < projectileNum; i++)
        {
            float angle = 360 / projectileNum * i * Mathf.Deg2Rad;
            skillData.dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Execute(skillData);
        }
        return true;
    }

    private void Execute(SkillData skillData)
    {
        Vector3 pos = new Vector3(skillData.caster.transform.position.x, projectile.transform.position.y, skillData.caster.transform.position.z);
        Quaternion rotation = Quaternion.Euler(projectile.transform.eulerAngles.x, 0, Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg + projectile.transform.eulerAngles.y);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, projectile, pos, rotation);
        clone.GetComponent<Projectile>().Init(skillData, projectileSpeed);
    }
}
