using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
public class Monster : MonoBehaviour, ILivingEntity
{
    protected string id;
    protected int atk;
    protected int hp;
    protected int maxHp;
    protected int speed;
    protected int atkSpeed;
    protected int spawnTime;
    private bool attackImmediately;
    [SerializeField]
    protected float attackRange = 1;
    protected State state = State.None;

    protected PathFinder pathFinder;
    protected Transform target;
    protected Animator animator;
    protected SpriteRenderer sr;
    private CapsuleCollider cc;
    private SpriteSetup ss;

    [SerializeField]
    protected Skill[] skills;
    [SerializeField]
    protected GameObject[] marbles;

    protected virtual void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider>();
        ss = GetComponent<SpriteSetup>();
        ss.SetupShadow(new Vector3(cc.radius * 2, cc.radius, 1), transform);

        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = Instantiate(skills[i], transform);
        }

        target = FindObjectOfType<Player>().transform;
        pathFinder.SetTarget(target);
    }

    protected virtual void Update()
    {
        if (state == State.None)
        {
            if (attackRange - 0.5f < Vector3.Distance(target.position, transform.position))
            {
                Move();
            }
            else
            {
                Attack();
            }
        }
    }

    public virtual void Init(string _id)
    {
        id = _id;
        Dictionary<string, object> data = DataManager.monsters.FindDic("ID", _id);
        atk = (int)data["ATK"] * GameManager.GetDifficulty();
        hp = (int)data["HP"] * (int)Mathf.Pow(2, GameManager.GetDifficulty() - 1);
        maxHp = hp;
        speed = 1;
        atkSpeed = 1;
        spawnTime = (int)data["SpawnTime"];
        attackImmediately = true;
        state = State.None;
    }

    protected virtual void Move()
    {
        Vector3 dir = pathFinder.GetMoveDir(target.position - transform.position);
        transform.position += dir * speed * Time.deltaTime;
        if (dir != Vector3.zero)
        {
            animator.SetBool("IsMove", true);
            sr.flipX = dir.x > 0;
        }
        else
        {
            animator.SetBool("IsMove", false);
        }
    }

    protected virtual void Attack()
    {
        animator.SetBool("IsMove", false);

        for (int i = 0; i < skills.Length; i++)
        {
            if (attackImmediately || skills[i].timer.GetTimer(skills[i].delay))
            {
                if (skills[i].Attack(GetSkillData()))
                {
                    attackImmediately = false;
                    animator.SetTrigger("Attack");
                }
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
        if (hp == 0) return;

        hp = Mathf.Max(0, hp - damage);
        FloatingDamageManager.Instance.FloatingDamage(gameObject, damage.ToString(), DamageType.Normal);

        if (hp == 0)
        {
            OnDeath();
            ObjectPooler.ObjectInactive(ObjectPooler.monsterHolder, gameObject);
        }
    }

    protected virtual void OnDeath()
    {
        if (CardManager.Instance.PoisonExplosion)
        {
            CardManager.Instance.GetPoisonExplosion.GetComponent<PoisonExplosion>().Attack(GetSkillData());
        }

        Dictionary<string, object> data = DataManager.monsters.FindDic("ID", id);
        int[] probs = { (int)data["BlueMarbleProb"], (int)data["YellowMarbleProb"] };
        
        int rand = Random.Range(0, 100);
        int sumOfProb = 0;
        for (int i = 0; i < probs.Length; i++)
        {
            sumOfProb += probs[i];
            if (rand < sumOfProb)
            {
                Vector3 pos = new Vector3(transform.position.x, marbles[i].transform.position.y, transform.position.z);
                ObjectPooler.ObjectPool(ObjectPooler.itemHolder, marbles[i], pos, marbles[i].transform.rotation);
                break;
            }
        }

        PlayerData.Gold.Set((int)data["Gold"] * GameManager.GetDifficulty(), ResourcesModType.Add);
    }

    public virtual SkillData GetSkillData()
    {
        Transform target = FindTarget(attackRange);
        SkillData skillData = new SkillData();
        skillData.caster = gameObject;
        skillData.casterTag = gameObject.tag;
        skillData.damage = atk;
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
        return (float)hp / maxHp;
    }
}
