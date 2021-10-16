using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static Transform holder;
    private static Transform Holder
    {
        get
        {
            if (holder == null) holder = new GameObject("Holder").transform;
            return holder;
        }
    }
    public static string skillHolder = "SkillHolder";
    public static string monsterHolder = "MonsterHolder";
    public static string trapHolder = "TrapHolder";
    public static string itemHolder = "ItemHolder";
    public static string floatingDamageHolder = "FloatingDamageHolder";
    public static string hpBarHolder = "HPBarHolder";
    public static string effectHolder = "EffectHolder";

    public static GameObject ObjectPool(string holderName, GameObject obj)
    {
        return ObjectPool(holderName, obj, Vector3.zero, Quaternion.identity, null);
    }

    public static GameObject ObjectPool(string holderName, GameObject obj, Vector3 position)
    {
        return ObjectPool(holderName, obj, position, Quaternion.identity, null);
    }

    public static GameObject ObjectPool(string holderName, GameObject obj, Quaternion rotation)
    {
        return ObjectPool(holderName, obj, Vector3.zero, rotation, null);
    }

    public static GameObject ObjectPool(string holderName, GameObject obj, Vector3 position, Quaternion rotation)
    {
        return ObjectPool(holderName, obj, position, rotation, null);
    }

    public static GameObject ObjectPool(string holderName, GameObject obj, Quaternion rotation, Transform parent)
    {
        return ObjectPool(holderName, obj, Vector3.zero, rotation, parent);
    }

    public static GameObject ObjectPool(string holderName, GameObject obj, Vector3 position, Quaternion rotation, Transform parent)
    {
        Transform holder = Holder.Find(holderName);
        if (holder == null)
        {
            holder = new GameObject(holderName).transform;
            holder.SetParent(Holder);
        }

        return ObjectPool(holder, obj, position, rotation, parent);
    }

    public static GameObject ObjectPool(Transform holder, GameObject obj, Vector3 position, Quaternion rotation, Transform parent)
    {
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

    public static void ObjectInactive(string holderName, GameObject obj)
    {
        Transform holder = Holder.Find(holderName);
        if (holder == null)
        {
            holder = new GameObject(holderName).transform;
            holder.SetParent(Holder);
        }

        ObjectInactive(holder, obj);
    }

    public static void ObjectInactive(Transform holder, GameObject obj)
    {
        obj.transform.SetParent(holder);
        obj.SetActive(false);
    }

    public static bool CheckForDistance(float distance)
    {
        return distance > 15;
    }
}
