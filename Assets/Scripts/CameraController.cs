using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float y, z;

    private void Start()
    {
        y = transform.position.y;
        z = transform.position.z;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(target.position.x, y, target.position.z + z);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
