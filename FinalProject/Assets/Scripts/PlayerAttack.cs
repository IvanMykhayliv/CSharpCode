using UnityEngine;
using System.Collections;
using System;

public class PlayerAttack : MonoBehaviour 
{
	public GameObject target;
	public float attackTimer;
	public int attackPower = 10;
	public float coolDown;
	public int score;
	public int scoreGoal = 4;
	public GUIText scoreText;
	public GUIText winText;
	public String winMessage = "VICTORY!";
	
	// Use this for initialization
	void Start () 
	{
		setScoreText();
		winText.text = " ";
		attackTimer = 0;
		coolDown = 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(attackTimer > 0)
			attackTimer -= Time.deltaTime;
			
		if(attackTimer < 0)
			attackTimer = 0;
			
		if(Input.GetKeyUp(KeyCode.F))
		{
			if(attackTimer == 0)
			{
				Attack();
				attackTimer = coolDown;
			}
		}
	}
	
	private void Attack()
	{
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot(dir, transform.forward);
		
		Debug.Log(direction);
		
		if(distance < 2.5f)
		{
			if(direction > 0)
			{
				EnemyHealth eh = (EnemyHealth) target.GetComponent("EnemyHealth");
				EnemyAttack ea = (EnemyAttack) target.GetComponent("EnemyAttack");
				
				if (ea.attackTimer >= 1.63)
				{
					eh.adjustCurHealth(-attackPower * 2);
				}
				else
				{
					eh.adjustCurHealth(-attackPower);
				}
				
				if (eh.curHealth <= 0)
				{
					eh.curHealth = 0;
					score++;
					target.SetActive(false);
					setScoreText();
				}
				
				if (score >= scoreGoal)
				{
					score = scoreGoal;
					YouWin();
				}
			}
		}
	}
	
	public void setScoreText()
	{
		scoreText.text = "Score: " + score.ToString();
	}
	
	public void YouWin()
	{
		winText.text = winMessage;
	}
}
