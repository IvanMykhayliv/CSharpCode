using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathCounter : MonoBehaviour {

    public AudioSource source;

    private int counter;
    public int maxVal;
	void Start () 
    {
        counter = 0;
	}

    void FixedUpdate()
    {
        if(counter >= maxVal)
        {
            counter = maxVal;
            source.Play();
            Destroy(this.gameObject);
        }
    }
	
	public void Increment(int amount)
    {
        counter += amount;
    }
}
