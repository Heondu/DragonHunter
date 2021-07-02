using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;
    private List<string> bossList = new List<string>();
    private bool isBossSpawn = false;
    [SerializeField]
    private float spawnTime = 1;

    private void Start()
    {
        StartCoroutine("SpawnCo");
    }

    private IEnumerator SpawnCo()
    {
        while (true)
        {
            if (!isBossSpawn)
            {
                Spawn();
                yield return new WaitForSeconds(spawnTime);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void Spawn()
    {
        int rand = Random.Range(0, 6);
        List<string> monsters = new List<string>();
        string boss = "";
        for (int i = 0; i < DataManager.monsters.Count; i++)
        {
            if (rand == 0 && CanSpawn(i, "range"))
            {
                monsters.Add(DataManager.monsters[i]["Name"].ToString());
            }
            else if (rand > 0 && CanSpawn(i, "melee"))
            {
                monsters.Add(DataManager.monsters[i]["Name"].ToString());
            }
            else if (CanSpawnBoss(i))
            {
                boss = DataManager.monsters[i]["Name"].ToString();
                bossList.Add(DataManager.monsters[i]["Name"].ToString());
                isBossSpawn = true;
                break;
            }
        }

        if (boss != "")
        {
            Instantiate(Resources.Load("Prefabs/Monster/" + boss));
        }
        else
        {
            Instantiate(Resources.Load("Prefabs/Monster/" + monsters[Random.Range(0, monsters.Count)]));
        }
    }

    private bool CanSpawn(int index, string type)
    {
        if (!DataManager.monsters[index]["Type"].Equals("type")) return false;
        if ((int)DataManager.monsters[index]["SpawnTime"] > manager.GetTime()) return false;
        return true;
    }

    private bool CanSpawnBoss(int index)
    {
        if (!CanSpawn(index, "boss")) return false;
        if (bossList.Contains(DataManager.monsters[index]["Name"].ToString())) return false;
        return true;
    }
}
