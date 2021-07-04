using UnityEngine;

public class MonsterBoss : Monster
{
    public override void TakeDamage(int damage)
    {
        hp = Mathf.Max(0, hp - damage);

        if (hp == 0)
        {
            FindObjectOfType<SpawnManager>().OnBossDeath();
            Destroy(gameObject);
        }
    }
}
