using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour 
{

    public GameObject player;
    public GameObject pickup;
    public Renderer rendPl;
    public Renderer rendPi;
    public static Text scoreText;
    public static int score;
    public int maxScore;


	// Use this for initialization
	void Start () 
    {
        rendPi = pickup.GetComponent<Renderer>();
        rendPl = player.GetComponent<Renderer>();
        setScoreText();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        setScoreText();
	    if(score >= maxScore)
        {
            setWin();
        }

        if (rendPi.bounds.Intersects(rendPl.bounds))
        {
            pickupOrb();
        }
	}

    public void setWin()
    {
        score = maxScore;
        scoreText.text = "You win!";
    }

    public void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void pickupOrb()
    {
        rendPi.enabled = false;
        score++;
    }
}
