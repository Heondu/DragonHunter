using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected string id;
    protected int atk;
    protected Transform player;
    protected bool stopAtPlayerVisible;

    public virtual void Init(string _id, Transform _player)
    {
        id = _id;
        Dictionary<string, object> data = DataManager.traps.FindDic("ID", _id);
        atk = (int)data["ATK"];
        player = _player;
        stopAtPlayerVisible = true;
        StartCoroutine("Timer", (int)data["Lifetime"]);
    }

    protected IEnumerator Timer(int lifetime)
    {
        float t = 0;
        while (t < lifetime)
        {
            if (stopAtPlayerVisible)
            {
                if (Vector3.Distance(player.position, transform.position) > 11)
                {
                    t += Time.deltaTime;
                }
            }
            else
            {
                t += Time.deltaTime;
            }
            yield return null;
        }

        ObjectPooler.ObjectInactive(ObjectPooler.trapHolder, gameObject);
    }
}
