using UnityEngine;
using TMPro;

public class CharacterViewer : MonoBehaviour
{
    [SerializeField]
    private string id;
    [SerializeField]
    private TextMeshProUGUI textStatus;

    private void Update()
    {
        textStatus.text = $"{DataManager.Localization(id)}\n" +
            $"LV : {CharacterManager.List.characters[id].LV}\n" +
            $"ATK : {CharacterManager.List.characters[id].ATK}\n" +
            $"HP : {CharacterManager.List.characters[id].HP}";
    }
}
