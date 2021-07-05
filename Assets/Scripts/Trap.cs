using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private string id;
    private int atk;
    private Transform player;

    public void Init(string _id, Transform _player)
    {
        id = _id;
        Dictionary<string, object> data = DataManager.traps.FindDic("ID", _id);
        atk = (int)data["ATK"];
        player = _player;
        StartCoroutine("Timer", (int)data["Lifetime"]);
    }

    private IEnumerator Timer(int lifetime)
    {
        float t = 0;
        while (t < lifetime)
        {
            if (Vector3.Distance(player.position, transform.position) > 11)
            {
                t += Time.deltaTime;
            }
            yield return null;
        }

        ObjectPooler.Instance.ObjectInactive(ObjectPooler.Instance.trapHolder, gameObject);
    }
}
