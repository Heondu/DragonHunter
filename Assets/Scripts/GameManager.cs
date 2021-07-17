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
            if (tElapsed > 1200)
            //if (tElapsed > 3)
            {
                SetItem();
                GoToMain();
            }
            tElapsed += Time.deltaTime;
        }
    }

    public float GetTime()
    {
        return tElapsed;
    }

    private void SetItem()
    {
        int rand = Random.Range(0, DataManager.items.Count);
        Dictionary<string, object> data = DataManager.items[rand];
        ItemData itemData = new ItemData(data["ID"].ToString(), Random.Range(0, 6));
        Inventory.AddItem(itemData);
    }

    public void Pause(bool value)
    {
        if (value) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void Mute(bool value)
    {
        if (value)
        {
            SettingsManager.setBGM(0);
            SettingsManager.setSE(0);
        }
        else
        {
            SettingsManager.setBGM(1);
            SettingsManager.setSE(1);
        }
    }

    public void GoToMain()
    {
        StatusManager.Sub(CharacterManager.GetCharacter().list);
        StatusManager.Print();
        LoadingSceneManager.LoadScene("Main");
    }
}
