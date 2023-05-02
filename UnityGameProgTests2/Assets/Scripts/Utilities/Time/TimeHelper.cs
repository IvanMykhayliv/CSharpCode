using UnityEngine;

public class TimeHelper : MonoBehaviour
{
    public static bool HasReachedTimeLimit(int inputTargetTimeMS, float inputStartTime)
    {
        return (Time.deltaTime - inputStartTime) * 0.001 >= inputTargetTimeMS;
    }

    public static bool HasReachedTimeLimitFixed(int inputTargetTimeMS, float inputStartTime)
    {
        return (Time.fixedDeltaTime - inputStartTime) * 0.001 >= inputTargetTimeMS;
    }
}