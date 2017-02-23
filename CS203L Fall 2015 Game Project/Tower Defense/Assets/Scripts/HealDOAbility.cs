using UnityEngine;
using System.Collections;

/// <summary>
/// Special ability that heals 1/3 of the defendable object's health,
/// Extends Ability class
/// </summary>
public class HealDOAbility : Ability {

    public HealDOAbility()
    {
        specialAbility = true;
        cooldownTime = 20;
        Name = "Heal DO";
    }

    public override void Use()
    {
        DefendableObject dObject = GameObject.FindObjectOfType<DefendableObject>();
        dObject.PartialHeal();
    }
}
