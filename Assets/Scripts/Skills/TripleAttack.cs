using System.Collections;
using UnityEngine;

public class TripleAttack : Skill
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float attackDelay = 0.5f;
    [SerializeField]
    private float speed = 3;

    public override bool Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return false;

        StartCoroutine("AttackCo", skillData);

        return true;
    }

    private IEnumerator AttackCo(SkillData skillData)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(skillData.caster.transform.position.x, projectile.transform.position.y, skillData.caster.transform.position.z);
            Quaternion rotation = Quaternion.Euler(projectile.transform.eulerAngles.x, 0, Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg + projectile.transform.eulerAngles.y);
            GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, projectile, pos, rotation);
            clone.GetComponent<FireballProjectile>().Init(skillData, speed);

            yield return new WaitForSeconds(attackDelay);
        }
    }
}
