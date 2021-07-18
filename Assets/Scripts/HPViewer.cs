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
        if (entity.GetHP() <= 0 || target.gameObject.activeSelf == false)
        {
            gameObject.SetActive(false);
            return;
        }

        image.fillAmount = entity.GetHP();
        transform.position = Camera.main.WorldToScreenPoint(target.position + new Vector3(0, 0.75f, 0));
    }
}
