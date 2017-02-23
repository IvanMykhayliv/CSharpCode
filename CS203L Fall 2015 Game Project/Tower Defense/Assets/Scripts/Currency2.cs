/// A class for our currency system by Ivan Mykhayliv
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Static class that keeps track of the chicken tendies and good boy points used for buying abilities
/// </summary>
public class Currency2 : MonoBehaviour 
{
	
    //Fire an event whenever the currency number changes
    //So that other objects know when the currency changes
    public delegate void CurrencyChangeHandler(int currentCurrency, int gbpoints);  ///<Event handler for changing currency/gbp
    public static event CurrencyChangeHandler CurrencyChanged;                      ///<Called when the currency changes
    public delegate void GbpointsChangeHandler(int currentgbpoints, int gbpoints);  ///<Event handler for changing currency/gbp
    public static event GbpointsChangeHandler GbpointsChanged;                      ///<Called when the amount of good boy points changes
    public Text moneyText;                                                          ///<Chicken tendies amount display
    public Text gbText;                                                             ///<Good boy points amount display
    public static bool hasMoney;                                                    ///<Indicate whehter or not money can be spent (not used)
    public static bool hasGbpoints;                                                 ///<Indicate whether or not good boy points can be spent (not used)
    public Text amountText;                                                         ///<Displays the number of items in the inventory (not used)
    public int invAmount;                                                           ///<Number of items in inventory (not used)
    public int invAmountTotal;                                                      ///<Max. number of items allowed in inventory (not used)

    //this class is static so that as many pieces that have this code always reference the same object
    // Defaults to zero to make sure it always works.
    public static int currency = 0;                                                 ///<The number of chicken tendies the player has
    public static int gbpoints = 0;                                                 ///<The number of good boy points the player has
    public const int gbPointsCost = 10;                                             ///<The cost of chicken tendies to purchase 1 good boy point

    /// <summary>
    /// sets the default values for the currency
    /// </summary>
    /// <returns></returns>
    IEnumerator InitializeCurrency()
    {
        yield return 0;
        SetCurrency(100, 10);
        hasGbpoints = false;
        hasMoney = true;
    }

    /// <summary>
    /// runs on start
    /// </summary>
    void Start()
    {
        StartCoroutine(InitializeCurrency());
    }

    /// <summary>
    /// updates the on screen text for the currency
    /// </summary>
    void Update()
    {
        if (moneyText != null && amountText != null)
        {
            moneyText.text = "Money: " + currency;
            gbText.text = "Good Boy Points: " + gbpoints;
            amountText.text = "Item Amount: " + invAmount;
        }
    }

    /// <summary>
    /// Calls subroutines to update the amount of tendies and GBP that we have
    /// </summary>
    /// <param name="startingCurrency"></param>
    /// <param name="startingGbpoints"></param>
    public static void SetCurrency(int startingCurrency, int startingGbpoints)
    {
        // requires a value for initilizaition for if we want different maps to start with more or less currency
        currency = startingCurrency;
        gbpoints = startingGbpoints;

        if (CurrencyChanged != null)
        {
            CurrencyChanged(currency, gbpoints);
        }

        if (GbpointsChanged != null)
        {
            GbpointsChanged(currency, gbpoints);
        }
    }

    /// <summary>
    /// Adds tendies to our account. Gets called from outside the script
    /// </summary>
    /// <param name="amount">amount of tendies we want to be added</param>
    public static void AddCurrency(int amount)
    {
        currency += amount;
        if (CurrencyChanged != null)
        {
            CurrencyChanged(currency, gbpoints);
        }
    }

    /// <summary>
    /// Adds GBP to our account. Gets called from outside the script
    /// </summary>
    /// <param name="amount">amount of GBP we want to be added</param>
    public static void AddGbPoints(int amount)
    {
 
        gbpoints += amount;

        if (GbpointsChanged != null)
        {
            GbpointsChanged(currency, gbpoints);
        }
    }

    /// <summary>
    /// returns the amount of money we have at the moment
    /// </summary>
    /// <returns>currency</returns>
    public static int getCurrency()
    {
        // other things might want to know how much money the player has
        return currency;
    }

    /// <summary>
    /// returns the amount of GBP we have at the moment
    /// </summary>
    /// <returns>gbpoints</returns>
    public static int getGbpoints()
    {
        // other things might want to know how much money the player has
        return gbpoints;
    }

    /// <summary>
    /// subtracts the amount of the bought item from our tendies after checking if we can buy it or not.
    /// </summary>
    /// <param name="buyAmount">the amount of the item we are trying to buy</param>
    public void modifyCurrency(int buyAmount)
    {
        //Debug.Log("currency:");
        //Debug.Log(currency);
        // for enemy drops, selling towers, base upgrades, etc.
        if (getCurrency() >= Mathf.Abs(buyAmount) && invAmount < invAmountTotal)
            hasMoney = true;
        else
            hasMoney = false;

        if (hasMoney)
        { 
            currency += buyAmount;
            invAmount += 1;
        }
        if (CurrencyChanged != null)
        {
            CurrencyChanged(currency, gbpoints);
        }
        //Debug.Log(currency);
    }

    /// <summary>
    /// subtracts the amount of the bought item from our GBP after checking if we can buy it or not.
    /// </summary>
    /// <param name="buyAmount">the amount of the item we are trying to buy</param>
    public void modifygbpoints(int buyAmount)
    {
        //Debug.Log("gbpoints:");
        //Debug.Log(gbpoints);
        // for enemy drops, selling towers, base upgrades, etc.
        if (getGbpoints() >= Mathf.Abs(buyAmount) && invAmount < invAmountTotal)
            hasGbpoints = true;
        else
            hasGbpoints = false;

        if (hasGbpoints)
        {
            gbpoints += buyAmount;
            invAmount += 1;
        }
        if (GbpointsChanged != null)
        {
            GbpointsChanged(currency, gbpoints);
        }
        // Debug.Log(gbpoints);
    }
}