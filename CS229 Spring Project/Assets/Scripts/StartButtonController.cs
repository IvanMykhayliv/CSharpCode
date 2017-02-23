using UnityEngine;
using System.Collections;

public class StartButtonController : MonoBehaviour
{
	public GameObject upSprite;
	public GameObject downSprite;
	public float downTime = 0.1f;
	public GameStates stateManager = null;
    public AudioSource titleMusic;
    public AudioSource gameMusic;
	
	private enum buttonStates
	{
		up = 0,
		down
	}
	
	private buttonStates currentState = buttonStates.up;
	private float nextStateTime = 0.0f;
	
	void Start()
	{
		upSprite.SetActive(true);
		downSprite.SetActive(false);
        titleMusic.Play();

	}
	
	void OnMouseDown()
	{
		GameObject play = GameObject.FindGameObjectWithTag("Player");
		play.GetComponent<PlayerStateListener>();

		if(nextStateTime == 0.0f && currentState == StartButtonController.buttonStates.up)
		{
			nextStateTime = Time.time + downTime;
			upSprite.SetActive(false);
			downSprite.SetActive(true);
			currentState = StartButtonController.buttonStates.down;

			if (gameObject.tag == "God") {
				play.GetComponent<PlayerStateListener> ().SetLives (100, true);
			} else {
				play.GetComponent<PlayerStateListener> ().SetLives (5, false);
			}
		}
	}
	
	void Update()
	{
		if(nextStateTime > 0.0f)
		{
			if(nextStateTime < Time.time)
			{
				// Set the button back to its 'up' state
				nextStateTime = 0.0f;
				upSprite.SetActive(true);
				downSprite.SetActive(false);
				currentState = StartButtonController.buttonStates.up;
				
				// Start the game!
				stateManager.startGame();
			}
		}
	}
}
