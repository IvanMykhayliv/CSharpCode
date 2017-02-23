using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour {
    private int pickUp;
    GameObject playe;

	// Use this for initialization
	void Start () {
        pickUp = Random.Range(0, 2);
        playe = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D hitObj){
		if (hitObj.tag == "Player Bullet") {
			playe.GetComponent<PlayerStateListener>().currentWeapon = pickUp;
			DestroyObject (gameObject);
		} 

		if (hitObj.tag == "Enemy" || hitObj.tag == "GoldEnemy") {
			DestroyObject (gameObject);
		}
	}
}