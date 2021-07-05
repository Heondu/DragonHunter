using UnityEngine;

public class Lightning : Skill
{
    [SerializeField]
    private GameObject lightningBolt;

    public override void Attack(SkillData skillData)
    {
        int rand = Random.Range(3, 6);
        for (int i = 0; i < rand; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, lightningBolt.transform.position.y, transform.position.z);
            Quaternion rotation = Quaternion.Euler(lightningBolt.transform.eulerAngles.x, 0, Random.Range(0, 360));
            GameObject clone = ObjectPooler.Instance.ObjectPool(ObjectPooler.Instance.skillHolder, lightningBolt, pos, rotation, transform);
            clone.GetComponent<AttackBounds>().Init(skillData);
            Destroy(clone, 1);
        }
    }
}
