using UnityEngine;

public class Explosive : MonoBehaviour
{
    private SkillData skillData;
    [SerializeField]
    private float radius = 3;

    public void Explode(SkillData skillData, float destroyTime)
    {
        this.skillData = skillData;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(skillData.casterTag)) continue;
            ILivingEntity entity = collider.GetComponent<ILivingEntity>();
            if (entity != null)
            {
                Attack(entity);
            }
        }
        Destroy(gameObject, destroyTime);
    }

    protected virtual void Attack(ILivingEntity entity)
    {
        entity.TakeDamage(skillData.damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
