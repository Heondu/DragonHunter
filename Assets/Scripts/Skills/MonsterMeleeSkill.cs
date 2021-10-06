using UnityEngine;

public class MonsterMeleeSkill : Skill
{
    [SerializeField]
    private GameObject attackBounds;

    public override bool Attack(SkillData skillData)
    {
        Vector3 pos = new Vector3(skillData.caster.transform.position.x, attackBounds.transform.position.y, skillData.caster.transform.position.z);
        float angle = Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, attackBounds, pos, rotation, skillData.caster.transform);
        clone.GetComponent<AttackBounds>().Init(skillData, true);

        return true;
    }
}
