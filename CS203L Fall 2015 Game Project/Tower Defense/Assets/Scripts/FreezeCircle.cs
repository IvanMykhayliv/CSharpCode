﻿using UnityEngine;
using System.Collections;

/// <summary>
/// The freeze circle generated by My Ex, Elsa
/// </summary>
public class FreezeCircle : MonoBehaviour {
    public bool Enable;     ///<Allow the freeze circle to be used
	// Use this for initialization
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && Enable)
        {
            other.SendMessage("Freeze", 0f);
        }
    }
}
