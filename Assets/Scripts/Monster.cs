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
    [SerializeField]
    protected float attackRange = 1;
    protected State state = State.None;

    private PathFinder pathFinder;
    private Transform target;
    protected Animator animator;
    private SpriteRenderer sr;

    [SerializeField]
    protected Skill[] skills;
    [SerializeField]
    protected Skill poisonExplosion;
    [SerializeField]
    protected GameObject[] marbles;

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        target = FindObjectOfType<Player>().transform;
        pathFinder.SetTarget(target);

        //FloatingDamageManager.Instance.InitHPBar(this, transform);

        for (int i = 0; i < skills.Length; i++)
        {
            skills[i] = Instantiate(skills[i], transform);
        }
    }

    private void Update()
    {
        if (state == State.None)
        {
            if (attackRange - 1 < Vector3.Distance(target.position, transform.position))
                Move();
            else
                Attack();
        }
    }

    public virtual void Init(string _id)
    {
        id = _id;
        Dictionary<string, object> data = DataManager.monsters.FindDic("ID", _id);
        atk = (int)data["ATK"];
        hp = (int)data["HP"];
        maxHp = hp;
        speed = 1;
        atkSpeed = 1;
        spawnTime = (int)data["SpawnTime"];
    }

    protected void Move()
    {
        Vector3 dir = pathFinder.GetMoveDir(target.position - transform.position);
        transform.position += dir * speed * Time.deltaTime;
        if (dir != Vector3.zero)
        {
            animator.SetBool("IsMove", true);
            sr.flipX = dir.x > 0;
        }
    }

    protected virtual void Attack()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i].timer.GetTimer(skills[i].delay))
            {
                skills[i].Attack(GetSkillData());
                animator.SetTrigger("Attack");
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

        if (hp == 0)
        {
            OnDeath();
            ObjectPooler.Instance.ObjectInactive(ObjectPooler.Instance.monsterHolder, gameObject);
        }
    }

    protected virtual void OnDeath()
    {
        if (CardManager.Instance.PoisonExplosion)
        {
            Vector3 pos = new Vector3(transform.position.x, poisonExplosion.transform.position.y, transform.position.z);
            GameObject clone = ObjectPooler.Instance.ObjectPool(ObjectPooler.Instance.skillHolder, poisonExplosion.gameObject, pos, poisonExplosion.transform.rotation);
            clone.GetComponent<Skill>().Attack(GetSkillData());
        }

        int sumOfProb = 0;
        Dictionary<string, object> data = DataManager.monsters.FindDic("ID", id);
        int[] probs = { (int)data["BlueMarbleProb"], (int)data["YellowMarbleProb"] }; 
        sumOfProb += probs[0];
        sumOfProb += probs[1];

        int rand = Random.Range(0, 100);
        int sum = 0;
        for (int i = 0; i < probs.Length; i++)
        {
            sum += probs[i];
            if (rand < sum)
            {
                Vector3 pos = new Vector3(transform.position.x, marbles[i].transform.position.y, transform.position.z);
                ObjectPooler.Instance.ObjectPool(ObjectPooler.Instance.itemHolder, marbles[i], pos, marbles[i].transform.rotation);
                break;
            }
        }

    }

    public virtual SkillData GetSkillData()
    {
        Transform target = FindTarget(attackRange);
        SkillData skillData = new SkillData();
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
