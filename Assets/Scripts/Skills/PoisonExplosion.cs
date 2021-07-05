using UnityEngine;

public class PoisonExplosion : Skill
{
    [SerializeField]
    private GameObject explosive;

    public override void Attack(SkillData skillData)
    {
        Vector3 pos = new Vector3(transform.position.x, explosive.transform.position.y, transform.position.z);
        GameObject clone = ObjectPooler.Instance.ObjectPool(ObjectPooler.Instance.skillHolder, explosive, pos, explosive.transform.rotation, transform);
        clone.GetComponent<Explosive>().Explode(skillData, 1);
        Destroy(gameObject, 1);
    }
}
