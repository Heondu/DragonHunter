using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    [SerializeField]
    private Image image;
    private ILivingEntity entity;

    public void Init(ILivingEntity _entity)
    {
        entity = _entity;
    }

    private void Update()
    {
        image.fillAmount = entity.GetHP();

        if (entity.GetHP() <= 0) Destroy(gameObject);
    }
}
