using UnityEngine;

public class Lightning : Skill
{
    [SerializeField]
    private GameObject projectile;

    public override bool Attack(SkillData skillData)
    {
        int rand = Random.Range(3, 6);
        for (int i = 0; i < rand; i++)
        {
            float angle = Random.Range(0, 360);
            skillData.dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
            Vector3 pos = new Vector3(skillData.caster.transform.position.x, projectile.transform.position.y, skillData.caster.transform.position.z);
            Quaternion rotation = Quaternion.Euler(projectile.transform.eulerAngles.x, 0, angle + projectile.transform.eulerAngles.y);
            GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, projectile, pos, rotation);
            clone.GetComponent<Projectile>().Init(skillData, 5);
        }

        return true;
    }
}
