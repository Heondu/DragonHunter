using UnityEngine;
using UnityEngine.Rendering;

public class SpriteSetup : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool receiveShadows = true;
    [SerializeField] private ShadowCastingMode shadowCastingMode = ShadowCastingMode.On;

    private void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.receiveShadows = receiveShadows;
        //spriteRenderer.shadowCastingMode = shadowCastingMode;
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Sqrt(Mathf.Pow(transform.localScale.y, 2) * 2), transform.localScale.z);
    }

    public void SetupShadow(Vector3 size, Transform parent)
    {
        GameObject shadow = Resources.Load<GameObject>("Prefabs/Shadow");
        Debug.Assert(shadow, "prefab couldn't be loaded.");
        shadow = Instantiate(shadow, parent);
        shadow.transform.localScale = size;
    }
}
