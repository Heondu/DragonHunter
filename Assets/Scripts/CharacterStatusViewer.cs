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
    private Image imageCharacter;
    [SerializeField]
    private GameObject imageLock;
    [SerializeField]
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Unlock);
    }

    private void OnEnable()
    {
        imageLock.SetActive(CharacterManager.List.characters[id].IsLock);
    }

    private void Unlock()
    {
        CharacterManager.List.characters[id].Unlock();
        CharacterManager.Save();
        imageLock.SetActive(CharacterManager.List.characters[id].IsLock);
    }

    private void Update()
    {
        imageCharacter.sprite = Resources.Load<GameObject>("Prefabs/Characters/" + id).GetComponent<SpriteRenderer>().sprite;
        textStatus.text = $"{DataManager.Localization(id)} LV.{CharacterManager.List.characters[id].LV}\n" +
            $"{DataManager.Localization("atk")} : {(int)CharacterManager.List.characters[id].list.status["atk"].Value}\n" +
            $"{DataManager.Localization("hp")} : {(int)CharacterManager.List.characters[id].list.status["hp"].Value}";
    }
}
