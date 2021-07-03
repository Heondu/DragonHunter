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
                clone.GetComponent<Projectile>().Init(target.position.normalized, "Player", (int)StatusManager.Status.atk.Value, 5);
            }
        }
    }

    private Transform FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6);
        Transform target = null;
        float distance = 0;
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<ILivingEntity>() == null) continue;
            if (collider.CompareTag("Player")) continue;

            if (target == null)
            {
                target = collider.transform;
                distance = Vector3.Distance(target.position, transform.position);
            }
            else
            {
                float newDistance = Vector3.Distance(target.position, transform.position);
                if (distance > newDistance)
                {
                    target = collider.transform;
                    distance = newDistance;
                }
            }
        }
        return target;
    }
}
