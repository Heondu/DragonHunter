using UnityEngine;

public class PenetratingBeam : Skill
{
    [SerializeField]
    private GameObject beam;

    public override bool Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return false;
        Vector3 pos = new Vector3(skillData.caster.transform.position.x, beam.transform.position.y, skillData.caster.transform.position.z);
        Quaternion rotation = Quaternion.Euler(beam.transform.eulerAngles.x, 0, Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, beam, pos, rotation, skillData.caster.transform);
        clone.GetComponent<AttackBounds>().Init(skillData, 1, false);

        return true;
    }
}
