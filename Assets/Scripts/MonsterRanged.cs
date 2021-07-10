using System.Collections;
using UnityEngine;

public class MonsterRanged : Monster
{
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

        ObjectPooler.Instance.ObjectInactive(ObjectPooler.Instance.skillHolder, gameObject);
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