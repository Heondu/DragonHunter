using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Normal, Critical, Heal, Miss }

public class FloatingDamageManager : MonoBehaviour
{
    private static FloatingDamageManager instance;
    public static FloatingDamageManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<FloatingDamageManager>();
            return instance;
        }
    }
    [SerializeField]
    private GameObject[] damagePrefab;
    [SerializeField]
    private GameObject hpBar;
    private Dictionary<GameObject, List<FloatingDamage>> damageList = new Dictionary<GameObject, List<FloatingDamage>>();
    [SerializeField]
    private Transform holder;

    public void FloatingDamage(GameObject executor, string damage, DamageType damageType)
    {
        GameObject clone = ObjectPooler.Instance.ObjectPool(ObjectPooler.Instance.floatingDamageHolder, damagePrefab[(int)damageType], executor.transform.position, Quaternion.identity, holder);
        clone.GetComponent<FloatingDamage>().Init(damage, executor.transform.position);

        if (damageList.ContainsKey(executor) == false)
        {
            damageList.Add(executor, new List<FloatingDamage>());
        }
        damageList[executor].Add(clone.GetComponent<FloatingDamage>());

        for (int i = damageList[executor].Count - 1; i >= 0; i--)
        {
            if (i > 0 && damageList[executor][i - 1] != null && damageList[executor][i] != null)
            {
                float distance = Mathf.Abs(damageList[executor][i - 1].transform.localPosition.y - damageList[executor][i].transform.localPosition.y);
                float sizeY = damageList[executor][i].GetComponent<RectTransform>().sizeDelta.y;
                float newPos = (sizeY - distance) / damageList[executor].Count;
                newPos = Mathf.Max(0, newPos);
                damageList[executor][i - 1].SetPos(newPos);
            }
        }
    }

    public void RemoveDamage(GameObject executor, FloatingDamage floatingDamage)
    {
        damageList[executor].Remove(floatingDamage);
    }

    public void InitHPBar(ILivingEntity entity, Transform target)
    {
        Instantiate(hpBar, target.transform.position, Quaternion.identity, holder).GetComponent<HPViewer>().Init(entity, target);
    }
}
