using UnityEngine;
using System.Collections;

public class LavaMover : MonoBehaviour 
{
    public float moveRate;
    private float moveCounter;
    public float moveMax;
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        transform.Translate(new Vector3(0f, moveRate * Time.deltaTime, 0f));
        moveCounter += moveRate;

        if(moveCounter > moveMax)
        {
            //transform.Translate(new Vector3(0f, -moveRate * Time.deltaTime, 0f));
            moveCounter = moveMax;
            moveRate *= -1;
        }

        else if (moveCounter < 0)
        {
            //transform.Translate(new Vector3(0f, -moveRate * Time.deltaTime, 0f));
            moveCounter = 0;
            moveRate *= -1;
        }
	}
}
