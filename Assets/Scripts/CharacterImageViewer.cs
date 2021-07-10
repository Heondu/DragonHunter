using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterImageViewer : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI atk;
    [SerializeField] private TextMeshProUGUI hp;

    private void Update()
    {
        image.sprite = Resources.Load<GameObject>("Prefabs/Characters/" + CharacterManager.currentID).GetComponent<SpriteRenderer>().sprite;
        atk.text = $"{DataManager.Localization("atk")} : {StatusManager.GetStatus("atk").Value}";
        hp.text = $"{DataManager.Localization("hp")} : {StatusManager.GetStatus("hp").Value}";
    }
}
