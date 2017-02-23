using UnityEngine;
using System.Collections;

public class PickUpGenerator : MonoBehaviour {
	public GameObject pickUp; 

	int drop = 0;
	int location;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(drop == 1000){
			spawnCrates();
			drop = 0;
		}
		drop++;
	}

	void spawnCrates(){
				GameObject newDrop = (GameObject)Instantiate(pickUp);

				location = Random.Range(1, 6);

				if(location == 1){
					newDrop.transform.position = new Vector3 (0f, 2.5f, 0f);
				}
				if(location == 2){
					newDrop.transform.position= new Vector3 (5.5f, 2.5f, 0f);
				}
				if(location == 3){
					newDrop.transform.position = new Vector3 (10f, 2.5f, 0f);
				}
				if(location == 4){
					newDrop.transform.position = new Vector3 (15f, 2.5f, 0f);
				}
				if(location == 5){
					newDrop.transform.position = new Vector3 (-4f, 2.5f, 0f);
				}
				if(location == 6){
					newDrop.transform.position = new Vector3 (-9f, 2.5f, 0f);
				}

				transform.position = new Vector3 (0f, 2.5f, 0f);
	}
}