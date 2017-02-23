using UnityEngine;
using System.Collections;

public class EnemyRespawner : MonoBehaviour
{
	public GameObject spawnEnemy = null;
    public GameObject spawnGoldEnemy = null;
	float respawnTime = 0.0f;

    private bool isGoldEnemy = false;
	
	void OnEnable()
	{
		EnemyControllerScript.enemyDied += scheduleRespawn;
        EnemyControllerScript.goldenemyDied += scheduleRespawn;
	}
	
	void OnDisable()
	{
		EnemyControllerScript.enemyDied -= scheduleRespawn;
        EnemyControllerScript.goldenemyDied -= scheduleRespawn;
	}
	
	// Note: Even though we don't need the enemyScore, we still need to accept it because the event passes it
	void scheduleRespawn(int enemyScore)
	{
		// Randomly decide if we will respawn or not
		if(Random.Range(0,10) < 5)
			return;

		respawnTime = Time.time + 4.0f;

        if (Random.Range(0, 100) < 10)
            isGoldEnemy = true;

    } 

    void Update()
	{
		if(respawnTime > 0.0f)
		{
            if(isGoldEnemy)
            {
                if (respawnTime < Time.time)
                {
                    respawnTime = 0.0f;
                    GameObject newEnemy = Instantiate(spawnGoldEnemy) as GameObject;
                    newEnemy.transform.position = transform.position;
                }
            }
			else if(respawnTime < Time.time)
			{
				respawnTime = 0.0f;
				GameObject newEnemy = Instantiate(spawnEnemy) as GameObject;
				newEnemy.transform.position = transform.position;
			}
		}
	}
}
