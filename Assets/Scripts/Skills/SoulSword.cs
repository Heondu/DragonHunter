using System.Collections;
using UnityEngine;

public class SoulSword : Skill
{
    [SerializeField] private float speed = 50;
    private AttackBounds[] bounds;
    private ILivingEntity entity;

    public void Start()
    {
        bounds = GetComponentsInChildren<AttackBounds>();
        entity = GetComponentInParent<ILivingEntity>();
        StartCoroutine("AttackCo");
    }

    private IEnumerator AttackCo()
    {
        float angle = 0; 
        while (true)
        {
            angle += speed * Time.deltaTime;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            for (int i = 0; i < bounds.Length; i++)
            {
                bounds[i].Init(entity.GetSkillData(), false);
            }

            yield return null;
        }
    }
}
