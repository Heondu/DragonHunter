using UnityEngine;

public class MonsterRanged : Monster
{
    private GameObject hpBar;

    public override void Init(string _id)
    {
        base.Init(_id);
        hpBar = FloatingDamageManager.Instance.InitHPBar(this, transform);
        hpBar.SetActive(true);
    }

    protected override void Update()
    {
        base.Update();

        if (ObjectPooler.CheckForDistance(Vector3.Distance(target.transform.position, transform.position)))
        {
            ObjectPooler.ObjectInactive(ObjectPooler.monsterHolder, gameObject);
        }
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
            Vector3 targetPos = target.position + new Vector3(Random.Range(-1f, 1f), target.position.y, Random.Range(-1f, 1f));
            skillData.dir = (targetPos - transform.position).normalized;
        }
        skillData.penetrate = 1;
        return skillData;
    }
}