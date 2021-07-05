using UnityEngine;

public class MonsterBoss : Monster
{
    protected override void OnDeath()
    {
        base.OnDeath();
        FindObjectOfType<SpawnManager>().OnBossDeath();
    }
}
