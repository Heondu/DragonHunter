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
        StatusManager.Sum(CharacterManager.GetCharacter().list);
        StatusManager.Print();
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
            //if (tElapsed > 1200)
            if (tElapsed > 3)
            {
                StatusManager.Sub(CharacterManager.GetCharacter().list);
                StatusManager.Print();
                int rand = Random.Range(0, DataManager.items.Count);
                Dictionary<string, object> data = DataManager.items[rand];
                ItemData itemData = new ItemData(data["ID"].ToString(), Random.Range(0, 6));
                Inventory.AddItem(itemData);
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
