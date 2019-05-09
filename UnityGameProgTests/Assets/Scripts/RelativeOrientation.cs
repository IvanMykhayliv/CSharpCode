using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script for determining whether or not a given Targetter is in Front of, Behind, to the Left, to the Right of its Target, or some other
Orientation relative to it.*/

//Note to Remember: Unity's Vertical Axis is Y and Forward Axis is Z, rather than the other way around.
public class RelativeOrientation : MonoBehaviour
{
    //Targetter is whatever Entity has this script.

    /*Targetter's Relative Orientation States, specifying whether or not it is In Front Of, To the Left or Right Of, or Behind the Target, or if
    such information is completely unknown altogether, which it is from the start and if such information cannot be known for whatever other
    reason, such as if it is not relevant while the Targetter is not actually targetting the Target.*/
    public enum TARGETTERSTATE
    {
        UNKNOWN,
        INFRONTOF,
        TOTHELEFTOF,
        TOTHERIGHTOF,
        BEHIND
    }

    //Targetter's Current Relative Orientation State.
    public TARGETTERSTATE currentState;

    //Target that will be used to determine the Targetter's relative orientation to it.
    public GameObject target;

    /*Vectors that determine the Target's forward-facing direction, its upward direction, and the Vector tracking the distance From the Targetter
    to the Target.
    
      By default, the last Vector would instead track the distance from the Target to the Targetter, but due to Unity's Vertical Axis being Y and
    Forward Axis being Z rather than vice-versa, then so to must this particular Vector be. This prevents the Target's Forward Vector from needing
    to be set to a negative one several times.*/
    private Vector3 targetForward;
    private Vector3 targetUpward;
    private Vector3 targetterToTarget;

    /*Minimum and Maximum Values for the Dot Product to specify the Maximum FoV value for what constitutes a Targetter being "In Front Of" a
    Target; should be 0.5f in the Inspector for the former, for 45 degrees from the Target's Front, and 1.0f for the latter, with the latter
    never being able to change.*/
    public float minForwardDotRangeVal;
    private float maxForwardDotRangeVal;

    /*Minimum and Maximum Values for the Cross-Dot Product to further specify the Maximum FoV value for what constitutes a Targetter being "In
    Front Of" a Target; should be ((-Mathf.Sqrt(2)) / 2) for the former, for 45 degrees from the Target's Front or Back, and
    ((Mathf.Sqrt(2)) / 2) for the latter, as per the intended Dot Product Minimum Forward Range Value specified in the above comment.*/
    private float minForwardCrossDotRangeVal;
    private float maxForwardCrossDotRangeVal;

    /*Minimum and Maximum Values for the Dot Product to further specify the Maximum FoV value for what constitutes a Targetter being "To the Right
    Of" a Target; should be -0.5f for the former, for 45 degrees from the Target's Left or Right, and 0.5f for the latter, with both values being
    only subject to change if the Minimum Cross-Dot Right Value changes as well.*/
    private float minRightDotRangeVal;
    private float maxRightDotRangeVal;

    /*Minimum and Maximum Values for the Cross-Dot Product to specify the Maximum FoV value for what constitutes a Targetter being "To the Right
    Of" a Target; should be ((Mathf.Sqrt(2)) / 2) (Or 0.7071068 in the Inspector) for the former, for 45 degrees from the Target's Right, and 1.0f
    for the latter, with the latter never being able to change.*/
    public float minRightCrossDotRangeVal;
    private float maxRightCrossDotRangeVal;

    /*Minimum and Maximum Values for the Dot Product to further specify the Maximum FoV value for what constitutes a Targetter being "To the Left
    Of" a Target; should be -0.5f for the former, for 45 degrees from the Target's Left or Right, and 0.5f for the latter, with both values being
    only subject to change if the Maximum Cross-Dot Left Value changes as well.*/
    private float minLeftDotRangeVal;
    private float maxLeftDotRangeVal;

    /*Minimum and Maximum Values for the Cross-Dot Product to specify the Maximum FoV value for what constitutes a Targetter being "To the Left
    Of" a Target; should be -1.0f for the former and ((-Mathf.Sqrt(2)) / 2) (Or -0.7071068 in the Inspector) for the latter, for 45 degrees from
    the Target's Left for the latter, with the former never being able to change.*/
    private float minLeftCrossDotRangeVal;
    public float maxLeftCrossDotRangeVal;

    /*Minimum and Maximum Values for the Dot Product to specify the Maximum FoV value for what constitutes a Targetter being "Behind" a Target;
    should be -0.5f in the Inspector for the latter, for 45 degrees from the Target's Back, and -1.0f for the former, with the former never being
    able to change.*/
    private float minRearDotRangeVal;
    public float maxRearDotRangeVal;

    /*Minimum and Maximum Values for the Cross-Dot Product to further specify the Maximum FoV value for what constitutes a Targetter being
    "Behind" a Target; should be ((-Mathf.Sqrt(2)) / 2) for the former, for 45 degrees from the Target's Front or Back, and
    ((Mathf.Sqrt(2)) / 2) for the latter, as per the intended Dot Product Maximum Rear Range Value specified in the above comment.*/
    private float minRearCrossDotRangeVal;
    private float maxRearCrossDotRangeVal;

    //For Variable Value Initialization upon Game Start.
    void Start()
    {
        currentState = TARGETTERSTATE.UNKNOWN;

        maxForwardDotRangeVal = 1.0f;
        //minForwardCrossDotRangeVal = ((-Mathf.Sqrt(2)) / 2);
        //maxForwardCrossDotRangeVal = ((Mathf.Sqrt(2)) / 2);
        minForwardCrossDotRangeVal = (float) -0.7071068;
        maxForwardCrossDotRangeVal = (float) 0.7071068;

        minRightDotRangeVal = -0.5f;
        maxRightDotRangeVal = 0.5f;
        maxRightCrossDotRangeVal = 1.0f;

        minLeftDotRangeVal = -0.5f;
        maxLeftDotRangeVal = 0.5f;
        minLeftCrossDotRangeVal = -1.0f;

        minRearDotRangeVal = -1.0f;
        //minRearCrossDotRangeVal = ((-Mathf.Sqrt(2)) / 2);
        //maxRearCrossDotRangeVal = ((Mathf.Sqrt(2)) / 2);
        minRearCrossDotRangeVal = (float) -0.7071068;
        maxRearCrossDotRangeVal = (float) 0.7071068;
    }
    //Update should be used, due to it being consistent and because Physics calculations are non-factorial for this script.
	void Update()
    {
        processRelativeOrientationH();
        
        /*Draw a line in the Scene View Window that tracks the distance and orientation from the Targetter to the Target; essentially a
        debug-based, visualization of targetterToTarget.*/
        //Debug.DrawLine(transform.position, target.transform.position, Color.yellow);
	}

    /*
     * Handmade Function for finding the Dot Product between 2 Vectors, which is useful for determining whether a Targetter is in Front of, Behind,
     *or to an indeterminate Side of its Target.
     *
     * The floating point number that gets returned can be used to specify what value ranges should constitute "Forward", "To the Side", or
     *"Behind" a given Target. It should be interpreted that the Target is always facing East and on the Unit Circle, thus facing (1,0), with the
     *Dot Product value representing a given X-Coordinate value above and below the X-Axis that falls on the Unit Circle.
     * 
     * If the Dot Product is 1, then the Targetter is directly in Front of the Target. If the Dot Product is 0, then the Targetter is directly to
     *the Left OR the Right of a given Target. If the Dot Product is -1, then the Targetter is directly Behind the Target.
     * 
     * Using the above base values, individual value ranges can be specified to stipulate what exactly should constitute being "In Front Of",
     *"Behind", or "To Some Indeterminate Side" of a given Target, and should be interpreted as specifying "Zones" of the Unit Circle by
     *demarcating different fractional parts of the Unit Circle, processed as Circle Sector Pie Slices.
     *
     * Algebraic Notation, assumes Vectors in parameters are Normalized, Normalization must occur outside the function.*/
    private float FrontOrBackDotH (Vector3 targetForwardNormalized, Vector3 targetterToTargetNormalized)
    {
        float frontOrBackVar = (targetForwardNormalized.x * targetterToTargetNormalized.x) +
                               (targetForwardNormalized.y * targetterToTargetNormalized.y) +
                               (targetForwardNormalized.z * targetterToTargetNormalized.z);


        return frontOrBackVar;
    }

    /*
     * Handmade Function that uses Cross Products, and then Dot Products, to find out whether or not a Targetter is to the Left or Right of its
     *target.
     *
     * This is highly recommended if different Behavior is desired whenever the Targetter is to a specific Side of its Target, and helps
     *to specify individual "Circle Sector Zones" (As described in the above comment) even further, such as whether or not it should be accounted
     *for if the Targetter is diagonally Northeast to its Target, at 2:00 relative to its Target (With the clock being on the Target), and so on,
     *all determined by specified value ranges.
     *
     * What is occurring in this function is that the Cross Product is discovered between the Target's Forward Vector and the Vector that tracks
     *the distance From the Target To the Targetter, done Algebraically by setting values for the Cross Product Vector's individual X, Y, and Z
     *components. This yields a Non-Unit, Non-Normalized Vector that is always perpindicular to the 2 input Vectors. Specifically: An Upward
     *Vector is yielded if the Targetter is anywhere to the Right of the Target, or a Downward one  if the Targetter is anywhere to the Left
     *instead.
     *After this has occurred, the Cross Product Vector is tested against the Target's presumably normalized Up Vector, utilizing the Dot Product.
     *When this happens, the Unit Circle described in the Handmade Dot Product function comments is Pitched Upwards by 90 degrees and then Yawed
     *90 degrees Clockwise, with what was (1,0) facing Straight-Up, what was (0,1) facing Straight-Ahead, North, etc. Or, in other words, instead
     *of the Target facing East, it should be considered that the Target is now facing North, towards (0,1), which allows the Dot Product Unit
     *Circle interpretation to work as described above, but with the Target simply rotated 90 degrees counter-clockwise.
     *
     * Once the Dot Product between the Cross Product and the Target's Up Vector has occurred, the final value, similar to the Dot Product by
     *itself, determines the Targetter's Orientation relative to its Target's sides, with the base values being as follows:
     *
     * If the Dot Product is 1, then the Targetter is directly to the Right of the Target. If the Dot Product is 0, then the Targetter is directly
     *in Front of or Behind the Target. Finally, if the Dot Product is -1, then the Targetter is directly to the Left of the Target.
     *
     * Algebraic Notation, assumes Vectors in Parameters are Normalized, Normalization must be done outside the function. Finding only the Dot
     *Product between the X and Z axes would suffice, but the Cross Product Vector used for it CANNOT be Normalized, as it will lose far too much
     *significance and be unable to determine Front or Behind Orientation.*/
    private float LeftOrRightDotH(Vector3 targetForwardNormalized, Vector3 targetterToTargetNormalized, Vector3 targetUpNormalized)
    {
        Vector3 crossUpOrDown;

        crossUpOrDown.x = (targetForwardNormalized.y * targetterToTargetNormalized.z) -
                          (targetForwardNormalized.z * targetterToTargetNormalized.y);

        crossUpOrDown.y = (targetForwardNormalized.z * targetterToTargetNormalized.x) -
                          (targetForwardNormalized.x * targetterToTargetNormalized.z);

        crossUpOrDown.z = (targetForwardNormalized.x * targetterToTargetNormalized.y) -
                          (targetForwardNormalized.y * targetterToTargetNormalized.x);

        float upOrDownVar = FrontOrBackDotH(crossUpOrDown, targetUpNormalized);

        /*float upOrDownVar = (crossUpOrDown.x * targetUp.x) +
                              (crossUpOrDown.y * targetUp.y) +
                              (crossUpOrDown.z * targetUp.z);*/


        return upOrDownVar;
    }

    //Dot Product function that utilizes Unity's static Dot Product function.
    private float FrontOrBackDotU (Vector3 targetForwardInput, Vector3 targetterToTargetInput)
    {
        float frontOrBackVar = Vector3.Dot(targetForwardInput.normalized, targetterToTargetInput.normalized);


        return frontOrBackVar;
    }

    /*Function that uses Cross Products, and then Dot Products, to find out whether or not a Targetter is to the Left or Right of its target, via
    Unity's Cross and Dot Product functions.*/
    private float LeftOrRightDotU(Vector3 targetForwardInput, Vector3 targetterToTargetInput, Vector3 targetUpInput)
    {
        Vector3 crossUpOrDown = Vector3.Cross(targetForwardInput.normalized, targetterToTargetInput.normalized);

        float upOrDownVar = Vector3.Dot(crossUpOrDown, targetUpInput.normalized);


        return upOrDownVar;
    }

    /*Function that determines a Targetter's relative orientation to its Target, utilizing almost only the handmade functions and modifiable
    values. Also, Comparison Operators may change depending on requirements and further tests.*/
    void processRelativeOrientationH()
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

        /*If the Targetter is from 45 degrees in Front of, Inclusive, to Straight-Ahead to the Target And most assurredly in Front of or Behind
        it, then it should be considered "In Front Of" the Target.*/
        if
        (
            FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized) >= minForwardDotRangeVal &&
            FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized) <= maxForwardDotRangeVal &&
            LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized) >= minForwardCrossDotRangeVal &&
            LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized) <= maxForwardCrossDotRangeVal
        )
        {
            currentState = TARGETTERSTATE.INFRONTOF;
            //Debug.Log("Targetter is " + currentState + " the Target");
        }

        /*If the Targetter is from 45 degrees Behind, Inclusive, to 45 degrees in Front of, Exclusive, to the Target And most assurredly to the
        Right of it, then it should be considered "To the Right Of" the Target.*/
        if
        (
            FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized) >= minRightDotRangeVal &&
            FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized) < maxRightDotRangeVal &&
            LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized) > minRightCrossDotRangeVal &&
            LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized) <= maxRightCrossDotRangeVal
        )
        {
            currentState = TARGETTERSTATE.TOTHERIGHTOF;
            //Debug.Log("Targetter is " + currentState + " the Target");
        }

        /*If the Targetter is from 45 degrees Behind, Inclusive, to 45 degrees in Front of, Exclusive, to the Target And most assurredly to the
        Left of it, then it should be considered "To the Left Of" the Target.*/
        if
        (
            FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized) >= minLeftDotRangeVal &&
            FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized) < maxLeftDotRangeVal &&
            LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized) >= minLeftCrossDotRangeVal &&
            LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized) < maxLeftCrossDotRangeVal
        )
        {
            currentState = TARGETTERSTATE.TOTHELEFTOF;
            //Debug.Log("Targetter is " + currentState + " the Target");
        }

        /*If the Targetter is from 45 degrees Behind, Exclusive, to Straight-Behind the Target And most assurredly in Front of or Behind it, then
        it should be considered "Behind" the Target.*/
        if
        (
            FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized) >= minRearDotRangeVal &&
            FrontOrBackDotH(targetForward.normalized, targetterToTarget.normalized) < maxRearDotRangeVal &&
            LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized) > minRearCrossDotRangeVal &&
            LeftOrRightDotH(targetForward.normalized, targetterToTarget.normalized, targetUpward.normalized) < maxRearCrossDotRangeVal
        )
        {
            currentState = TARGETTERSTATE.BEHIND;
            //Debug.Log("Targetter is " + currentState + " the Target");
        }
    }

    //Function that determines a Targetter's relative orientation to its Target, utilizing almost only Unity-available functions.
    void processRelativeOrientationU()
    {
        targetForward = target.transform.forward;
        targetterToTarget = transform.position - target.transform.position;

        /*Vector doesn't need to be negative, as Unity's default Cross Product static function interprets the formula as originally written: With
        the Vertical axis being Z and the Forward one being Y.*/
        targetUpward = target.transform.up;

        if
        (
            FrontOrBackDotU(targetForward, targetterToTarget) >= 0.5f &&
            FrontOrBackDotU(targetForward, targetterToTarget) <= 1.0f &&
            LeftOrRightDotU(targetForward, targetterToTarget, targetUpward) >= ((-Mathf.Sqrt(2)) / 2) &&
            LeftOrRightDotU(targetForward, targetterToTarget, targetUpward) <= ((Mathf.Sqrt(2)) / 2)
        )
        {
            currentState = TARGETTERSTATE.INFRONTOF;
            //Debug.Log("Targetter is " + currentState + " the Target");
        }
        if
        (
            FrontOrBackDotU(targetForward, targetterToTarget) >= -0.5f &&
            FrontOrBackDotU(targetForward, targetterToTarget) < 0.5f &&
            LeftOrRightDotU(targetForward, targetterToTarget, targetUpward) > ((Mathf.Sqrt(2)) / 2) &&
            LeftOrRightDotU(targetForward, targetterToTarget, targetUpward) <= 1.0f
        )
        {
            currentState = TARGETTERSTATE.TOTHERIGHTOF;
            //Debug.Log("Targetter is " + currentState + " the Target");
        }
        if
        (
            FrontOrBackDotU(targetForward, targetterToTarget) >= -0.5f &&
            FrontOrBackDotU(targetForward, targetterToTarget) < 0.5f &&
            LeftOrRightDotU(targetForward, targetterToTarget, targetUpward) >= -1.0f &&
            LeftOrRightDotU(targetForward, targetterToTarget, targetUpward) < ((-Mathf.Sqrt(2)) / 2)
        )
        {
            currentState = TARGETTERSTATE.TOTHELEFTOF;
            //Debug.Log("Targetter is " + currentState + " the Target");
        }
        if
        (
            FrontOrBackDotU(targetForward, targetterToTarget) >= -1.0f &&
            FrontOrBackDotU(targetForward, targetterToTarget) < -0.5f &&
            LeftOrRightDotU(targetForward, targetterToTarget, targetUpward) > ((-Mathf.Sqrt(2)) / 2) &&
            LeftOrRightDotU(targetForward, targetterToTarget, targetUpward) < ((Mathf.Sqrt(2)) / 2)
        )
        {
            currentState = TARGETTERSTATE.BEHIND;
            //Debug.Log("Targetter is " + currentState + " the Target");
        }
    }
}
