using System.Collections;
using UnityEngine;

public class CharacterDefaultSkill : Skill
{
    [SerializeField]
    private GameObject projectile;

    public override bool Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return false;
        StartCoroutine("AttackCo", skillData);

        return true;
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
        Quaternion rotation = Quaternion.Euler(projectile.transform.eulerAngles.x, 0, Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg + projectile.transform.eulerAngles.y);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, projectile, pos, rotation);
        clone.GetComponent<Projectile>().Init(skillData, 5);
    }
}
