using System.Collections;
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
        if (state == State.None)
        {
            if (attackRange / 2 < Vector3.Distance(target.position, transform.position))
                Move(true);
            else
            {
                Attack();
                Move(true);
            }
        }
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
            StartCoroutine("InactiveTimer", 5);
    }

    private void OnBecameVisible()
    {
        StopCoroutine("InactiveTimer");
    }

    private void OnDisable()
    {
        StopCoroutine("InactiveTimer");
    }

    private IEnumerator InactiveTimer(float t)
    {
        yield return new WaitForSeconds(t);

        ObjectPooler.ObjectInactive(ObjectPooler.skillHolder, gameObject);
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