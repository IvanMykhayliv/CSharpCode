using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Money;

/// <summary>
/// Display the number of chicken tendies and good boy points on the screen by using UI Text
/// </summary>
public class CurrencyText : MonoBehaviour {
    
    public Text text;///<the text that we update
    
    /// <summary>
    /// updates the text to the latest 
    /// </summary>
    void Start()
    {
        UpdateText(Currency2.currency, Currency2.gbpoints);
    }

    void OnEnable()
    {
        Currency2.CurrencyChanged += UpdateText;
        Currency2.GbpointsChanged += UpdateText;
    }

    void OnDisable()
    {
        Currency2.CurrencyChanged -= UpdateText;
        Currency2.GbpointsChanged -= UpdateText;
    }

    /// <summary>
    /// Update the chicken tendies and good boy points amounts on the screen
    /// </summary>
    /// <param name="currency">tendies</param>
    /// <param name="gbp">good boy points</param>
    void UpdateText(int currency, int gbp){
        text.text = "Chicken Tendies:\t" + currency + "\nGood Boy Points:\t" + gbp + "\n Current Wave: \t" + EnemyGenerator.Waves;
    }
}
