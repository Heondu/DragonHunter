using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Lifetime))]
public class Trap : MonoBehaviour
{
    private string id;
    private int atk;

    private Lifetime lifetime;

    public void Init(string _id, Transform player)
    {
        id = _id;
        Dictionary<string, object> data = DataManager.traps.FindDic("ID", _id);
        atk = (int)data["ATK"];
        lifetime = GetComponent<Lifetime>();
        lifetime.Init((int)data["Lifetime"], player);
    }
}
