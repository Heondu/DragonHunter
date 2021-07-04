using System.Collections;
using UnityEngine;

public class SwordAura : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;

    private void Start()
    {
        StartCoroutine("Attack");
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(StatusManager.Status.atkSpeed.Value);

            Transform target = FindTarget();
            if (target != null)
            {
                GameObject clone = Instantiate(projectile, transform.position, projectile.transform.rotation);
                ProjectileData data = new ProjectileData();
                data.dir = (target.position - transform.position).normalized;
                data.casterTag = "Player";
                data.damage = (int)StatusManager.Status.atk.Value;
                data.speed = 5;
                clone.GetComponent<Projectile>().Init(data);
            }
        }
    }

    private Transform FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6);
        Transform target = null;
        float distance = float.MaxValue;
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<ILivingEntity>() == null) continue;
            if (collider.CompareTag("Player")) continue;

            float newDistance = Vector3.Distance(collider.transform.position, transform.position);
            if (distance > newDistance)
            {
                target = collider.transform;
                distance = newDistance;
            }
        }
        return target;
    }
}
