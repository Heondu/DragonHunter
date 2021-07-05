using System.Collections;
using UnityEngine;

public class SoulShield : Skill
{
    [SerializeField] private float speed = 50;
    private Shield[] shields;
    private ILivingEntity entity;

    public void Start()
    {
        shields = GetComponentsInChildren<Shield>();
        entity = GetComponentInParent<ILivingEntity>();
        for (int i = 0; i < shields.Length; i++)
        {
            shields[i].Init(entity.GetSkillData(), this);
        }
        StartCoroutine("AttackCo");
    }

    private IEnumerator AttackCo()
    {
        float angle = 0;
        while (true)
        {
            angle += speed * Time.deltaTime;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);

            yield return null;
        }
    }

    public IEnumerator InactiveShield(GameObject shield)
    {
        shield.SetActive(false);

        yield return new WaitForSeconds(15);

        shield.SetActive(true);
    }
}
