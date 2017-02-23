using UnityEngine;
using System.Collections;

public class PlayerMoveScript2 : MonoBehaviour 
{

	// Use this for initialization
    private Rigidbody2D moveBox;
    public float moveScale;
	void Start () 
    {
        moveBox = GetComponent <Rigidbody2D>();
	}
	
	// Update is called once per fixed frame
	void FixedUpdate () 
    {
        //Every fixed frame, horizontal key input is obtained
        float moveLat = Input.GetAxis("Horizontal");

        //Every fixed frame, vertical key input is obtained
        float moveVert = Input.GetAxis("Vertical");

        Vector2 force = new Vector2(moveLat, moveVert);

        moveBox.AddForce(force * moveScale);
	}
}
