using UnityEngine;

public class SectorFormShot : Skill
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private int projectileNum = 5;
    [SerializeField] private float speed = 3;

    public override bool Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return false;

        float angle = Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg - 45;
        for (int i = 0; i < projectileNum; i++)
        {
            float newAngle = (angle + ((90 / projectileNum) * i)) * Mathf.Deg2Rad;
            skillData.dir = new Vector3(Mathf.Cos(newAngle), 0, Mathf.Sin(newAngle));
            Execute(skillData);
        }
        return true;
    }

    private void Execute(SkillData skillData)
    {
        Vector3 pos = new Vector3(skillData.caster.transform.position.x, projectile.transform.position.y, skillData.caster.transform.position.z);
        Quaternion rotation = Quaternion.Euler(projectile.transform.eulerAngles.x, 0, Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg + projectile.transform.eulerAngles.y);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, projectile, pos, rotation);
        clone.GetComponent<Projectile>().Init(skillData, speed);
    }
}
