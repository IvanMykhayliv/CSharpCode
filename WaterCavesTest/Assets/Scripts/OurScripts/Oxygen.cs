using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour 
{
    public float oxygen = 100;
    public float oxRate;
    public float healthRate;
    public float maxOx = 100;
    public Text oxDisplayText = null;
    public Text displayText2 = null;
    public string oxTextPrefix = "", oxTextSuffix = " 02";
    public Image oxDisplayBar = null;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	//Update is called once per frame
	void FixedUpdate () 
    {
       oxDisplayText.text = oxTextPrefix + (int) oxygen + oxTextSuffix;

        if(oxygen < 0)
        {
            oxygen = 0;
            GetComponent<Health>().health -= healthRate;
        }
        oxygen -= oxRate;

        if(oxygen > maxOx)
        {
            oxygen = maxOx;
        }
        Vector3 sc = oxDisplayBar.transform.localScale; //unity forces duplicate vector
        sc.x = oxygen/maxOx;
        oxDisplayBar.transform.localScale = sc;
	}

    void Update()
    {
        oxDisplayText.text = oxTextPrefix + (int) oxygen + oxTextSuffix;
    }

    public void ModifyAmount(float oA)
    {
        oxygen += oA;
    }
}
