using UnityEngine;

public class Frozen : Skill
{
    [SerializeField]
    private GameObject explosive;

    public override void Attack(SkillData skillData)
    {
        Vector3 pos = new Vector3(transform.position.x, explosive.transform.position.y, transform.position.z);
        GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, explosive, pos, explosive.transform.rotation, transform);
        clone.GetComponent<FrozenBounds>().Explode(skillData, 1);
    }
}
