using System.Collections;
using UnityEngine;

public class MonsterMelee : Monster
{
    private GameObject hpBar;

    public override void Init(string _id)
    {
        base.Init(_id);
        hpBar = FloatingDamageManager.Instance.InitHPBar(this, transform);
        hpBar.SetActive(true);
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ILivingEntity entity = other.gameObject.GetComponent<ILivingEntity>();
            if (entity != null)
            {
                entity.TakeDamage(GetSkillData().damage);
            }
        }
    }
}