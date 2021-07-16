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
        atk.text = $"{DataManager.Localization("atk")} : {(int)CharacterManager.GetCharacter().list.status["atk"].Value}";
        hp.text = $"{DataManager.Localization("hp")} : {(int)CharacterManager.GetCharacter().list.status["hp"].Value}";
    }
}
