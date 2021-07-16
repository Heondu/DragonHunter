using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardViewer : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDesc;
    private Button button;

    private string id;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);
    }

    public void SetCard(string _id)
    {
        id = _id;
        image.sprite = DataManager.LoadImage(id);
        textName.text = DataManager.Localization(id);
        textDesc.text = DataManager.Localization(id + "_Desc");
    }

    public void Select()
    {
        CardManager.Instance.Select(id);
    }
}
