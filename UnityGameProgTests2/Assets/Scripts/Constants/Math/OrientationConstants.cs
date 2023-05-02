public static class OrientationConstants
{
    #region Orientation Ranges, Modifiable
    public static float MinForwardDotRangeVal = 0.7071068f;

    public static float MinRightCrossDotRangeVal = 0.7071068f;

    public static float MaxLeftCrossDotRangeVal = -0.7071068f;

    public static float MaxRearDotRangeVal = -0.7071068f;
    #endregion

    #region Orientation Ranges, Fixed
    #region Orientation Ranges, Targetter is in Front of Target, Fixed
    public static readonly float MaxForwardDotRangeVal = 1.0f;
    public static readonly float MinForwardCrossDotRangeVal = -MinForwardDotRangeVal;
    public static readonly float MaxForwardCrossDotRangeVal = MinForwardDotRangeVal;
    #endregion

    #region Orientation Ranges, Targetter is to the Right of Target, Fixed
    public static readonly float MinRightDotRangeVal = -MinRightCrossDotRangeVal;
    public static readonly float MaxRightDotRangeVal = MinRightCrossDotRangeVal;
    public static readonly float MaxRightCrossDotRangeVal = 1.0f;
    #endregion

    #region Orientation Ranges, Targetter is to the Left of Target, Fixed
    public static readonly float MinLeftDotRangeVal = MaxLeftCrossDotRangeVal;
    public static readonly float MaxLeftDotRangeVal = -MaxLeftCrossDotRangeVal;
    public static readonly float MinLeftCrossDotRangeVal = -1.0f;
    #endregion

    #region Orientation Ranges, Targetter is Behind Target, Fixed
    public static readonly float MinRearDotRangeVal = -1.0f;
    public static readonly float MinRearCrossDotRangeVal = MaxRearDotRangeVal;
    public static readonly float MaxRearCrossDotRangeVal = -MaxRearDotRangeVal;
    #endregion
    #endregion
}