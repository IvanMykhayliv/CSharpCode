using UnityEngine;
using System.Collections;

public class ReverseAbility : Ability {
    public delegate void ActionHandler();
    public static event ActionHandler ReverseEnemies;
    public ReverseAbility()
    {
        specialAbility = false;
        cooldownTime = 10;
        Name = "Reverse";
    }
    public override void Use()
    {
        if (ReverseEnemies != null)
        {
            ReverseEnemies();
        }
        Debug.Log("Reverse Ability Used");
    }
}
