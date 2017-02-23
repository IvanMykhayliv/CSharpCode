using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode upKey;
    public KeyCode downKey;
    //public Rigidbody2D playerBody;
    public Vector2 playerDir;
    public float xVal;
    public float yVal;
    public float moveForce;

	// Use this for initialization
	void Start () 
    {
        //playerDir.y = yVal;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        /*if (Input.GetKeyUp(downKey))
        {
            //playerDir.y = yVal;
            yVal = 5;
        }*/

        float h = Input.GetAxis("Horizontal");

        if (Input.GetKeyUp(rightKey))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
        }

        if (Input.GetKeyUp(leftKey))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * -h * moveForce);
        }

        float k = Input.GetAxis("Vertical");

        if (Input.GetKeyUp(upKey))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * -k * -moveForce);
        }

        if (Input.GetKeyUp(downKey))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * k * moveForce);
        }

	}
}
