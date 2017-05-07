using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagedScript : MonoBehaviour{
    public Image img;

    public AudioSource source;
    public AudioClip hurtSound;

    // Use this for initialization
    void Start (){
        img.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
            
    }

    public void Damaged(){
        StartCoroutine(Wait());
    }

    void unDamaged(){
        img.gameObject.SetActive(false);
    }

    void GetDamaged(){
        img.gameObject.SetActive(true);
        if (!source.isPlaying) {
            source.PlayOneShot(hurtSound);
        }
    }

    IEnumerator Wait(){
        GetDamaged();
        yield return new WaitForSeconds(1);
        unDamaged();
    }
}