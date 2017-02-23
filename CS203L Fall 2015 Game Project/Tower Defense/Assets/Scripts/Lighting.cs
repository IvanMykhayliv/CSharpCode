/// a script that controls the lighting by Ben Melanson ☺
using UnityEngine;
using System.Collections;

public class Lighting : MonoBehaviour {

    public GameObject Redlight;
    public GameObject MainLight;
    private GameObject defendableObject;
    private DefendableObject DO;
    private Light RL;
    private float mul = 1.0f;

    /// <summary>
    /// assign the defendable object and get the script component
    /// </summary>
	void Start () 
    {
        defendableObject = GameObject.Find("DefendableObject");
        DO = defendableObject.GetComponent<DefendableObject>();
        RL = Redlight.GetComponent<Light>();
	}
	
	/// <summary>
	/// if the health is less than 25% then run the red light flashing sequence.
	/// </summary>
	void Update () 
    {
        if (DO.currentHealth <= (DO.maximumHealth * 0.25f))
        {
            if (MainLight.activeInHierarchy)
            {
                MainLight.SetActive(false);
            }
            Flash();
        }
        else
        {
            if (!MainLight.activeInHierarchy)
            {
                MainLight.SetActive(true);
                RL.intensity = 0;
            }
        }
	}

    /// <summary>
    /// sequence for the red light to go up and down.
    /// </summary>
    void Flash()
    {

        if (RL.intensity == 0)
        {
            mul = 1.0f;
        }
        if (RL.intensity >= 1)
        {
            mul = -1.0f;
        }
        RL.intensity += (0.1f * mul);
    }
}
