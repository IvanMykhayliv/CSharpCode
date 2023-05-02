using UnityEngine;
using UnityEngine.UI;

public class GameStateMgr : MonoBehaviour
{
    private Health playerHealth;
    private Health enemyHealth;

    private Rigidbody playerRB;
    private Rigidbody enemyRB;

    private Vector3 playerStartingPos;
    private Vector3 enemyStartingPos;
    private Quaternion playerStartingRot;
    private Quaternion enemyStartingRot;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();

        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        enemyRB = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>();

        playerStartingPos = playerRB.position;
        enemyStartingPos = enemyRB.position;
        playerStartingRot = playerRB.rotation;
        enemyStartingRot = enemyRB.rotation;
    }

    void Update()
    {
        ProcessGameStateInput();
    }

    /*Will cover things like menus and whether or not regular gameplay is occurring. For debugging, it will only process
    whether the Player or the Enemy is dead.*/
    private void ProcessGameStateInput()
	{
        if(Input.GetKeyDown(KeyCode.H) && !playerHealth.bIsDead)
		{
            playerHealth.Kill();
            DeclareOutcome("Enemy", "Player");
            ResetCharacters();
        }
        else if (Input.GetKeyDown(KeyCode.J) && !enemyHealth.bIsDead)
        {
            enemyHealth.Kill();
            DeclareOutcome("Player", "Enemy");
            ResetCharacters();
        }
    }

    /*May need to have 2 result statements eventually, for the sake of efficiency and not having to re-allocate memory for
    strings each time.*/
    private void DeclareOutcome(string victor, string loser)
	{
        string result = "Match has ended, the winner is the " + victor + ", and the loser is the " + loser;
        Debug.Log(result);
	}

    public void ResetCharacters()
	{
        playerHealth.ResetHealth();
        enemyHealth.ResetHealth();

        playerRB.velocity = Vector3.zero;
        enemyRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        enemyRB.angularVelocity = Vector3.zero;

        playerRB.position = playerStartingPos;
        enemyRB.position = enemyStartingPos;

        playerRB.rotation = playerStartingRot;
        enemyRB.rotation = enemyStartingRot;
    }
}