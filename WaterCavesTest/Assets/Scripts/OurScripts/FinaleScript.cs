using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleScript : MonoBehaviour {
    public GameObject Inventory;
    bool tutIsUp;

    public GameObject FinaleTut;
    bool finaleUp;

    // Use this for initialization
    void Start () {
        FinaleTut.SetActive(false);
        finaleUp = false;
        tutIsUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (tutIsUp == true){
            if (Input.GetKeyDown(KeyCode.E)){
                FinaleTut.SetActive(false);
                Inventory.SetActive(true);
                tutIsUp = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (tutIsUp == false){
            if (collision.gameObject.tag == "FinalTut"){
                Inventory.SetActive(false);
                FinaleTut.SetActive(true);
                tutIsUp = true;
                Destroy(collision.gameObject);
            }
        }
    }
 }
