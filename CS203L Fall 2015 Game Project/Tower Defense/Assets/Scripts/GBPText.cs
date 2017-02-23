using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Money;

[System.Obsolete]
public class GBPText : MonoBehaviour {
    public Text text;

    void Start()
    {
        //text = GetComponent<Text>();
        UpdateText(0, 0);
    }

    void OnEnable()
    {
        Currency2.GbpointsChanged += UpdateText;
    }

    void OnDisable()
    {
        Currency2.GbpointsChanged -= UpdateText;
    }

    void UpdateText(int currency, int currentGbpoints)
    {
        text.text = "Good Boy Points: " + currentGbpoints;
    }
}
