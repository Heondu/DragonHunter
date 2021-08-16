using UnityEngine;

public struct SkillData
{
    public GameObject caster;
    public string casterTag;
    public int damage;
    public Vector3 dir;
    public int penetrate;
}

public class Skill : MonoBehaviour
{
    public float delay = 0;
    public Timer timer = new Timer();

    public virtual bool Attack(SkillData skillData)
    {
        return true;
    }
}
