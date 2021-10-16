using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    [SerializeField]
    private Image image;
    private ILivingEntity entity;
    private float width;

    private void Start()
    {
        width = image.rectTransform.rect.width;
    }

    public void Init(ILivingEntity _entity)
    {
        entity = _entity;
    }

    private void Update()
    {
        //image.fillAmount = entity.GetHP();
        image.rectTransform.sizeDelta = new Vector2(width * entity.GetHP(), image.rectTransform.rect.height);

        if (entity.GetHP() <= 0) Destroy(gameObject);
    }
}
