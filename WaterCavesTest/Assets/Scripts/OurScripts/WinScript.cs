using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour {
    int goal;
    int needed;
    public Text counter = null;
    Vector3 pos;
    public GameObject EndZone;
    public GameObject Phase2;

    public AudioSource source;
    public AudioClip mainTreasureSound;

    // Use this for initialization
    void Start() {
        goal = 0;

        needed = GameObject.FindGameObjectsWithTag("Goal").Length;

        if (needed == 0) {
            if (counter) counter.text = "Main Treasure: ?";
        }

        if (needed > 0) {
        if (counter) counter.text = "Main Treasure: " + goal + "/" + needed;
        }

        Phase2.SetActive(false);
        EndZone.SetActive(false);
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (needed > 0){
            if (counter) counter.text = "Main Treasure: " + goal + "/" + needed;
        }
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Goal"){
            goal++;
            source.PlayOneShot(mainTreasureSound);
            Destroy(collision.gameObject);
            if (goal == needed){
                //SceneManager.LoadScene("WinScreen");
                pos = GameObject.FindGameObjectWithTag("Starting").transform.position;
                Destroy(GameObject.FindGameObjectWithTag("Starting"));
                EndZone.SetActive(true);
                //Instantiate(EndZone, pos, Quaternion.identity);
                Phase2.SetActive(true);
            }
        }

        if (collision.gameObject.tag == "Finale"){
            SceneManager.LoadScene("WinScreen");
        }

        if (collision.gameObject.tag == "Boat"){
            SceneManager.LoadScene("WinScreen");
        }
    }

    public void Die(){
        SceneManager.LoadScene("LoseScreen");
    }
}