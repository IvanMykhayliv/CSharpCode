using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour 
{
	public int maxHealth = 150;
	public int curHealth = 150;
	public float healthBarLength;
	public GUIText winText;
	public String loseMessage = "GAME OVER";
	public String godMessageTrue = "God Mode Active";
	public bool godMode = false;
	
	// Use this for initialization
	void Start () 
	{
		healthBarLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//adjustCurHealth(0);
		
		/*if(Input.GetKeyUp(KeyCode.G))
		{
			toggleGodMode();
		}*/
	}
	
	void OnGUI()
	{
		GUI.color = Color.red;
		GUI.backgroundColor = Color.red;
		GUI.contentColor = Color.red;
		GUI.Box(new Rect(10,10,healthBarLength, 20), curHealth + "/" + maxHealth);
	}
	
	public void adjustCurHealth(int adj)
	{
	
	if(!godMode)
	{
		curHealth += adj;
		
		if (curHealth < 0)
		{
			curHealth = 0;
			YouLose();
		}
		
		if (curHealth > maxHealth)
		{
			curHealth = 150;
		}
		
		if (maxHealth < 1)
		{
			maxHealth = 1;
		}
		healthBarLength = (Screen.width / 2) * (curHealth / (float) maxHealth);
	}
	}
	
	public void YouLose()
	{
		winText.text = loseMessage;
	}
	
	public void toggleGodMode()
	{
		if(!godMode)
		{
			godMode = true;
			winText.text = godMessageTrue;
		}
		else if(godMode)
		{
			godMode = false;
			winText.text = null;
			
			if (curHealth <= 0)
			{
				curHealth = 0;
				YouLose();
			}
		}
	}
}
