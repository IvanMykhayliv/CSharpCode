using UnityEngine;
using System.Collections;
using Money;

public class SellTower : MonoBehaviour {

    public int SellAmount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        Debug.Log("hi?");
        if(Input.GetKeyDown("s"))
        {
            Debug.Log("click");
            Destroy(gameObject);
            Currency.increaseCurrency(SellAmount);
        }
    }
}
