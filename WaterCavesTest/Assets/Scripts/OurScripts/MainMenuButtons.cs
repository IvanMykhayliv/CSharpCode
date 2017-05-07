using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

    public float buttonControl;
    static private bool _cvis = false;

    public AudioSource source;

    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        SetMouseCursorVisibility(true);
    }

    public void SetMouseCursorVisibility(bool on){
        if (on != _cvis){
            _cvis = on;
            if (_cvis) { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }
            else { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
        }
    }

    // Update is called once per frame
    void Update () {
        Cursor.visible = true;
        SetMouseCursorVisibility(true);
    }

    public void OnClick(){
        if (buttonControl == 0) {
            SceneManager.LoadScene("LevelSelect");
        }

        if (buttonControl == 1){
            SceneManager.LoadScene("CreditScene");
        }

        if (buttonControl == 2){
            Application.Quit();
        }

        if (buttonControl == 3){
            SceneManager.LoadScene("MainMenu");
        }
        if (buttonControl == 4){
            source.Play();
            Wait();
            SceneManager.LoadScene("Level1");
        }
        if (buttonControl == 5){
            source.Play();
            Wait();
            SceneManager.LoadScene("Level2");
        }
        if (buttonControl == 6){
            source.Play();
            Wait();
            SceneManager.LoadScene("Level3");
        }
        if (buttonControl == 7){
            source.Play();
            Wait();
            SceneManager.LoadScene("Boss Level");
        }
    }

    IEnumerator Wait(){
        yield return new WaitForSeconds(6);
    }
}
