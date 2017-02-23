﻿using UnityEngine;
using System.Collections;

public class PlayerSlash : MonoBehaviour 
{
    public GameObject slasher;
    public KeyCode attackKey;

    private int slashCounter = 0;
    public int slashMax;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(attackKey))
        {
            slashCounter += 1;
            slasher.SetActive(true);

            if(slashCounter > slashMax)
            {
                slashCounter = slashMax;
                slasher.SetActive(false);
            }
        }
        if (Input.GetKeyUp(attackKey))
        {
            slashCounter = 0;
            slasher.SetActive(false);
        }
	}
}
