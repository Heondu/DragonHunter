using UnityEngine;

public class CardManager : MonoBehaviour
{
    private static CardManager instance;
    public static CardManager Instance
    {
        get 
        { 
            if (instance == null) instance = FindObjectOfType<CardManager>();
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    [SerializeField]
    private CardViewer[] cardViewers;
    [SerializeField]
    private GameObject panel;

    private Player player;
    [SerializeField]
    private SpawnManager spawnManager;

    public int Penetrate { get; private set; } = 1;
    public int Repeat { get; private set; } = 1;
    public bool DiagonalAttack { get; private set; } = false;
    public bool BackAttack { get; private set; } = false;
    public bool PoisonExplosion { get; private set; } = false;

    [ContextMenu("Show Card")]
    public void ShowCard()
    {
        Time.timeScale = 0;
        panel.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            int sumOfProb = 0;
            for (int j = 0; j < DataManager.cards.Count; j++)
            {
                sumOfProb += (int)DataManager.cards[j]["Prob"];
            }

            int rand = Random.Range(0, sumOfProb);
            int sum = 0;
            for (int j = 0; j < DataManager.cards.Count; j++)
            {
                sum += (int)DataManager.cards[j]["Prob"];
                if (rand < sum)
                {
                    cardViewers[i].SetCard(DataManager.cards[j]["ID"].ToString());
                    break;
                }
            }
        }
    }

    [ContextMenu("Show Special Card")]
    public void ShowSpecialCard()
    {
        Time.timeScale = 0;
        panel.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            int sumOfProb = 0;
            for (int j = 0; j < DataManager.specialCards.Count; j++)
            {
                sumOfProb += (int)DataManager.specialCards[j]["Prob"];
            }

            int rand = Random.Range(0, sumOfProb);
            int sum = 0;
            for (int j = 0; j < DataManager.specialCards.Count; j++)
            {
                sum += (int)DataManager.specialCards[j]["Prob"];
                if (rand < sum)
                {
                    cardViewers[i].SetCard(DataManager.specialCards[j]["ID"].ToString());
                    break;
                }
            }
        }
    }

    public void Select(string id)
    {
        switch (id)
        {
            case "card001":
                StatusManager.GetStatus("atk").AddModifier(new StatusModifier(10, StatusModType.PercentAdd)); 
                break;
            case "card002":
                StatusManager.GetStatus("atkSpeed").AddModifier(new StatusModifier(10, StatusModType.PercentAdd));
                StatusManager.GetStatus("speed").AddModifier(new StatusModifier(10, StatusModType.PercentAdd));
                break;
            case "card003":
                StatusManager.GetStatus("critChance").AddModifier(new StatusModifier(10, StatusModType.PercentAdd));
                StatusManager.GetStatus("critDamage").AddModifier(new StatusModifier(10, StatusModType.PercentAdd));
                break;
            case "card004":
                player.Restore(20, 1);
                break;
            case "card005":
                player.Restore(40, 1);
                break;
            case "card006":
                Penetrate++;
                break;
            case "card007":
                Repeat++;
                break;
            case "card008":
                DiagonalAttack = true;
                break;
            case "card009":
                BackAttack = true;
                break;
            case "card010":
                spawnManager.CanTrapSpawn = false;
                break;
            case "spcard001":
                player.AddSkill("SoulSword");
                break;
            case "spcard002":
                player.AddSkill("SoulShield");
                break;
            case "spcard003":
                player.AddSkill("Fireball");
                break;
            case "spcard004":
                player.Restore(100, 1);
                break;
            case "spcard005":
                player.AddSkill("Lightning");
                break;
            case "spcard006":
                player.AddSkill("Frozen");
                break;
            case "spcard007":
                player.AddSkill("PenetratingBeam");
                break;
            case "spcard008":
                PoisonExplosion = true;
                break;
            case "spcard009":
                StatusManager.GetStatus("score").AddModifier(new StatusModifier(30, StatusModType.PercentAdd));
                break;
        }

        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
    }
}
