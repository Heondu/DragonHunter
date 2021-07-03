using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private List<string> bossList = new List<string>();
    public bool IsBossSpawn = false;
    [SerializeField]
    private float spawnTime = 1;
    [SerializeField]
    private float distance = 10;
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<Character>().transform;
        StartCoroutine("SpawnCo");
    }

    private IEnumerator SpawnCo()
    {
        while (true)
        {
            if (!IsBossSpawn)
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
        string id;
        id = GetBoss();
        if (id != "")
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Monsters/" + id);
            Vector3 pos = GetRandomPos(prefab.transform.position);
            prefab = Instantiate(prefab, pos, prefab.transform.rotation);
            prefab.GetComponent<Monster>().Init(id);
            prefab = Resources.Load<GameObject>("Prefabs/Wall/Wall");
            Instantiate(prefab, (player.position - pos) / 2, prefab.transform.rotation);
            return;
        }

        Dictionary<string, int> monsters = GetMonsterList();
        Dictionary<string, int> traps = GetTrapList();
        int sumOfProb = 0;
        foreach (string key in monsters.Keys)
        {
            sumOfProb += monsters[key];
        }
        foreach (string key in traps.Keys)
        {
            sumOfProb += traps[key];
        }

        int rand = Random.Range(0, sumOfProb);
        int sum = 0;
        foreach (string key in monsters.Keys)
        {
            sum += monsters[key];
            if (rand < sum)
            {
                GameObject prefab = Resources.Load<GameObject>("Prefabs/Monsters/" + key);
                prefab = Instantiate(prefab, GetRandomPos(prefab.transform.position), prefab.transform.rotation);
                prefab.GetComponent<Monster>().Init(key);
                return;
            }
        }
        foreach (string key in traps.Keys)
        {
            sum += traps[key];
            if (rand < sum)
            {
                GameObject prefab = Resources.Load<GameObject>("Prefabs/Traps/" + key);
                prefab = Instantiate(prefab, GetRandomPos(prefab.transform.position), prefab.transform.rotation);
                prefab.GetComponent<Trap>().Init(key, player);
                return;
            }
        }
    }

    private Dictionary<string, int> GetMonsterList()
    {
        Dictionary<string, int> monsters = new Dictionary<string, int>();
        for (int i = 0; i < DataManager.monsters.Count; i++)
        {
            if (CanSpawnMonster(i))
            {
                if (!IsBoss(i))
                {
                    monsters.Add(DataManager.monsters[i]["ID"].ToString(), (int)DataManager.monsters[i]["Prob"]);
                }
            }
        }
        return monsters;
    }

    private string GetBoss()
    {
        for (int i = 0; i < DataManager.monsters.Count; i++)
        {
            if (CanSpawnMonster(i))
            {
                if (IsBoss(i) && CanSpawnBoss(i))
                {
                    bossList.Add(DataManager.monsters[i]["ID"].ToString());
                    IsBossSpawn = true;
                    return DataManager.monsters[i]["ID"].ToString();
                }
            }
        }
        return "";
    }

    private Dictionary<string, int> GetTrapList()
    {
        Dictionary<string, int> traps = new Dictionary<string, int>();
        for (int i = 0; i < DataManager.traps.Count; i++)
        {
            if (CanSpawnTrap(i))
            {
                traps.Add(DataManager.traps[i]["ID"].ToString(), (int)DataManager.traps[i]["Prob"]);
            }
        }
        return traps;
    }

    private Vector3 GetRandomPos(Vector3 originPos)
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle) * distance;
        float z = Mathf.Sin(angle) * distance;
        return new Vector3(x, originPos.y, z) + player.position;
    }

    private bool CanSpawnMonster(int i)
    {
        if ((int)DataManager.monsters[i]["SpawnTime"] >= gameManager.GetTime()) return false;
        return true;
    }

    private bool IsBoss(int i)
    {
        if (!DataManager.monsters[i]["Type"].ToString().Equals("boss")) return false;
        return true;
    }

    private bool CanSpawnBoss(int i)
    {
        if (bossList.Contains(DataManager.monsters[i]["ID"].ToString())) return false;
        return true;
    }

    private bool CanSpawnTrap(int i)
    {
        if ((int)DataManager.traps[i]["SpawnTime"] >= gameManager.GetTime()) return false;
        return true;
    }
}
