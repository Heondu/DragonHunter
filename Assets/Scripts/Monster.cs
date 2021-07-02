using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, ILivingEntity
{
    private int atk;
    private int hp;
    private int maxHp;
    private int speed;
    private int atkSpeed;
    private int spawnTime;

    public void Move()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {

    }
}
