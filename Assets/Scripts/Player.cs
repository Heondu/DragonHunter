using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    protected Status atk;
    protected Status hp;
    protected int lv;
    protected int maxHp;
    protected int exp;

    private Animator animator;
    private SpriteRenderer sr;

    private void Start()
    {
        atk = new Status("atk", 1, CharacterManager.GetCharacter().ATK);
        hp = new Status("hp", 1, CharacterManager.GetCharacter().HP);
        atk.AddModifier(new StatusModifier(StatusManager.GetStatus("atk").Value, StatusModType.Flat, this));
        hp.AddModifier(new StatusModifier(StatusManager.GetStatus("hp").Value, StatusModType.Flat, this));

        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine("Attack");
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(x, 0, z).normalized * StatusManager.List.status["speed"].Value * Time.deltaTime;
        if (x != 0 || z != 0)
        {
            animator.SetBool("IsMove", true);
            sr.flipX = x > 0;
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds((float)StatusManager.GetStatus("atkSpeed").Value);

            Skill[] skills = GetComponentsInChildren<Skill>();
            for (int i = 0; i < skills.Length; i++)
            {
                if (skills[i].Attack(gameObject.tag, (int)atk.Value))
                {
                    animator.SetTrigger("Attack");
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {

    }
}
