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
    private GameObject wallPrefab;
    private GameObject wall;
    public bool CanTrapSpawn = true;
    [SerializeField]
    private Transform canvas;
    [SerializeField]
    private GameObject bossHP;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        wallPrefab = Resources.Load<GameObject>("Prefabs/Wall/Wall");
        StartCoroutine("SpawnCo");
        StartCoroutine("BossSpawnCo");
    }

    private IEnumerator SpawnCo()
    {
        while (true)
        {
            if (!IsBossSpawn)
            {
                SpawnMonster();
                SpawnTrap();
                yield return new WaitForSeconds(spawnTime);
            }
            else
            {
                yield return null;
            }
        }
    }

    private IEnumerator BossSpawnCo()
    {
        Dictionary<string, int> bosses = GetList("boss", false);
        List<string> keys = new List<string>();

        foreach (string key in bosses.Keys)
        {
            keys.Add(key);
        }
        int i = 0;
        while (i < bosses.Count)
        {
            if (!IsBossSpawn)
            {
                if ((int)DataManager.monsters.Find("ID", keys[i], "SpawnTime") <= gameManager.GetTime())
                {
                    IsBossSpawn = true;
                    SpawnBoss(keys[i]);
                    i++;
                }
            }

            yield return null;
        }
    }

    private void SpawnMonster()
    {
        Dictionary<string, int> monsters = GetList("monster", true);
        int sumOfProb = 0;
        foreach (string key in monsters.Keys)
        {
            sumOfProb += monsters[key];
        }

        int rand = Random.Range(0, sumOfProb);
        int sum = 0;
        foreach (string key in monsters.Keys)
        {
            sum += monsters[key];
            if (rand < sum)
            {
                GameObject prefab = Resources.Load<GameObject>("Prefabs/Monsters/" + key);
                prefab = ObjectPooler.ObjectPool(ObjectPooler.monsterHolder, prefab, GetRandomPos(prefab.transform.position), prefab.transform.rotation);
                prefab.GetComponent<Monster>().Init(key);
                return;
            }
        }

    }

    private void SpawnBoss(string id)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Monsters/" + id);
        Vector3 pos = GetRandomPos(prefab.transform.position);
        prefab = ObjectPooler.ObjectPool(ObjectPooler.monsterHolder, prefab, pos, prefab.transform.rotation);
        prefab.GetComponent<Monster>().Init(id);
        Instantiate(bossHP, canvas).GetComponent<BossHPViewer>().Init(prefab.GetComponent<ILivingEntity>());
        wall = ObjectPooler.ObjectPool(ObjectPooler.monsterHolder, wallPrefab, (player.position + pos) / 2, prefab.transform.rotation);
    }

    private void SpawnTrap()
    {
        Dictionary<string, int> traps = GetList("trap", true);
        int sumOfProb = 0;
        foreach (string key in traps.Keys)
        {
            sumOfProb += traps[key];
        }

        int rand = Random.Range(0, sumOfProb);
        int sum = 0;
        foreach (string key in traps.Keys)
        {
            sum += traps[key];
            if (rand < sum)
            {
                GameObject prefab = Resources.Load<GameObject>("Prefabs/Traps/" + key);
                prefab = ObjectPooler.ObjectPool(ObjectPooler.trapHolder, prefab, GetRandomPos(prefab.transform.position), prefab.transform.rotation);
                prefab.GetComponent<Trap>().Init(key, player);
                return;
            }
        }
    }

    private Dictionary<string, int> GetList(string type, bool canSpawn)
    {
        Dictionary<string, int> list = new Dictionary<string, int>();
        if (type == "monster")
        {
            for (int i = 0; i < DataManager.monsters.Count; i++)
            {
                if (!DataManager.monsters[i]["Type"].ToString().Equals("boss"))
                {
                    if (canSpawn)
                    {
                        if (CanSpawn(i, type))
                        {
                            list.Add(DataManager.monsters[i]["ID"].ToString(), (int)DataManager.monsters[i]["Prob"]);
                        }
                    }
                    else
                    {
                        list.Add(DataManager.monsters[i]["ID"].ToString(), (int)DataManager.monsters[i]["Prob"]);
                    }
                }
            }
        }
        if (type == "boss")
        {
            for (int i = 0; i < DataManager.monsters.Count; i++)
            {
                if (DataManager.monsters[i]["Type"].ToString().Equals(type))
                {
                    list.Add(DataManager.monsters[i]["ID"].ToString(), (int)DataManager.monsters[i]["Prob"]);
                }
            }
        }
        else if (type == "trap")
        {
            for (int i = 0; i < DataManager.traps.Count; i++)
            {
                if (canSpawn)
                {
                    if (CanSpawn(i, type))
                    {
                        list.Add(DataManager.traps[i]["ID"].ToString(), (int)DataManager.traps[i]["Prob"]);
                    }
                }
                else
                {
                    list.Add(DataManager.traps[i]["ID"].ToString(), (int)DataManager.traps[i]["Prob"]);
                }
            }
        }
        return list;
    }

    private Vector3 GetRandomPos(Vector3 originPos)
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle) * distance;
        float z = Mathf.Sin(angle) * distance;
        return new Vector3(x, originPos.y, z) + player.position;
    }

    private bool CanSpawn(int i, string type)
    {
        if (type == "monster")
        {
            if ((int)DataManager.monsters[i]["SpawnTime"] <= gameManager.GetTime()) return true;
            else return false;
        }
        else if (type == "trap")
        {
            if ((int)DataManager.traps[i]["SpawnTime"] <= gameManager.GetTime()) return true;
            else return false;
        }
        return false;
    }

    public void OnBossDeath(string id)
    {
        wall.SetActive(false);
        IsBossSpawn = false;
        if (id == "mon020") GameManager.Instance.GoToMain();
    }
}
