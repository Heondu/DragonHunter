using System.Collections;
using UnityEngine;
using TMPro;

public class FloatingDamage : MonoBehaviour
{
    private float moveSpeed = 1f;
    private float fadeOutTime = 1f;
    private float destroyTime = 2f;
    private float distance = 50f;
    private TextMeshProUGUI damageText;
    private Color alpha = Color.black;
    private Vector3 originPos;
    private Vector3 offset = Vector3.up;

    public void Init(string damage, Vector3 originPos)
    {
        damageText = GetComponent<TextMeshProUGUI>();
        StartCoroutine("InactiveTimer", destroyTime);

        this.originPos = originPos;
        damageText.text = damage;
        //offset.x += Random.Range(-0.5f, 0.5f);
        StartCoroutine("FadeOut");
    }

    private void Update()
    {
        offset += Vector3.forward * moveSpeed * Time.deltaTime;
        Vector3 newPos = originPos + offset;
        transform.position = newPos;
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(destroyTime - fadeOutTime);

        float percent = 0;
        while (percent < 1)
        {
            alpha.a = Mathf.Lerp(1, 0, percent);
            damageText.color = alpha;

            percent += Time.deltaTime / fadeOutTime;
            yield return null;
        }
    }

    public void SetPos(float moveValue)
    {
        offset += Vector3.forward * (moveValue / distance);
    }

    private IEnumerator InactiveTimer(float t)
    {
        yield return new WaitForSeconds(t);

        ObjectPooler.ObjectInactive(ObjectPooler.floatingDamageHolder, gameObject);
    }
}
