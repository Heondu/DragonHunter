using UnityEngine;

public class Wreck : Trap
{
    [SerializeField]
    private GameObject[] objs;

    public override void Init(string _id, Transform _player)
    {
        base.Init(_id, _player);

        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].SetActive(false);
        }

        if (GameManager.Instance.GetTime() < 720)
        {
            objs[Random.Range(0, 2)].SetActive(true);
        }
        else
        {
            objs[Random.Range(2, 4)].SetActive(true);
        }
    }
}
