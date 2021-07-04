using UnityEngine;

public class Player : MonoBehaviour, ILivingEntity
{
    protected int atk;
    protected int hp;
    protected int lv;
    protected int maxHp;
    protected int exp;

    private void Start()
    {
        StatusManager.Status.atk.RemoveAllModifiersFromSource(CharacterManager.GetCharacter());
        StatusManager.Status.hp.RemoveAllModifiersFromSource(CharacterManager.GetCharacter());
        StatusManager.Status.atk.AddModifier(new StatusModifier(CharacterManager.GetCharacter().ATK, StatusModType.Flat, CharacterManager.GetCharacter()));
        StatusManager.Status.hp.AddModifier(new StatusModifier(CharacterManager.GetCharacter().HP, StatusModType.Flat, CharacterManager.GetCharacter()));
    }

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
