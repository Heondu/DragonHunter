using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private SkillData skillData;
    private Skill skill;

    public void Init(SkillData _skillData, Skill _skill)
    {
        skillData = _skillData;
        skill = _skill;
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile != null)
        {
            if (projectile.skillData.casterTag != skillData.casterTag)
            {
                Destroy(other);
                skill.StartCoroutine("InactiveShield", gameObject);
            }
        }
    }
}
