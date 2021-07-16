using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatusViewer : MonoBehaviour
{
    [SerializeField]
    private string id;
    [SerializeField]
    private TextMeshProUGUI textStatus;
    [SerializeField]
    private Image image;

    private void Update()
    {
        image.sprite = Resources.Load<GameObject>("Prefabs/Characters/" + id).GetComponent<SpriteRenderer>().sprite;
        textStatus.text = $"{DataManager.Localization(id)} LV.{CharacterManager.List.characters[id].LV}\n" +
            $"{DataManager.Localization("atk")} : {(int)CharacterManager.List.characters[id].list.status["atk"].Value}\n" +
            $"{DataManager.Localization("hp")} : {(int)CharacterManager.List.characters[id].list.status["hp"].Value}";
    }
}
