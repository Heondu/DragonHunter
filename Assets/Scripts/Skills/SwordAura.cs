using System.Collections;
using UnityEngine;

public class SwordAura : Skill
{
    [SerializeField]
    private GameObject projectile;

    public override void Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return;
        StartCoroutine("AttackCo", skillData);
    }

    private IEnumerator AttackCo(SkillData skillData)
    {
        Vector3 dir = skillData.dir;
        skillData.penetrate = CardManager.Instance.Penetrate;
        if (CardManager.Instance.DiagonalAttack)
        {
            skillData.dir = dir;
            float angle = Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg;
            float left = (angle - 30) * Mathf.Deg2Rad;
            float right = (angle + 30) * Mathf.Deg2Rad;
            skillData.dir = new Vector3(Mathf.Cos(left), 0, Mathf.Sin(left));
            Execute(skillData);
            skillData.dir = new Vector3(Mathf.Cos(right), 0, Mathf.Sin(right));
            Execute(skillData);
        }
        if (CardManager.Instance.BackAttack)
        {
            skillData.dir = dir;
            float angle = Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg;
            float back = (angle - 180) * Mathf.Deg2Rad;
            skillData.dir = new Vector3(Mathf.Cos(back), 0, Mathf.Sin(back));
            Execute(skillData);
        }

        for (int i = 0; i < CardManager.Instance.Repeat; i++)
        {
            skillData.dir = dir;
            Execute(skillData);

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void Execute(SkillData skillData)
    {
        Vector3 pos = new Vector3(transform.position.x, projectile.transform.position.y, transform.position.z);
        GameObject clone = ObjectPooler.Instance.ObjectPool(ObjectPooler.Instance.skillHolder, projectile, pos, projectile.transform.rotation);
        clone.GetComponent<Projectile>().Init(skillData, 5);
    }
}
