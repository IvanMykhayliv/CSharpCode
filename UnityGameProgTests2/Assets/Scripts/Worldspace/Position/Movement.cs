using System;
using UnityEngine;

//Basic Character Movement Script
public class Movement : MonoBehaviour
{
    internal Rigidbody rb;

    private float x, y, z;

    //Float Scalar for the Vector Movement Speed
    public float vel;
    private float angleVel;

    internal Vector3 moveVec;

    //Boolean to control whether or not Movement is possible, based on other states.
    public bool bCanWalk;

    void Start()
    {
        //Rigidbody Component to allow for Character Movement obtained from Game Object components.
        rb = GetComponent<Rigidbody>();

        /*Freeze Rigidbody Physics Rotation - Characters can still Rotate, just not in response to Physics and related Forces, which prevents
         *things such as Characters spinning out of control when colliding with other Characters, Geometry, etc.*/
        //rb.freezeRotation = true;

        /*How to specify which axes whose rotations should be frozen.
         * 
         * This represents bitwise operations in play. RigidbodyConstraints is an enum with hard-coded integers
         * representing different values that can be compared against with bitwise operators like |, a.k.a. OR, &, a.k.a.
         * AND, and even NANDS, XORs/exclusive ORs, etc.
         * 
         * FreezeRotationX has the hard-coded integer value 16, while FreezeRotationZ's is 64. When using an OR bitwise
         * operator, you are converting the 2 numbers into binary, and putting one below the other, as if doing arithmetic.
         * 
         * Then, based on your operator, you compare each aligned 0 and 1, from right to left, using the operator's
         * associated truth table - in the case of OR, the result of a comparison between 2 inputs only yields 1, or true,
         * if one or both of the inputs are 1, or true.
         * 
         * You repeat this process until you reach the end of the binary representation of both numbers. Then, the resultant
         * value can be converted back into decimal/integer format - in the case of freezing rotation X and Z, the
         * enum-to-integer value is 80.
         * 
         * 
         * This works because the binary representation of 80 just so happens to look like a combination of 2 hard-coded,
         * enum-to-integer values - think of the binary representation of these values as rows of on-off switches, which
         * happen to yield the combined effects of different enum-to-integer values if they look like the individual
         * binary representations of the enum-to-integer values that comprise the base enum values.
         * 
         * Because of how these bitwise comparisons work, if you know the hard-coded, enum-to-integer values in advance,
         * you can avoid the comparisons with the operators altogether and simply set the enum to the desired, valid,
         * combined value.
         * 
         * Interestingly enough, if the combined value is NOT a valid combination, meaning if one where to set the
         * constraints enum to 5, it will still partially work, because the binary representation is still comprised of a
         * valid on-off switch value, which is 4, or the enum-to-integer value of FreezePositionY.
         * 
         * This is demonstrated via the binary representations - 5 in binary is 101, while 4 is 100. Since 5 has the same
         * "switch" set to "on" as 4, meaning the 1st, left-most 1, then it is considered a valid value, and will yield the
         * behavior of 4, or FreezePositionY, and nothing else, because there are no enum-to-integer values of 1 in the
         * RigidbodyConstraints enum.
         * 
         * 
         * Thus, through this method, you can actually set a variable to have multiple values via these bitwise
         * combinations.
        */
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    void Update()
    {
        //Move once per frame
        Move();
    }

    void Move()
    {
        //If the Character is even able to walk, then do the following:
        if (bCanWalk)
        {
            //If the Game Object to which this Script is attached is tagged as "Player", then do the following:
            if (gameObject.tag == "Player")
            {
                //Refresh the Player's Movement
                RefreshPlayerMove();
            }

            //Otherwise, if the attached Game Object's tag is NOT "Player", then do the following:
            else
            {
                //The Rigidbody's Velocity is equal to the Game Object's Forward Facing Direction component multiplied by a Scalar
                rb.velocity = transform.forward * vel;
            }
        }
    }

    //Refreshes the Player's movement, intended to be used on a per-frame basis.

    /*NOTE: This movement code will always make the Player move along the Axes, meaning if W is pressed, then the Player will always move North,
    REGARDLESS OF ANY NEW FACING DIRECTION.*/
    void RefreshPlayerMove()
    {
        /*1. Set the Movement Vector to equal a new one that obtains axes for detecting default Left-to-Right and Forward-to-Backward Keyboard
        *input that is then multiplied by the scalar Vel, whereas the Vertical Y, Up-and-Down axis of the Movement Vector is equal to whatever it
        *currently is for the Player Movement-enabling Rigidbody component.*/

        /*TBD later, need to figure out how to make character orbit around target if moving horizontally. Then, to move
        forward and backward based off of the transform.forward Vector.*/
        y = rb.velocity.y;

        if (Input.GetAxis("Horizontal") != 0)
        {
            angleVel += vel;
            x = (float)Math.Sin(angleVel) + Input.GetAxis("Horizontal") * vel;
            z = (float)Math.Cos(angleVel) + Input.GetAxis("Vertical") * vel;
        }
        else
        {
            angleVel = 0;
            x = 0;
            z = Input.GetAxis("Vertical") * vel;
        }


        //moveVec = new Vector3(x, y, z);

        moveVec.x = x;
        moveVec.y = y;
        moveVec.z = z;

        /*2. The Rigidbody's Velocity is now equal to whatever the Movement Input Vector is, as described above.*/
        rb.velocity = moveVec;
    }
}