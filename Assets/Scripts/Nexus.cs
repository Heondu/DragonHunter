using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : Trap
{
    [SerializeField] private AudioClip clip;

    private bool canAttack = true;

    public override void Init(string _id, Transform _player)
    {
        id = _id;
        Dictionary<string, object> data = DataManager.traps.FindDic("ID", _id);
        atk = (int)data["ATK"];
        player = _player;
        stopAtPlayerVisible = false;
        StartCoroutine("Timer", (int)data["Lifetime"]);
        StartCoroutine("Growing", (int)data["Lifetime"]);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canAttack)
            {
                other.GetComponent<ILivingEntity>().TakeDamage(atk);
                GameObject clone = ObjectPooler.ObjectPool(ObjectPooler.skillHolder, hitEffect, other.transform.position, hitEffect.transform.rotation);
                SoundManager.Instance.PlaySFX(clip);
                canAttack = false;
                StartCoroutine("Cooltime", 1);
            }
        }
    }

    private IEnumerator Cooltime(float t)
    {
        yield return new WaitForSeconds(t);

        canAttack = true;
    }

    private IEnumerator Growing(float lifetime)
    {
        Vector3 originScale = transform.localScale;
        float percent = 0;
        float current = 0;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / lifetime;
            transform.localScale = Vector3.Lerp(originScale, originScale * 3, percent);

            yield return null;
        }
    }
}
