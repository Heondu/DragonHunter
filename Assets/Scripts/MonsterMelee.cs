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

    protected override void Update()
    {
        base.Update();

        if (ObjectPooler.CheckForDistance(Vector3.Distance(target.transform.position, transform.position)))
        {
            ObjectPooler.ObjectInactive(ObjectPooler.monsterHolder, gameObject);
        }
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