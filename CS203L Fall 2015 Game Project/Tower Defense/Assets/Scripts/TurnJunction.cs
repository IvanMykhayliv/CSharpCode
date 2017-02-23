///Turn script by Josh Rand ☺
using UnityEngine;
using System.Collections;

public class TurnJunction : MonoBehaviour {
    bool position;
    public bool reverseDirection;   ///<Direction enemies should turn when the Reverse ability is in effect
    public float currentDirection
    {
        get
        {
            if (position)
            {
                return direction1;
            }
            else
            {
                return direction2;
            }
        }
    }
    public float direction1;        ///<Direction enemies should turn
    public float direction2;        ///<Direction enemies should turn

    ///<summary>
    /// function to have the enemies split along a path if there is more than one way to reach the end.
    /// </summary>
    public void Switch()
    {
        if (position)
        {
            position = false;
        }
        else
        {
            position = true;
        }
    }
}
