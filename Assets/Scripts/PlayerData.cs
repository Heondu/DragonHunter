using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static int gold = 0;
    public static int diamond = 0;

    [ContextMenu("Add Gold")]
    public void AddGold()
    {
        gold += 10000;
    }
}
