using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected string id;
    protected int atk;
    protected Transform player;
    protected bool stopAtPlayerVisible;
    private bool atkCool = false;
    private float cooltime = 2;
    private Timer timer = new Timer();
    [SerializeField] protected GameObject hitEffect;

    public virtual void Init(string _id, Transform _player)
    {
        id = _id;
        Dictionary<string, object> data = DataManager.traps.FindDic("ID", _id);
        atk = (int)data["ATK"] * GameManager.GetDifficulty();
        player = _player;
        stopAtPlayerVisible = true;
        StartCoroutine("Timer", (int)data["Lifetime"]);
    }

    private void Update()
    {
        if (atkCool)
        {
            if (timer.GetTimer(cooltime)) atkCool = false;
        }
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

    private void OnTriggerStay(Collider other)
    {
        if (!atkCool)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<ILivingEntity>().TakeDamage(atk);
                GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, hitEffect, other.transform.position, hitEffect.transform.rotation);
                atkCool = true;
            }
        }
    }
}
