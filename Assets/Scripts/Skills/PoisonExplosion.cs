using UnityEngine;

public class PoisonExplosion : Skill
{
    [SerializeField]
    private GameObject explosive;

    public override bool Attack(SkillData skillData)
    {
        Vector3 pos = new Vector3(skillData.caster.transform.position.x, explosive.transform.position.y, skillData.caster.transform.position.z);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, explosive, pos, explosive.transform.rotation);
        clone.GetComponent<PoisonExplosive>().Explode(skillData);

        return true;
    }
}
