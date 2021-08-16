using System.Collections;
using UnityEngine;

public class AttackBounds : MonoBehaviour
{
    protected SkillData skillData;
    protected bool isOnce;

    public void Init(SkillData _skillData, float tDestroy, bool isOnce)
    {
        skillData = _skillData;
        this.isOnce = isOnce;
        if (tDestroy != -1)
        {
            StartCoroutine("InactiveTimer", tDestroy);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(skillData.casterTag)) return;

        ILivingEntity entity = other.GetComponent<ILivingEntity>();
        if (entity != null)
        {
            Attack(entity);
            if (isOnce)
            {
                ObjectPooler.ObjectInactive(ObjectPooler.skillHolder, gameObject);
            }
        }
    }

    protected virtual void Attack(ILivingEntity entity)
    {
        entity.TakeDamage(skillData.damage);
    }

    private IEnumerator InactiveTimer(float t)
    {
        yield return new WaitForSeconds(t);

        ObjectPooler.ObjectInactive(ObjectPooler.skillHolder, gameObject);
    }
}
