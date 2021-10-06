using System.Collections.Generic;
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
    }
    [SerializeField] private CardViewer[] cardViewers;
    [SerializeField] private CardViewer[] specialCardViewers;
    [SerializeField] private GameObject panelCard;
    [SerializeField] private GameObject panelSpecialCard;

    private Player player;
    [SerializeField]
    private SpawnManager spawnManager;
    private List<Dictionary<string, object>> spawnableCards;
    private List<Dictionary<string, object>> spawnableSpecialCards;

    public int Penetrate { get; private set; } = 1;
    public int Repeat { get; private set; } = 1;
    public bool DiagonalAttack { get; private set; } = false;
    public bool BackAttack { get; private set; } = false;
    public bool PoisonExplosion { get; private set; } = false;

    public GameObject GetPoisonExplosion => Resources.Load<GameObject>("Prefabs/Skills/Skill_PoisonExplosion");

    [ContextMenu("Show Card")]
    public void ShowCard()
    {
        Time.timeScale = 0;
        panelCard.SetActive(true);

        if (spawnableCards == null)
        {
            spawnableCards = new List<Dictionary<string, object>>();
            spawnableCards = DataManager.cards;
        }

        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        for (int i = 0; i < spawnableCards.Count; i++)
        {
            list.Add(spawnableCards[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            int sumOfProb = 0;
            for (int j = 0; j < list.Count; j++)
            {
                sumOfProb += (int)list[j]["Prob"];
            }

            int rand = Random.Range(0, sumOfProb);
            int sum = 0;
            for (int j = 0; j < list.Count; j++)
            {
                sum += (int)list[j]["Prob"];
                if (rand < sum)
                {
                    cardViewers[i].SetCard(list[j]["ID"].ToString());
                    if (list.Count > 1) list.RemoveAt(j);
                    break;
                }
            }
        }
    }

    [ContextMenu("Show Special Card")]
    public void ShowSpecialCard()
    {
        Time.timeScale = 0;
        panelSpecialCard.SetActive(true);

        if (spawnableSpecialCards == null)
        {
            spawnableSpecialCards = new List<Dictionary<string, object>>();
            spawnableSpecialCards = DataManager.specialCards;
        }

        List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
        for (int i = 0; i < spawnableSpecialCards.Count; i++)
        {
            list.Add(spawnableSpecialCards[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            int sumOfProb = 0;
            for (int j = 0; j < list.Count; j++)
            {
                sumOfProb += (int)list[j]["Prob"];
            }

            int rand = Random.Range(0, sumOfProb);
            int sum = 0;
            for (int j = 0; j < list.Count; j++)
            {
                sum += (int)list[j]["Prob"];
                if (rand < sum)
                {
                    specialCardViewers[i].SetCard(list[j]["ID"].ToString());
                    if (list.Count > 1) list.RemoveAt(j);
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
                player.AddSkill("Skill_SoulSword");
                break;
            case "spcard002":
                player.AddSkill("Skill_SoulShield");
                break;
            case "spcard003":
                player.AddSkill("Skill_Fireball");
                break;
            case "spcard004":
                player.Restore(100, 1);
                break;
            case "spcard005":
                player.AddSkill("Skill_Lightning");
                break;
            case "spcard006":
                player.AddSkill("Skill_Frozen");
                break;
            case "spcard007":
                player.AddSkill("Skill_Beam");
                break;
            case "spcard008":
                PoisonExplosion = true;
                break;
            case "spcard009":
                StatusManager.GetStatus("score").AddModifier(new StatusModifier(30, StatusModType.PercentAdd));
                break;
        }

        if (spawnableCards != null)
        {
            for (int i = 0; i < spawnableCards.Count; i++)
            {
                if (spawnableCards[i]["ID"].ToString() == id)
                {
                    if (spawnableCards[i]["OnlyOnce"].ToString() == "TRUE")
                    {
                        spawnableCards.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        if (spawnableSpecialCards != null)
        {
            for (int i = 0; i < spawnableSpecialCards.Count; i++)
            {
                if (spawnableSpecialCards[i]["ID"].ToString() == id)
                {
                    if (spawnableSpecialCards[i]["OnlyOnce"].ToString() == "TRUE")
                    {
                        spawnableSpecialCards.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        panelCard.SetActive(false);
        panelSpecialCard.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
    }
}
