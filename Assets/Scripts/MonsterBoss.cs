using UnityEngine;

public class MonsterBoss : Monster
{

    protected override void Attack()
    {

    }

    public override void TakeDamage(int damage)
    {
        if (hp <= 0) FindObjectOfType<SpawnManager>().IsBossSpawn = false;
    }
}
