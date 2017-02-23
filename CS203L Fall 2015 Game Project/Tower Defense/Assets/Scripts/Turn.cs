using UnityEngine;
using System.Collections;

/// <summary>
/// Empty game objects that change the direction of the enemies,
/// Trigger detection handled in EnemyController
/// </summary>
public class Turn : MonoBehaviour {
    public bool reverseDirection;   ///<Direction enemies should turn when the Reverse ability is in effect
    public float direction;         ///<Direction enemies should turn
}
