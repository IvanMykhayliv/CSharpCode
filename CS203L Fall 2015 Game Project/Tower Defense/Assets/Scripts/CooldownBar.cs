using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

/// <summary>
/// Cooldown bars, They slowly fill up after using an ability, and the ability is usable again once the bar fills up
/// </summary>

public class CooldownBar : MonoBehaviour {
    public static event Action<int> BarFilled;  ///< Event called when the bar is filled completely
    public Ability Ability
    {
        get
        {
            return ability;
        }

        set
        {
            ability = value;
            AbilityName.text = ability.Name + " (" + ability.Quantity + ")";
            coolDownSeconds = ability.cooldownTime;
        }
    }    ///< The corresponding ability (for getting name and cooldown time
    public int abilityIndex;    ///< Between 0 and 3, the index of the corresponding ability
    public Image Bar;           ///< The slowly-refilling bar
    public Image BackBar;       ///< The empty shell behind the bar
    public Text AbilityName;
    private Vector3 startPos;
    private int coolDownSeconds; ///< The duration in seconds of the ability's cooldown period
    private Ability ability;
	// Use this for initialization
    //The gameObject should initially be set to inactive
	void Start () {
        Bar.enabled = false;
        BackBar.enabled = false;
        AbilityName.enabled = false;
	}

    /// <summary>
    /// Start the cooldown period by calling the CoolDown coroutine
    /// </summary>
    public void StartCoolDown()
    {
        Bar.enabled = true;
        BackBar.enabled = true;
        AbilityName.enabled = true;
        AbilityName.text = ability.Name + " (" + ability.Quantity + ")";
        StartCoroutine(CoolDown());
    }

    /// <summary>
    /// The CoolDown coroutine,
    /// The ability should be usable again when the coroutine finishes
    /// </summary>
    /// <returns></returns>
    IEnumerator CoolDown()
    {
        for (int i = 0; i < coolDownSeconds; i++)
        {
            float scale = (float)i / coolDownSeconds;
            Bar.rectTransform.localScale = new Vector3(1f, scale, 1f);
            yield return new WaitForSeconds(1f);
        }
        Bar.enabled = false;
        BackBar.enabled = false;
        AbilityName.enabled = false;
        if (BarFilled != null)
        {
            BarFilled(abilityIndex);
        }
    }
}
