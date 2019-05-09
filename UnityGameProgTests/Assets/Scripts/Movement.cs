using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic Character Movement Script
public class Movement : MonoBehaviour
{
    private Rigidbody rb;

    //Float Scalar for the Vector Movement Speed
    public float vel;

    private Vector3 moveInput;
    private Vector3 moveVec;

    //Boolean to control whether or not Movement is possible, based on other states.
    public bool bCanWalk;

    void Start()
    {
        //Rigidbody Component to allow for Character Movement obtained from Game Object components.
        rb = GetComponent<Rigidbody>();

        /*Freeze Rigidbody Physics Rotation - Characters can still Rotate, just not in response to Physics and related Forces, which prevents
         *things such as Characters spinning out of control when colliding with other Characters, Geometry, etc.*/
        rb.freezeRotation = true;
    }
    void Update()
    {
        //Move once per frame
        move();
    }
   
    void move()
    {
        //If the Character is even able to walk, then do the following:
        if (bCanWalk)
        {
            //If the Game Object to which this Script is attached is tagged as "Player", then do the following:
            if (gameObject.tag == "Player")
            {
                /*1. Set the Movement Vector to equal a new one that obtains axes for detecting default Left-to-Right and Forward-to-Backward Keyboard
                *input that is then multiplied by the scalar Vel, whereas the Vertical Y, Up-and-Down axis of the Movement Vector is equal to
                *whatever it currently is for the Player Movement-enabling Rigidbody component.*/
                moveVec = new Vector3(Input.GetAxis("Horizontal") * vel, rb.velocity.y, Input.GetAxis("Vertical") * vel);

                /*2. A separate Movement Vector for detecting input is now equal to what Movement Vec has been established to be.*/
                moveInput = moveVec;

                /*3. The Rigidbody's Velocity is now equal to whatever the Movement Input Vector is, as described above.*/
                rb.velocity = moveInput;
            }

            //Otherwise, if the attached Game Object's tag is NOT "Player", then do the following:
            else
            {
                //The Rigidbody's Velocity is equal to the Game Object's Forward Facing Direction component multiplied by a Scalar
                rb.velocity = transform.forward * vel;
            }
        }
    }
}
