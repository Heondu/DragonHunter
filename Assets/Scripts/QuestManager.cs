using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;
    [SerializeField]
    private Transform content;
    private static Dictionary<string, Quest> questList;
    private static QuestManager instance;
    public static QuestManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<QuestManager>();
            return instance;
        }
    }

    private void Awake()
    {
        Load();
    }

    [System.Serializable]
    public class QuestData
    {
        public string ID;
        public QuestState State;

        public QuestData(string id, QuestState state)
        {
            ID = id;
            State = state;
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public string date;
        public List<QuestData> list = new List<QuestData>();


        public SaveData(Dictionary<string, Quest> _list)
        {
            date = System.DateTime.Now.ToString("yyyy-MM-dd");
            foreach (string key in _list.Keys)
            {
                list.Add(new QuestData(_list[key].ID, _list[key].questState));
            }
        }
    }

    public static void Save()
    {
        SaveData saveData = new SaveData(questList);
        SaveManager.SaveToJson(saveData, SaveDataManager.saveFile[SaveFile.Quest]);
    }

    public static void Load()
    {
        SaveData saveData = SaveManager.LoadFromJson<SaveData>(SaveDataManager.saveFile[SaveFile.Quest]);
        questList = new Dictionary<string, Quest>();
        for (int i = 0; i < DataManager.quests.Count; i++)
        {
            string id = DataManager.quests[i]["ID"].ToString();
            Quest quest = Instantiate(Instance.questPrefab, Instance.content).GetComponent<Quest>();
            questList.Add(id, quest);
            questList[id].Init(id, QuestState.Not);
        }
        if (saveData != null && System.DateTime.Now.ToString("yyyy-MM-dd") == saveData.date)
        {
            for (int i = 0; i < saveData.list.Count; i++)
            {
                questList[saveData.list[i].ID].Init(saveData.list[i].ID, saveData.list[i].State);
            }
        }
    }

    private void Update()
    {
        if (GameManager.playCount > 0) questList["quest001"].Clear();
        if (GameManager.playTime >= 300) questList["quest002"].Clear();
        if (GameManager.playTime >= 600) questList["quest003"].Clear();
    }
}
