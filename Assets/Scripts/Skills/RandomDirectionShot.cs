using System.Collections;
using UnityEngine;

public class RandomDirectionShot : Skill
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private int projectileNum = 7;
    [SerializeField]
    private float attackDelay = 0.5f;
    [SerializeField]
    private float speed = 2;

    public override bool Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return false;

        StartCoroutine("AttackCo", skillData);

        return true;
    }

    private IEnumerator AttackCo(SkillData skillData)
    {
        float angle = Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg;
        for (int i = 0; i < projectileNum; i++)
        {
            Vector3 pos = new Vector3(skillData.caster.transform.position.x, projectile.transform.position.y, skillData.caster.transform.position.z);
            float newAngle = Random.Range(angle - 15, angle + 15);
            skillData.dir = new Vector3(Mathf.Cos(newAngle * Mathf.Deg2Rad), 0, Mathf.Sin(newAngle * Mathf.Deg2Rad));
            Quaternion rotation = Quaternion.Euler(projectile.transform.eulerAngles.x, 0, newAngle + projectile.transform.eulerAngles.y);
            GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, projectile, pos, rotation);
            clone.GetComponent<FireballProjectile>().Init(skillData, speed);

            yield return new WaitForSeconds(attackDelay);
        }
    }
}
