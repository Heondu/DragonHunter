using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
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
        text.text = DataManager.Localization(id);
        image.sprite = DataManager.LoadImage(id);
    }

    public void Select()
    {
        CardManager.Instance.Select(id);
    }
}
