public enum TargetterOrientation
{
    /*Targetter's Relative Orientation States, specifying whether or not it is In Front Of, To the Left or Right Of, or Behind the Target, or if
    such information is completely unknown altogether, which it is from the start and if such information cannot be known for whatever other
    reason, such as if it is not relevant while the Targetter is not actually targetting the Target.*/
    UNKNOWN,
    INFRONTOF,
    TOTHELEFTOF,
    TOTHERIGHTOF,
    BEHIND,
    ABOVE,
    BELOW
}