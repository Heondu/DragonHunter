using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum QuestState { Not, Clear, Recieve }

public class Quest : MonoBehaviour
{
    public string ID;
    [SerializeField]
    private Image imageReward;
    [SerializeField]
    private Sprite[] rewardIcons;
    [SerializeField]
    private TextMeshProUGUI textDesc;
    [SerializeField]
    private TextMeshProUGUI textReward;
    [SerializeField]
    private Image imageClear;
    [SerializeField]
    private Sprite[] clearIcons;
    [SerializeField]
    private Button button;
    private Dictionary<string, object> data;
    public QuestState questState = QuestState.Not;

    public void Init(string id, QuestState state)
    {
        ID = id;
        questState = state;
        data = DataManager.quests.FindDic("ID", ID);
        imageReward.sprite = data["Type"].ToString() == "Gold" ? rewardIcons[0] : rewardIcons[1];
        textReward.text = data["Amount"].ToString();
        textDesc.text = DataManager.Localization(ID);
        button.onClick.AddListener(RewardRecieve);
    }

    private void Update()
    {
        imageClear.sprite = clearIcons[(int)questState];
    }

    private void RewardRecieve()
    {
        if (questState == QuestState.Clear)
        {
            questState = QuestState.Recieve;
            QuestManager.Save();
            if (data["Type"].ToString() == "Gold") PlayerData.Gold.Set((int)data["Amount"], ResourcesModType.Add);
            else if (data["Type"].ToString() == "Diamond") PlayerData.Diamond.Set((int)data["Amount"], ResourcesModType.Add);
        }
    }

    public void Clear()
    {
        if (questState == QuestState.Not)
        {
            questState = QuestState.Clear;
            QuestManager.Save();
        }
    }
}
