using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ILivingEntity
{
    private int damage;
    private int hp;
    private int lv;
    private int maxHp;
    private int exp;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, 0, z) * StatusManager.Status.speed.Value * Time.deltaTime;
    }

    public virtual void Attack()
    {

    }

    public void TakeDamage(int damage)
    {

    }
}
