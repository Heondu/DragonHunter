using UnityEngine;
using UnityEngine.UI;

public class HPViewer : MonoBehaviour
{
    private ILivingEntity entity;
    [SerializeField]
    private Image image;
    private Transform target;

    public void Init(ILivingEntity _entity, Transform _target)
    { 
        entity = _entity;
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        image.fillAmount = entity.GetHP();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = new Vector3(target.position.x, target.position.y + 0.75f, target.position.z);
    }
}
