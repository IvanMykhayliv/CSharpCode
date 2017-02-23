using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {

	private int colliderCounter = 0;

	public EnemyControllerScript enemyObject = null;

	void OnTriggerEnter2D(Collider2D col){
		gameObject.GetComponentInParent<EnemyControllerScript>().switchDirections();
	}

	void OnTriggerStay2D(Collider2D col){
		if(colliderCounter == 0){
			gameObject.GetComponentInParent<EnemyControllerScript>().switchDirections();
			colliderCounter++;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		colliderCounter = 0;
	}
}
