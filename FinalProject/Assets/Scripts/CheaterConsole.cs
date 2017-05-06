using UnityEngine;
using System.Collections;
using System;

public class CheaterConsole : MonoBehaviour 
{  
	public GameObject target;
	public KeyCode consoleKey = KeyCode.C;
	public string stringToEdit = "Enter your cheat code here";
	private string godCheat = "godhandisthebestps2gameofalltime";
	public bool isActive = false;
	public string userInput = "";
	public string enter = " \n";
    
void OnGUI()     
{
	if(isActive)
		stringToEdit = GUI.TextField(new Rect(Screen.width - 200, Screen.height - 20, 200, 20), stringToEdit);
		
		userInput = stringToEdit;
		
		if(userInput == godCheat)		
		{			
			togglePlayerGodMode();
		}
			
	//else
				
	//stringToEdit = "Incorrect Command, try again";
}
    
    

void Update()  
{
	if(Input.GetKeyUp(consoleKey))
	{
		toggleConsole();
	}
	

	/*if(isActive)
	{
		userInput = Input.inputString;
			
	if(userInput == godCheat)
	{		
		PlayerHealth ph = (PlayerHealth) target.GetComponent("PlayerHealth");		
		ph.toggleGodMode();	
	}	
	//else			
	//stringToEdit = "Incorrect Command, try again";
	}*/
    
}

public void toggleConsole()
{
	if(!isActive)
	{	
		isActive = true;
		Time.timeScale = 0.0f;
	}

	else if(isActive)
	{
		isActive = false;
		Time.timeScale = 1.0f;
	}
}

public void togglePlayerGodMode()
{
	PlayerHealth ph = (PlayerHealth) target.GetComponent("PlayerHealth");		
	ph.toggleGodMode();
}

}