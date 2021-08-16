using UnityEngine;

public class Lightning : Skill
{
    [SerializeField]
    private GameObject lightningBolt;

    public override bool Attack(SkillData skillData)
    {
        int rand = Random.Range(3, 6);
        for (int i = 0; i < rand; i++)
        {
            Vector3 pos = new Vector3(skillData.caster.transform.position.x, lightningBolt.transform.position.y, skillData.caster.transform.position.z);
            Quaternion rotation = Quaternion.Euler(lightningBolt.transform.eulerAngles.x, 0, Random.Range(0, 360));
            GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, lightningBolt, pos, rotation, skillData.caster.transform);
            clone.GetComponent<AttackBounds>().Init(skillData, 1, false);
        }

        return true;
    }
}
