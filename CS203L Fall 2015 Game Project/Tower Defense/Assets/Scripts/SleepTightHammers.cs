using UnityEngine;
using System.Collections;

public class SleepTightHammers : Ability {

    public float timer = 3;
    bool keypressed;

    public SleepTightHammers()
    {
        specialAbility = false;
        cooldownTime = 10;
        Name = "Hammer";
    }

    public override void Use()
    {
        AbilityManager am = GameObject.FindObjectOfType<AbilityManager>();
        am.StartACoroutine(Run());
    }
	
	// Update is called once per frame
	IEnumerator Run() {
        float initEnemySpeed = GameController.EnemySpeed;
        GameController.EnemySpeed = 0;
        yield return new WaitForSeconds(timer);
        GameController.EnemySpeed = initEnemySpeed;
	}
}
