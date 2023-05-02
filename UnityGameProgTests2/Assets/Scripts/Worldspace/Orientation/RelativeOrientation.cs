using UnityEngine;

/*Script for determining whether or not a given Targetter is in Front of, Behind, to the Left, to the Right of its Target, or some other
Orientation relative to it.*/

//Note to Remember: Unity's Vertical Axis is Y and Forward Axis is Z, rather than the other way around.

/*IMPORTANT NOTE: Normal mathematical Unit Circle rules are still in play, meaning if you want to specify zones using
Dot Products, you have to understand that the Dot Product will represent the X value on the Unit Circle, as described below.
 
 To put it briefly, a Dot Product of 1 means that a Vector is directly in front of another, which corresponds to the X
 value of the Unit Circle 1, of the coordinates (1,0), which is where the Vector in front of the other would be, in this
 case. If a Vector was directly to the Left or Right of another Vector, the Dot Prodct would be 0, to correspond with the
 fact that the X value representing the Unit Circle Dot Product is 0 at coordinates (0,1) and (0, -1).
 
 This does NOT mean that a Dot Product of 0.5 puts a Vector halfway between 0 and 1, and that a Vector is thus halfway through
a quarter-slice of a circle, perfectly at the edge of a 45 degree angle eminating from the forward Vector.
 
 Instead, on the Unit Circle, the X value 0.5 corresponds to an angle of 60 degrees, not 45. If 45 degrees is desired, and
 it should be, then the intended Dot Product should be Sqrt(2)/2, or 0.7071068, as is shown on the Unit Circle.*/
public class RelativeOrientation : MonoBehaviour
{
    //Targetter is whatever Entity has this script.

    //Targetter's Next Relative Orientation State.
    private TargetterOrientation nextState;

    //Targetter's Current Relative Orientation State.
    public TargetterOrientation currentState;

    //Targetter's Previous Relative Orientation State.
    public TargetterOrientation previousState;

    //Target that will be used to determine the Targetter's relative orientation to it.
    public GameObject target;

    /*Vectors that determine the Target's forward-facing direction, its upward direction, and the Vector tracking the distance From the Targetter
    to the Target.
    
      By default, the last Vector would instead track the distance from the Target to the Targetter, but due to Unity's Vertical Axis being Y and
    Forward Axis being Z rather than vice-versa, then so to must this particular Vector be. This prevents the Target's Forward Vector from needing
    to be set to a negative one several times. Also, access level is internal so that other scripts referencing this one may use the Vector, but
    it cannot be modified in the Inspector.*/
    private Vector3 targetForward;
    private Vector3 targetUpward;

    /*Change to private and provide a getter later. Either that, or convert into a property, like with the other Vector3s
    and RelativeOrientation enums.*/
    internal Vector3 targetterToTarget;

    private float frontOrBackDotH;
    private float leftOrRightDotH;

    //For Variable Value Initialization upon Game Start.
    void Start()
    {
        currentState = TargetterOrientation.UNKNOWN;

        previousState = currentState;
    }
    //Update should be used, due to it being consistent and because Physics calculations are non-factorial for this script.
    void Update()
    {
        ProcessRelativeOrientation();

        /*Draw a line in the Scene View Window that tracks the distance and orientation from the Targetter to the Target; essentially a
        debug-based, visualization of targetterToTarget.*/
        //Debug.DrawLine(transform.position, target.transform.position, Color.yellow);
    }

    private void ProcessRelativeOrientation()
    {
        CalculateRelativeOrientation();

        /*Will likely be moved to individual scripts, as it would be too much to expect one script to handle every possible
        need for such a fundamental, wide-reaching concept such as relative orientation.*/
        switch (currentState)
        {
            case TargetterOrientation.INFRONTOF:
                break;
            case TargetterOrientation.TOTHERIGHTOF:
                break;
            case TargetterOrientation.TOTHELEFTOF:
                break;
            case TargetterOrientation.BEHIND:
                break;
            case TargetterOrientation.UNKNOWN:
            default:
                break;
        }
    }

    //Implementation for later.
    //public Vector3 GetTargetterToTarget() { return targetterToTarget; }

    //Assigns what each Vector variable should be.
    private void AssignVecs()
    {
        //Target's Forward Vector is equal to that of its Transform component
        targetForward = target.transform.forward;

        /*Vector for keeping track of distance From the Targetter to the Target is equal to the Targetter's position, determined by its Transform
        component, subtracted by the Target's position.*/
        targetterToTarget = transform.position - target.transform.position;

        /*Target's Up Vector is equal to that of its Transform component; script only works as intended when this Vector is negative if the
        function that uses it is rewritten to accommodate the fact that Unity uses Y for its Vertical axis and Z for its Forward one rather than
        vice-versa.*/
        targetUpward = target.transform.up;
    }

    /*Function that determines a Targetter's relative orientation to its Target, utilizing almost only the handmade functions and modifiable
    values. Also, Comparison Operators may change depending on requirements and further tests.*/

    /*NOTE: Switching the subsequent "ifs" into "else ifs" and then adding the final else results in the pure diagonal orientations being returned
    as "Unknowns", despite the assigned values being made to accomodate that; elab. on this later.
    
     UPDATE: The problem above was likely due to the incorrect values being used for specifying 45 degree cones. Re-adding the
    else ifs and final else statement after correcting the values does not replicate the inaccurate behavior.
    
     What is a problem now is, if the Player collides with anything, his body starts spinning out of control, and his Relative
    Orientation value with it. This is likely due to the Forward Vector being used stemming from the body, not the head.*/
    public TargetterOrientation CalculateRelativeOrientation()
    {
        AssignVecs();

        frontOrBackDotH = MathHelper.FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized);
        leftOrRightDotH = MathHelper.LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized);

        /*If the Targetter is from 45 degrees in Front of, Inclusive, to Straight-Ahead to the Target And most assurredly in Front of or Behind
        it, then it should be considered "In Front Of" the Target.*/
        if
        (
            frontOrBackDotH >= OrientationConstants.MinForwardDotRangeVal &&
            frontOrBackDotH <= OrientationConstants.MaxForwardDotRangeVal &&
            leftOrRightDotH >= OrientationConstants.MinForwardCrossDotRangeVal &&
            leftOrRightDotH <= OrientationConstants.MaxForwardCrossDotRangeVal
        )
        {
            nextState = TargetterOrientation.INFRONTOF;
        }

        /*If the Targetter is from 45 degrees Behind, Inclusive, to 45 degrees in Front of, Exclusive, to the Target And most assurredly to the
        Right of it, then it should be considered "To the Right Of" the Target.*/
        else if
        (
            frontOrBackDotH >= OrientationConstants.MinRightDotRangeVal &&
            frontOrBackDotH < OrientationConstants.MaxRightDotRangeVal &&
            leftOrRightDotH > OrientationConstants.MinRightCrossDotRangeVal &&
            leftOrRightDotH <= OrientationConstants.MaxRightCrossDotRangeVal
        )
        {
            nextState = TargetterOrientation.TOTHERIGHTOF;
        }

        /*If the Targetter is from 45 degrees Behind, Inclusive, to 45 degrees in Front of, Exclusive, to the Target And most assurredly to the
        Left of it, then it should be considered "To the Left Of" the Target.*/
        else if
        (
            frontOrBackDotH >= OrientationConstants.MinLeftDotRangeVal &&
            frontOrBackDotH < OrientationConstants.MaxLeftDotRangeVal &&
            leftOrRightDotH >= OrientationConstants.MinLeftCrossDotRangeVal &&
            leftOrRightDotH < OrientationConstants.MaxLeftCrossDotRangeVal
        )
        {
            nextState = TargetterOrientation.TOTHELEFTOF;
        }

        /*If the Targetter is from 45 degrees Behind, Exclusive, to Straight-Behind the Target And most assurredly in Front of or Behind it, then
        it should be considered "Behind" the Target.*/
        else if
        (
            frontOrBackDotH >= OrientationConstants.MinRearDotRangeVal &&
            frontOrBackDotH < OrientationConstants.MaxRearDotRangeVal &&
            leftOrRightDotH > OrientationConstants.MinRearCrossDotRangeVal &&
            leftOrRightDotH < OrientationConstants.MaxRearCrossDotRangeVal
        )
        {
            nextState = TargetterOrientation.BEHIND;
        }
        else
        {
            nextState = TargetterOrientation.UNKNOWN;
        }

        if (nextState != currentState)
        {
            previousState = currentState;
            currentState = nextState;

            Debug.Log("Targetter is " + currentState + " the Target");
        }

        return currentState;
    }

    //Relational pattern variant. Doesn't work, even though it should; newer version of .NET C# required.
    /*void ProcessRelativeOrientationHOptimized()
    {
        AssignVecs();

        frontOrBackDotH = MathHelper.FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized);
        leftOrRightDotH = MathHelper.LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized);

        switch(frontOrBackDotH + leftOrRightDotH)
		{
            case float value when
            value - leftOrRightDotH >= OrientationConstants.MinForwardDotRangeVal &&
            value - leftOrRightDotH <= OrientationConstants.MaxForwardDotRangeVal &&
            value - frontOrBackDotH >= OrientationConstants.MinForwardCrossDotRangeVal &&
            value - frontOrBackDotH <= OrientationConstants.MaxForwardCrossDotRangeVal:
                currentState = TargetterOrientation.INFRONTOF;
                break;
            case float value when
            value - leftOrRightDotH >= OrientationConstants.MinRightDotRangeVal &&
            value - leftOrRightDotH <  OrientationConstants.MaxRightDotRangeVal &&
            value - frontOrBackDotH >  OrientationConstants.MinRightCrossDotRangeVal &&
            value - frontOrBackDotH <= OrientationConstants.MaxRightCrossDotRangeVal:
                currentState = TargetterOrientation.TOTHERIGHTOF;
                break;
            case float value when
            value - leftOrRightDotH >= OrientationConstants.MinLeftDotRangeVal &&
            value - leftOrRightDotH <  OrientationConstants.MaxLeftDotRangeVal &&
            value - frontOrBackDotH >= OrientationConstants.MinLeftCrossDotRangeVal &&
            value - frontOrBackDotH <  OrientationConstants.MaxLeftCrossDotRangeVal:
                currentState = TargetterOrientation.TOTHELEFTOF;
                break;
            case float value when
            value - leftOrRightDotH >= OrientationConstants.MinRearDotRangeVal &&
            value - leftOrRightDotH <  OrientationConstants.MaxRearDotRangeVal &&
            value - frontOrBackDotH >  OrientationConstants.MinRearCrossDotRangeVal &&
            value - frontOrBackDotH <  OrientationConstants.MaxRearCrossDotRangeVal:
                currentState = TargetterOrientation.BEHIND;
                break;
            default:
                currentState = TargetterOrientation.UNKNOWN;
                break;
        }

        if (nextState != currentState)
        {
            previousState = currentState;
            currentState = nextState;

            Debug.Log("Targetter is " + currentState + " the Target");
        }
    }*/
}