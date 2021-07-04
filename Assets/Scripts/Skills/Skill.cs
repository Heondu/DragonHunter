using UnityEngine;

public class Skill : MonoBehaviour
{
    public virtual bool Attack(string casterTag, int damage)
    {
        return false;
    }

    public Transform FindTarget()
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
