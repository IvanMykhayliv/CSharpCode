using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.EventSystems;

public class NextLevel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public Image pepe;
    public bool playSound;
    public AudioSource sound;
    public string levelName;

	// Use this for initialization
    public void OnClick()
    {
        //Application.LoadLevel(levelName);
       SceneManager.LoadScene(levelName);
        
        GameController.EnemySpeed = 1.0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (pepe != null)
        {
            pepe.enabled = true;
        }
        if (sound != null && playSound)
        {
            sound.Play();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //if (pepe != null)
        //{
        //    pepe.enabled = false;
        //}
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
