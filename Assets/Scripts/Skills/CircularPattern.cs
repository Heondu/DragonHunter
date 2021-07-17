using UnityEngine;

public class CircularPattern : Skill
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int projectileNum = 8;
    [SerializeField] private int projectileSpeed;

    public override void Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return;

        for (int i = 0; i < projectileNum; i++)
        {
            float angle = 360 / projectileNum * i * Mathf.Deg2Rad;
            skillData.dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Execute(skillData);
        }
    }

    private void Execute(SkillData skillData)
    {
        Vector3 pos = new Vector3(transform.position.x, projectile.transform.position.y, transform.position.z);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, projectile, pos, projectile.transform.rotation);
        clone.GetComponent<Projectile>().Init(skillData, projectileSpeed);
    }
}
