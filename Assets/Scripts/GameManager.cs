using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int difficulty;
    private int killCount;
    private float tElapsed = 0;
    private int isClear;
    private int score;

    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private BackgroundScroller backgroundScroller;
    [SerializeField] private CardManager cardManager;

    private void Start()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Characters/" + CharacterManager.currentID);
        prefab = Instantiate(prefab, Vector3.zero, prefab.transform.rotation);
        cameraController.SetTarget(prefab.transform);
        backgroundScroller.SetTarget(prefab.transform);
        cardManager.SetPlayer(prefab.GetComponent<Player>());
    }

    private void Update()
    {
        if (SpawnManager.IsBossSpawn == false)
        {
            if (tElapsed > 3)
            {
                int rand = Random.Range(0, DataManager.items.Count);
                Dictionary<string, object> data = DataManager.items[rand];
                ItemData itemDate = new ItemData(data["ID"].ToString(), Random.Range(0, 6));
                Inventory.AddItem(itemDate);
                LoadingSceneManager.LoadScene("Main");
            }
            tElapsed += Time.deltaTime;
        }
    }

    public float GetTime()
    {
        return tElapsed;
    }
}
