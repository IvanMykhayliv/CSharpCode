using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour {
    public GameObject Inventory;
    bool tutIsUp;

    public GameObject [] Cleanup;

    public GameObject StartTut;
    public GameObject WallTut;
    public GameObject EnemyTut;
    public GameObject HazardTut;
    public GameObject StationsTut;
    public GameObject EndTut;

    bool startUp;
    bool wallUp;
    bool enemyUp;
    bool hazardUp;
    bool stationsUp;
    bool endUp;


    // Use this for initialization
    void Start () {
        StartTut.SetActive(false);
        WallTut.SetActive(false);
        EnemyTut.SetActive(false);
        HazardTut.SetActive(false);
        StationsTut.SetActive(false);
        EndTut.SetActive(false);
        tutIsUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (tutIsUp == true){
            if (Input.GetKeyDown(KeyCode.E)) {
                Inventory.SetActive(true);

                StartTut.SetActive(false);
                WallTut.SetActive(false);
                EnemyTut.SetActive(false);
                HazardTut.SetActive(false);
                StationsTut.SetActive(false);
                EndTut.SetActive(false);

                tutIsUp = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (tutIsUp == false){
            if (collision.gameObject.tag == "StartTut") {
                Inventory.SetActive(false);
                StartTut.SetActive(true);
                tutIsUp = true;
                Destroy(collision.gameObject);
            }
        }

        if (tutIsUp == false){
            if (collision.gameObject.tag == "WallTut"){
                Inventory.SetActive(false);
                WallTut.SetActive(true);
                tutIsUp = true;
                Destroy(collision.gameObject);
            }
        }

        if (tutIsUp == false){
            if (collision.gameObject.tag == "EnemyTut"){
                Inventory.SetActive(false);
                EnemyTut.SetActive(true);
                tutIsUp = true;
                Destroy(collision.gameObject);
            }
        }

        if (tutIsUp == false){
            if (collision.gameObject.tag == "HazardTut"){
                Inventory.SetActive(false);
                HazardTut.SetActive(true);
                tutIsUp = true;
                Destroy(collision.gameObject);
            }
        }

        if (tutIsUp == false){
            if (collision.gameObject.tag == "StationsTut"){
                Inventory.SetActive(false);
                StationsTut.SetActive(true);
                tutIsUp = true;
                Destroy(collision.gameObject);
            }
        }

        if (tutIsUp == false){
            if (collision.gameObject.tag == "EndTut"){
                Inventory.SetActive(false);
                EndTut.SetActive(true);
                tutIsUp = true;
                Destroy(collision.gameObject);
            }
        }
    }
}