using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Keeps track of the player's purchased abilities and displays the buttons for using abilities
/// </summary>
public class AbilityManager : MonoBehaviour {
    public GameObject Canvas;           ///<For attaching the ability store
    public GameObject AbilityStore;     ///<Ability store prefab
    public Button[] Buttons;            ///<Buttons on the ability HUD
    public CooldownBar[] CooldownBars;  ///<Cooldown bars for after using an ability
    public GameObject[] buttonsToHide;
    public Image White;                 ///<Screen flash for Glossner / Ninja Monkey Effect
    public AudioSource johnCena;        ///<The Time is Now sound clip
    public static IList<Ability> Abilities { get; private set; }    ///<Static list of all abilities the player has
   
                                                                 ///<Marked as static so abilities are kept through multiple scenes
    /// <summary>
    /// Self-explanatory
    /// </summary>
	void Start () {
        UpdateButtons();
	}

    /// <summary>
    /// Enable a button associated with an ability
    /// </summary>
    /// <param name="abilityIndex"></param>
    void EnableAbility(int abilityIndex)
    {
        Buttons[abilityIndex].gameObject.SetActive(true);
    }

    /// <summary>
    /// Allow other non-MonoBehaviour objects (such as abilities) to start a coroutine by calling this function
    /// </summary>
    /// <param name="routine"></param>
    public void StartACoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    /// <summary>
    /// Play The Time is Now sound effect
    /// </summary>
    public void JohnCena()
    {
        johnCena.Play();
    }

    /// <summary>
    /// Static method to initialize the static ability list
    /// </summary>
    static AbilityManager()
    {
        Abilities = new List<Ability>();
        //Temporary, for testing purposes
        Abilities.Add(new NinjaMonkeyAbility { Quantity = 99 });
        Abilities.Add(new HealDOAbility { Quantity = 99 });
        Abilities.Add(new ReverseAbility { Quantity = 99 });
    }

    /// <summary>
    /// Subscribe to the WaveEnd event from the EnemyGenerator
    /// </summary>
    void OnEnable()
    {
        CooldownBar.BarFilled += EnableAbility;
        EnemyGenerator.WaveEnd += EnemyWaveEnd;
    }

    /// <summary>
    /// Unsubscribe
    /// </summary>
    void OnDisable()
    {
        CooldownBar.BarFilled -= EnableAbility;
        EnemyGenerator.WaveEnd -= EnemyWaveEnd;
    }

    /// <summary>
    /// Enable and disable the buttons on the ability HUD as they're used
    /// </summary>
    void UpdateButtons()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (i < Abilities.Count)
            {
                if (Abilities[i].specialAbility)
                {
                    Buttons[i].image.color = Color.green;
                    CooldownBars[i].Bar.color = new Color(0f, 0.5f, 0f, 1f);
                }
                else
                {
                    Buttons[i].image.color = Color.white;
                    CooldownBars[i].Bar.color = Color.blue;
                }
                Buttons[i].enabled = true;
                Buttons[i].gameObject.transform.localPosition = new Vector3(50f + 160f * i, 5f, 0f);
                Buttons[i].GetComponentInChildren<Text>().text = Abilities[i].Name + " (" + Abilities[i].Quantity + ")";

                CooldownBars[i].Ability = Abilities[i];
            }
            else
            {
                Buttons[i].enabled = false;
                Buttons[i].gameObject.transform.localPosition = new Vector3(-200f, -200f, 0f);
            }
        }
    }
	
    /// <summary>
    /// Also self-explanatory
    /// </summary>
	void Update () {
	    
	}

    /// <summary>
    /// Show the ability store when the wave has ended
    /// </summary>
    void EnemyWaveEnd()
    {
        GameObject store = Instantiate(AbilityStore);
        foreach(GameObject button in buttonsToHide)
        {
            button.SetActive(false);
        }
        store.GetComponent<RectTransform>().parent = Canvas.GetComponent<RectTransform>();
        store.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    /// <summary>
    /// Add an ability from the ability store to the ability HUD as they are purchased
    /// </summary>
    /// <param name="AbilityType"></param>
    public void AddAbility(System.Type AbilityType)
    {
        //If the ability type is valid
        if (AbilityType.IsSubclassOf(typeof(Ability)))
        {
            bool AbilityExists = (Abilities.Where(a => a.GetType() == AbilityType).Count() > 0);
            //If the ability is already in the list
            if (AbilityExists)
            {
                Ability ability = Abilities.Where(a => a.GetType() == AbilityType).Select(a => a).Single();
                ability.Quantity++;
            }
            //If it is not
            else {
                Ability newa = System.Activator.CreateInstance(AbilityType) as Ability;
                newa.Quantity = 1;
                Abilities.Add(newa);
            }
            UpdateButtons();
        }
        else
        {
            Debug.Log("Error: Invalid Ability type");
        }
    }

    /// <summary>
    /// Use a specified ability when an ability button is clicked
    /// </summary>
    /// <param name="index"></param>
    public void UseAbility(int index)
    {
        if (EnemyGenerator.waveActive)
        {
            Ability ability = Abilities[index];
            //Check to see if the Ninja Monkey effect ability is available (requires at least one ninja monkey on the field)
            if (ability.GetType() == typeof(NinjaMonkeyAbility))
            {
                bool monkeysAreAvailable = GameObject.FindObjectsOfType<TowerScript>().Where(n => n.TowerType == TowerType.Ted).Where(n => !n.EffectUsed).Count() > 0;
                if (monkeysAreAvailable)
                {
                    GetComponent<AudioSource>().Play();
                    ability.Use();
                    if (ability.Quantity > 1)
                    {
                        Buttons[index].gameObject.SetActive(false);
                        CooldownBars[index].StartCoolDown();
                    }
                    ability.Quantity--;
                }
            }
            else
            {
                GetComponent<AudioSource>().Play();
                ability.Use();
                if (ability.Quantity > 1)
                {
                    Buttons[index].gameObject.SetActive(false);
                    CooldownBars[index].StartCoolDown();
                }
                ability.Quantity--;
            }
            if (ability.Quantity == 0)
            {
                Abilities.RemoveAt(index);
            }
            UpdateButtons();
        }
    }

    public void reactivateButtons()
    {
        foreach (GameObject button in buttonsToHide)
        {
            button.SetActive(true);
        }
    }
}
