using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowLocation : MonoBehaviour {

    public int NumberofLocations;
    bool locationCalled;

    GameObject[] location;
    GameObject button;
   
	// Use this for initialization
	void Start () 
    {
        locationCalled = false;
        button = GameObject.Find("ShowTowerLocation");
        location = GameObject.FindGameObjectsWithTag("BestLocation");

        for (int x = 0; x < NumberofLocations; x++)
        {
            location[x].SetActive(false);
        }
            
        //if (GameController.Difficulty != 0)
        //{
        //    button.SetActive(false);
        //}
	}
	
	// Update is called once per frame
	void Update () 
    {
       
	}

    public void OnClick()
    {
        switch (locationCalled)
        {
            case true:
                {
                    Debug.Log("true called");

                    //location.SetActive(false);
                    for (int x = 0; x < NumberofLocations; x++)
                    {
                        location[x].SetActive(false);
                    }
                    locationCalled = false;
                    break;
                }
            case false:
                {
                    Debug.Log("false called");

                    //location.SetActive(true);
                    for (int x = 0; x < NumberofLocations; x++)
                    {
                        location[x].SetActive(true);
                    }
                    locationCalled = true;
                    break;
                }
        }
    }
}
