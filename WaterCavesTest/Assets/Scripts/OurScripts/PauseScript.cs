using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    public bool isPaused;

    public GameObject menu;

    // Use this for initialization
    void Start (){
        isPaused = false;
        menu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update (){
        if (isPaused == true){
            menu.SetActive(true);
            Time.timeScale = 0;
        }
        else if (isPaused == false){
            menu.SetActive(false);
            Time.timeScale = 1;
        }
	}
}
