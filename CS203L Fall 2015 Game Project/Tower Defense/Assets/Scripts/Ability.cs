using UnityEngine;
using System.Collections;

/// <summary>
/// Event handler for ability actions
/// </summary>
public delegate void ActionHandler();
/// <summary>
/// A parent abstract class that defines behavior for each ability
/// </summary>
public abstract class Ability : MonoBehaviour {
    public string Name;         ///<The name of the ability
    public int cooldownTime;    ///<The duration in seconds of the ability's cooldown period
    public int Quantity;        ///<The number of times the ability can be used
    public bool specialAbility; ///<Indicates if this is a special or a normal ability
    /// <summary>
    /// Used in child ability classes, defines behavior of the ability
    /// </summary>
    public abstract void Use();
}