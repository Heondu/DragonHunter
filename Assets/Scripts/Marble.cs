using UnityEngine;

public enum MarbleType { Blue, Yellow }

public class Marble : MonoBehaviour
{
    [SerializeField]
    private MarbleType type;
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == MarbleType.Blue) CardManager.Instance.ShowCard();
            else if (type == MarbleType.Yellow) CardManager.Instance.ShowSpecialCard();
            ObjectPooler.ObjectInactive(ObjectPooler.itemHolder, gameObject);
        }
    }

    private void Update()
    {
        if (ObjectPooler.CheckForDistance(Vector3.Distance(player.transform.position, transform.position)))
        {
            ObjectPooler.ObjectInactive(ObjectPooler.itemHolder, gameObject);
        }
    }
}
