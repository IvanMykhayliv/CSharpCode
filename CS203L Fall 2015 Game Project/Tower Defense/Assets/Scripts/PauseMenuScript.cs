using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour 
{
	public GameObject target;
    //public GameObject target2;
    //public GameObject target3;
    //public GameObject target4;
    public KeyCode pauseKey = KeyCode.P;
	public bool isActive = false;
	//public string userInput = "";
	//public string enter = " \n";
    //public Canvas theCanvas;
    
    

void Update()  
{
	if(Input.GetKeyUp(pauseKey))
	{
		togglePause();
	}
}

public void togglePause()
{
	if(!isActive)
	{	
		isActive = true;
		Time.timeScale = 0.0f;
        target.SetActive(true);
        /*target2.SetActive(true);
        target3.SetActive(true);
        target4.SetActive(true);*/
	}

	else if(isActive)
	{		
        isActive = false;
		Time.timeScale = 1.0f;
        target.SetActive(false);
        /*target2.SetActive(true);
        target3.SetActive(true);
        target4.SetActive(true);*/
	}
}

}
