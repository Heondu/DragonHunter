using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    protected int hp;
    protected int lv;
    protected int maxHp;
    protected int exp;
    public int penetrate = 1;
    public int repeat = 1;
    public bool diagonalAttack = false;
    public bool backAttack = false;
    private float attackRange = 6;
    private State state = State.None;

    private Animator animator;
    private SpriteRenderer sr;

    [SerializeField]
    private List<Skill> skills = new List<Skill>();

    private void Start()
    {
        hp = (int)StatusManager.GetStatus("hp").Value;

        for (int i = 0; i < skills.Count; i++)
        {
            skills[i] = Instantiate(skills[i], transform);
        }

        //FloatingDamageManager.Instance.InitHPBar(this, transform);

        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (state == State.None)
        {
            Move();
            Attack();
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(x, 0, z).normalized * StatusManager.GetStatus("speed").Value * Time.deltaTime;
        if (x != 0 || z != 0)
        {
            animator.SetBool("IsMove", true);
            sr.flipX = x > 0;
        }
    }

    private void Attack()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].timer.GetTimer(skills[i].delay))
            {
                skills[i].Attack(GetSkillData());
                //animator.SetTrigger("Attack");
            }
        }
    }

    public Transform FindTarget(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        Transform target = null;
        float distance = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<ILivingEntity>() == null) continue;
            if (collider.CompareTag(gameObject.tag)) continue;

            float newDistance = Vector3.Distance(collider.transform.position, transform.position);
            if (distance > newDistance)
            {
                target = collider.transform;
                distance = newDistance;
            }
        }
        return target;
    }

    public void TakeDamage(int damage)
    {
        hp = Mathf.Max(0, hp - damage);
        FloatingDamageManager.Instance.FloatingDamage(gameObject, damage.ToString(), DamageType.Normal);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="modifier">0 = 더하기, 1 = 퍼센트</param>
    public void Restore(int value, int modifier)
    {
        if (modifier == 0)
        {
            hp = Mathf.Min(hp + value, (int)StatusManager.GetStatus("hp").Value);
        }
        else if (modifier == 1)
        {
            hp = Mathf.Min(hp + (int)(StatusManager.GetStatus("hp").Value * 0.2f), (int)StatusManager.GetStatus("hp").Value);
        }
    }

    public void AddSkill(string name)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Skills/" + name);
        prefab = Instantiate(prefab, transform);
        skills.Add(prefab.GetComponent<Skill>());
    }

    public SkillData GetSkillData()
    {
        Transform target = FindTarget(attackRange);
        SkillData skillData = new SkillData();
        skillData.casterTag = gameObject.tag;
        skillData.damage = (int)StatusManager.GetStatus("atk").Value;
        skillData.dir = target == null ? Vector3.zero : (target.position - transform.position).normalized;
        skillData.penetrate = 1;
        return skillData;
    }

    public void ChangeState(State _state, float t)
    {
        state = _state;
        StartCoroutine("StateTimer", t);
    }

    private IEnumerator StateTimer(float t)
    {
        yield return new WaitForSeconds(t);

        state = State.None;
    }

    public float GetHP()
    {
        return hp / StatusManager.GetStatus("hp").Value;
    }
}
