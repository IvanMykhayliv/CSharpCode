using UnityEngine;
using System;

public class HealingAbility : Ability
{
    private DefendableObject dO;
    private int healingValue = 100;
    
    public HealingAbility()
	{
        cooldownTime = 5;
        Name = "Pills Here";
	}

    public override void Use(){
        dO = GameObject.FindObjectOfType<DefendableObject>();

        //dO.heal(healingValue);
    }
}
