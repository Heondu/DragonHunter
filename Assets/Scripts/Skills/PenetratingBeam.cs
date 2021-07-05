using UnityEngine;

public class PenetratingBeam : Skill
{
    [SerializeField]
    private GameObject beam;

    public override void Attack(SkillData skillData)
    {
        if (skillData.dir == Vector3.zero) return;
        Vector3 pos = new Vector3(transform.position.x, beam.transform.position.y, transform.position.z);
        Quaternion rotation = Quaternion.Euler(beam.transform.eulerAngles.x, 0, Mathf.Atan2(skillData.dir.z, skillData.dir.x) * Mathf.Rad2Deg);
        GameObject clone = ObjectPooler.Instance.ObjectPool(ObjectPooler.Instance.skillHolder, beam, pos, rotation, transform);
        clone.GetComponent<AttackBounds>().Init(skillData);
        Destroy(clone, 1);
    }
}
