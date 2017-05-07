using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {
    public AudioSource source;
    public AudioClip MenuMusic;
    public AudioClip GameMusic;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevelName == "MainMenu"){
            if (source.clip != MenuMusic){
                source.clip = MenuMusic;
                source.Play();
            }
            if (!source.isPlaying){
                source.Play();
            }
        }
        if (Application.loadedLevelName == "CreditScene"){
            if (source.clip != MenuMusic){
                source.clip = MenuMusic;
                source.Play();
            }
            if (!source.isPlaying){
                source.Play();
            }
        }
        if (Application.loadedLevelName == "LevelSelect"){
            if (source.clip != MenuMusic){
                source.clip = MenuMusic;
                source.Play();
            }
            if (!source.isPlaying){
                source.Play();
            }
        }
        if (Application.loadedLevelName == "Level1"){
            if (source.clip != GameMusic){
                source.clip = GameMusic;
                source.Play();
            }
            if (!source.isPlaying){
                source.Play();
            }
        }
        if (Application.loadedLevelName == "Level2"){
            if (source.clip != GameMusic){
                source.clip = GameMusic;
                source.Play();
            }
            if (!source.isPlaying){
                source.Play();
            }
        }
        if (Application.loadedLevelName == "Level3"){
            if (source.clip != GameMusic){
                source.clip = GameMusic;
                source.Play();
            }
            if (!source.isPlaying){
                source.Play();
            }
        }
        if (Application.loadedLevelName == "WinScreen"){
            source.Stop();
        }
        if (Application.loadedLevelName == "LoseScreen"){
            source.Stop();
        }
    }


}