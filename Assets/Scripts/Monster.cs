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

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        target = FindObjectOfType<Character>().transform;
        pathFinder.SetTarget(target);
    }

    private void Update()
    {
        //Move();
    }

    public virtual void Init(string _id)
    {
        id = _id;
        Dictionary<string, object> data = DataManager.monsters.FindDic("ID", _id);
        atk = (int)data["ATK"];
        hp = (int)data["HP"];
        maxHp = hp;
        speed = 2;
        atkSpeed = 1;
        spawnTime = (int)data["SpawnTime"];
    }

    protected void Move()
    {
        transform.position += pathFinder.GetMoveDir(target.position - transform.position) * speed * Time.deltaTime;
    }

    protected virtual void Attack()
    {

    }

    public virtual void TakeDamage(int damage)
    {

    }
}
