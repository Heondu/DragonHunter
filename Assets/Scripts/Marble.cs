using UnityEngine;

public enum MarbleType { Blue, Yellow }

public class Marble : MonoBehaviour
{
    [SerializeField]
    private MarbleType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == MarbleType.Blue) CardManager.Instance.ShowCard();
            else if (type == MarbleType.Yellow) CardManager.Instance.ShowSpecialCard();
            Destroy(gameObject);
        }
    }
}
