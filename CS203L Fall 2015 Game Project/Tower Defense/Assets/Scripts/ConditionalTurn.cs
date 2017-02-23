using UnityEngine;
using System.Collections;

/// <summary>
/// Empty game objects that change the direction of the enemies (only if they're moving in a certain direction),
/// Trigger detection handled in EnemyController
/// </summary>
public class ConditionalTurn : MonoBehaviour {
    public bool reverseDirection;   ///<Direction enemies should turn when the Reverse ability is in effect
    public float requiredDirection;
    public float direction;         ///<Direction enemies should turn
}
