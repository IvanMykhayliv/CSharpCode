using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReloadScript : MonoBehaviour {

    public float lastLevel;
    public GameObject button;

    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == "Level1"){
            lastLevel = 4;
        }
        if (Application.loadedLevelName == "Level2"){
            lastLevel = 5;
        }
        if (Application.loadedLevelName == "Level3"){
            lastLevel = 6;
        }
        Setup();
    }

    public void Setup(){
       button = GameObject.FindGameObjectWithTag("SButton");
       button.GetComponent<MainMenuButtons>().buttonControl = lastLevel;
    }
}
