using UnityEngine;

public class MonsterBoss : Monster
{
    protected override void OnDeath()
    {
        base.OnDeath();
        FindObjectOfType<SpawnManager>().OnBossDeath();
    }

    public override SkillData GetSkillData()
    {
        Transform target = FindTarget(attackRange);
        SkillData skillData = new SkillData();
        skillData.casterTag = gameObject.tag;
        skillData.damage = atk;
        if (target == null)
        {
            skillData.dir = Vector3.zero;
        }
        else
        {
            Vector3 targetPos = target.position + new Vector3(Random.Range(-1f, 1f), target.position.y, Random.Range(-1f, 1f));
            skillData.dir = (targetPos - transform.position).normalized;
        }
        skillData.penetrate = 1;
        return skillData;
    }
}
