using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler instance;
    public static ObjectPooler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ObjectPooler>();
                instance.holder = new GameObject("Holder").transform;
            }
            return instance;
        }
    }

    private Transform holder;
    public string skillHolder = "SkillHolder";
    public string monsterHolder = "MonsterHolder";
    public string trapHolder = "TrapHolder";
    public string itemHolder = "ItemHolder";
    public string floatingDamageHolder = "FloatingDamageHolder";

    public GameObject ObjectPool(string holderName, GameObject obj)
    {
        return ObjectPool(holderName, obj, Vector3.zero, Quaternion.identity, null);
    }

    public GameObject ObjectPool(string holderName, GameObject obj, Vector3 position)
    {
        return ObjectPool(holderName, obj, position, Quaternion.identity, null);
    }

    public GameObject ObjectPool(string holderName, GameObject obj, Quaternion rotation)
    {
        return ObjectPool(holderName, obj, Vector3.zero, rotation, null);
    }

    public GameObject ObjectPool(string holderName, GameObject obj, Vector3 position, Quaternion rotation)
    {
        return ObjectPool(holderName, obj, position, rotation, null);
    }

    public GameObject ObjectPool(string holderName, GameObject obj, Quaternion rotation, Transform parent)
    {
        return ObjectPool(holderName, obj, Vector3.zero, rotation, parent);
    }

    /// <summary>
    /// 오브젝트 풀링 메소드
    /// </summary>
    /// <param name="holder">오브젝트를 찾거나 생성 시 부모로 등록할 오브젝트의 트랜스폼</param>
    /// <param name="obj">찾거나 없을 시 생성할 오브젝트</param>
    /// <returns></returns>
    public GameObject ObjectPool(string holderName, GameObject obj, Vector3 position, Quaternion rotation, Transform parent)
    {
        Transform holder = this.holder.Find(holderName);
        if (holder == null)
        {
            holder = new GameObject(holderName).transform;
            holder.SetParent(this.holder);
        }
        
        string name = obj.name + "(Clone)";

        GameObject clone = null;
        if (holder.childCount != 0)
        {
            for (int i = 0; i < holder.childCount; i++)
            {
                if (holder.GetChild(i).gameObject.activeSelf == false && holder.GetChild(i).gameObject.name == name)
                {
                    clone = holder.GetChild(i).gameObject;
                    clone.transform.SetPositionAndRotation(position, rotation);
                    clone.SetActive(true);
                    return clone;
                }
            }
        }
        if (clone == null)
        {
            clone = Instantiate(obj, position, rotation, holder);
        }

        if (parent != null)
        {
            Vector3 scale = clone.transform.localScale;
            clone.transform.SetParent(parent);
            clone.transform.localScale = scale;
        }

        return clone;
    }

    public void ObjectInactive(string holderName, GameObject obj)
    {
        Transform holder = this.holder.Find(holderName);
        if (holder == null)
        {
            holder = new GameObject(holderName).transform;
            holder.SetParent(this.holder);
        }

        obj.transform.SetParent(holder);
        obj.SetActive(false);
    }
}
