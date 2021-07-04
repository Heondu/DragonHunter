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

    private PathFinder pathFinder;
    private Transform target;
    private Animator animator;
    private SpriteRenderer sr;

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        target = FindObjectOfType<Player>().transform;
        pathFinder.SetTarget(target);

        StartCoroutine("Attack");
    }

    private void Update()
    {
        Move();
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

    protected virtual IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            Skill[] skills = GetComponentsInChildren<Skill>();
            for (int i = 0; i < skills.Length; i++)
            {
                if (skills[i].Attack(gameObject.tag, atk))
                {
                    animator.SetTrigger("Attack");
                }
            }
        }
    }

    public virtual void TakeDamage(int damage)
    {
        hp = Mathf.Max(0, hp - damage);

        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

    private void AutoDestroy()
    {
        if (Vector3.Distance(target.position, transform.position) > 20)
        {
            Destroy(gameObject);
        }
    }
}
