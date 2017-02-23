using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Ability store script written by, Josh Rand ☺
/// Ability store that appears between waves
/// Allows a user to purchase abilities
/// </summary>
public class AbilityStore : MonoBehaviour {
    public Button GBPointsButton;
    public Button HealDOButton;
    public Button ReverseEnemiesButton;
    public Button NinjaMonkeyEffectButton;
    public Button SleepTightHammerButton;
    public Button SabotageEnemiesButton;
    private AbilityManager abilityManager;  ///<Reference to the scene's ability manager
    private EnemyGenerator enemyGenerator;  ///<Reference to the scene's enemy generator
    public GameObject ludospeed;

    /// <summary>
    /// Gets the abilities and enemy generator items in the hiarchy and updates the store
    /// </summary>
	void Start () {
        abilityManager = GameObject.FindObjectOfType<AbilityManager>();
        enemyGenerator = GameObject.FindObjectOfType<EnemyGenerator>();
        UpdateStore();
	}

    void OnEnable()
    {
        Currency2.CurrencyChanged += UpdateStoreFromEvent;
        Currency2.GbpointsChanged += UpdateStoreFromEvent;
        DeactivateGameUI();
    }

    void OnDisable()
    {
        Currency2.CurrencyChanged -= UpdateStoreFromEvent;
        Currency2.GbpointsChanged -= UpdateStoreFromEvent;
    }

    /// <summary>
    /// Calls the UpdateStore function when the currency amount is changed
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="gbpoints"></param>
    void UpdateStoreFromEvent(int currency, int gbpoints)
    {
        UpdateStore();
    }

    public void ActivateGameUI()
    {

    }

    public void DeactivateGameUI()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("GameUI");
		
		foreach(GameObject uiGamePart in go)
        {
            uiGamePart.SetActive(false);
        }
    }

    /// <summary>
    /// A method for exchanging tendies and good boy points for abilities and good boy points
    /// </summary>
    /// <param name="abilityName"></param>
    public void BuyAbility(string abilityName)
    {
        if (abilityName == "Good Boy Points" && Currency2.currency >= Currency2.gbPointsCost)
        {
            Currency2.AddCurrency(-Currency2.gbPointsCost);
            Currency2.AddGbPoints(1);
            UpdateStore();
        }

        else if (abilityName == "Heal Defendable Object" && Currency2.gbpoints >= 5)
        {
            abilityManager.AddAbility(typeof(HealDOAbility));
            Currency2.AddGbPoints(-5);
            UpdateStore();
        }
        else if (abilityName == "Reverse" && Currency2.currency >= 15)
        {
            abilityManager.AddAbility(typeof(ReverseAbility));
            Currency2.AddCurrency(-15);
            UpdateStore();
        }

        else if (abilityName == "Ninja Monkey Effect" && Currency2.currency >= 40)
        {
            abilityManager.AddAbility(typeof(NinjaMonkeyAbility));
            Currency2.AddCurrency(-40);
            UpdateStore();
        }
        else if (abilityName == "Sleep Tight Hammer" && Currency2.currency >= 30)
        {
            abilityManager.AddAbility(typeof(SleepTightHammers));
            Currency2.AddCurrency(-30);
            UpdateStore();
        }
        else if (abilityName == "Sabotage Enemies" && Currency2.currency >= 30)
        {
            abilityManager.AddAbility(typeof(SabotageAbility));
            Currency2.AddCurrency(-30);
            UpdateStore();
        }
    }

    /// <summary>
    /// checks to see if the various abilities can be bought. if they can, then they are set to be purchasable
    /// </summary>
    void UpdateStore()
    {
        GBPointsButton.interactable = (Currency2.currency >= Currency2.gbPointsCost);
        HealDOButton.interactable = (Currency2.gbpoints >= 5);
        ReverseEnemiesButton.interactable = (Currency2.currency >= 15);
        NinjaMonkeyEffectButton.interactable = (Currency2.currency >= 40);
        SleepTightHammerButton.interactable = (Currency2.currency >= 30);
        SabotageEnemiesButton.interactable = (Currency2.currency >= 30);
    }

    /// <summary>
    /// Destroy method
    /// </summary>
    public void Close()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Resets all monkeys to be able to use the glossner affect after each round.
    /// </summary>
    void ResetMonkeys()
    {
        IEnumerable<TowerScript> allMonkeys = GameObject.FindObjectsOfType<TowerScript>().Where(n => n.TowerType == TowerType.Ted);
        foreach (TowerScript monkey in allMonkeys)
        {
            if (monkey.EffectUsed)
            {
                monkey.EffectUsed = false;
                monkey.UndimSprite();
            }
        }
    }

    /// <summary>
    /// Calls the reset monkeys and enemy generator NextWave function
    /// </summary>
    public void NextWave()
    {
        ResetMonkeys();
        enemyGenerator.NextWave();
        Destroy(gameObject);
        GameObject.FindObjectOfType<AbilityManager>().reactivateButtons();
    }
}
