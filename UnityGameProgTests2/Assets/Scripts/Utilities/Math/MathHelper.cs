using UnityEngine;

public class MathHelper : MonoBehaviour
{
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
     * However, this does NOT mean that specifying the aforementioned zones is done through simple fractional proportions,
     * meaning an angle of 45 degrees within a pie slice zone is not achieved by having a Dot Product value of 0.5, or
     * "halfway through" to 90 degrees, a.k.a. the next orthogonal axis.
     * 
     * Instead, the Dot Product is essentially the X coordinate of a given angle's X and Y coordinates ON the Unit Circle
     * itself, whose values are found by taking the desired angle and then finding its Cosine. If it's a common angle value,
     * like 30 or 45, found in a 30 60 90 triangle or a 45 45 90 one respectively, then finding the Cosine can be achieved
     * using the expected, minimum lengths found in such triangles using SOHCAHTOA.
     *
     * Algebraic Notation, assumes Vectors in parameters are Normalized, Normalization must occur outside the function.*/
    public static float FrontOrBackDotH(Vector3 targetForwardNormalized, Vector3 targetterToTargetNormalized)
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
     *90 degrees Clockwise, with what was (1,0) facing Straight-Up, what was (0,1) facing Straight-Ahead, North, etc. Or, instead of the Target
     *facing East, it should be considered that the Target is now facing North, towards (0,1), which allows the Dot Product Unit Circle
     *interpretation to work as described above, but with the Target simply rotated 90 degrees counter-clockwise.
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
    public static float LeftOrRightDotH(Vector3 targetForwardNormalized, Vector3 targetterToTargetNormalized, Vector3 targetUpNormalized)
    {
        Vector3 crossUpOrDown;

        crossUpOrDown.x = (targetForwardNormalized.y * targetterToTargetNormalized.z) -
                          (targetForwardNormalized.z * targetterToTargetNormalized.y);

        crossUpOrDown.y = (targetForwardNormalized.z * targetterToTargetNormalized.x) -
                          (targetForwardNormalized.x * targetterToTargetNormalized.z);

        crossUpOrDown.z = (targetForwardNormalized.x * targetterToTargetNormalized.y) -
                          (targetForwardNormalized.y * targetterToTargetNormalized.x);

        float upOrDownVar = FrontOrBackDotH(crossUpOrDown, targetUpNormalized);


        return upOrDownVar;
    }
}