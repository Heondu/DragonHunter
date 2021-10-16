using UnityEngine;

public class MonsterBoss : Monster
{
    protected override void OnDeath()
    {
        base.OnDeath();
        FindObjectOfType<SpawnManager>().OnBossDeath(id);
    }

    public override SkillData GetSkillData()
    {
        Transform target = FindTarget(attackRange);
        SkillData skillData = new SkillData();
        skillData.caster = gameObject;
        skillData.casterTag = gameObject.tag;
        skillData.damage = atk;
        if (target == null)
        {
            skillData.dir = Vector3.zero;
        }
        else
        {
            Vector3 targetPos = target.position;
            skillData.dir = (targetPos - transform.position).normalized;
        }
        skillData.penetrate = 1;
        return skillData;
    }
}
