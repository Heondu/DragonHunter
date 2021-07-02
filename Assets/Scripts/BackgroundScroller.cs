using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveDistance;

    private void Update()
    {
        if (target == null) return;
        float x = 0, z = 0;
        Vector3 distance = target.position - transform.position;
        if (distance.x < -moveDistance) x = -moveDistance;
        else if (distance.x > moveDistance )x = moveDistance;
        if (distance.z < -moveDistance) z = -moveDistance;
        else if (distance.z > moveDistance) z = moveDistance;

        transform.position += new Vector3(x * 2, 0, z * 2);
    }
}
