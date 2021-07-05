using UnityEngine;

public class MonsterMeleeSkill : Skill
{
    private ILivingEntity entity;
    private SkillData skillData;

    private void Start()
    {
        entity = GetComponentInParent<ILivingEntity>();
    }

    private void Update()
    {
        skillData = entity.GetSkillData();
        float angle = Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        skillData = this.entity.GetSkillData();
        if (other.CompareTag(skillData.casterTag)) return;

        ILivingEntity entity = other.GetComponent<ILivingEntity>();
        if (entity != null)
        {
            entity.TakeDamage(skillData.damage);
        }
    }
}
