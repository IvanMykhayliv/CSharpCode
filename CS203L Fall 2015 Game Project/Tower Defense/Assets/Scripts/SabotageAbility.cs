using UnityEngine;
using System.Collections;

public class SabotageAbility : Ability {
    public delegate void ActionHandler();
    public static event ActionHandler SabotageEnemies;
    GameObject[] CurrentEnemy;

    public SabotageAbility()
    {
        specialAbility = false;
        cooldownTime = 10;
        Name = "Sabotage";
    }

    public override void Use(){
            CurrentEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in CurrentEnemy){
                enemy.GetComponent<EnemyController>().Sabotage();
            }
        }
}
