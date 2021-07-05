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
        atk.text = $"{DataManager.Localization("atk")} : {CharacterManager.GetCharacter().ATK}";
        hp.text = $"{DataManager.Localization("hp")} : {CharacterManager.GetCharacter().HP}";
    }
}
