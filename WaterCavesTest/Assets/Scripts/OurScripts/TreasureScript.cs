using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TreasureScript : MonoBehaviour {

    public int treasure;
    public int max;
    public Text counter = null;

    private AudioSource source;
    public AudioClip secondaryTreasureSound;
    private float sound;


    // Use this for initialization
    void Start () {
        treasure = 0;
        max = GameObject.FindGameObjectsWithTag("Treasure").Length;

        if (max == 0){
            if (counter) counter.text = "Side Treasure: ?";
        }

        if (counter) counter.text = "Side Treasure: " + treasure + "/" + max;
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (counter) counter.text = "Side Treasure: " + treasure + "/" + max;

        if (max == 0){
            if (counter) counter.text = "Side Treasure: ?";
        }
        if (treasure > max){
            treasure = max;
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Treasure"){
            treasure++;
            source.PlayOneShot(secondaryTreasureSound);
            Destroy(other.gameObject);
        }
    }
}
