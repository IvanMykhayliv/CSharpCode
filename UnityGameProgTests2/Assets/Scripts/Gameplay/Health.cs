using UnityEngine;

public class Health : MonoBehaviour
{
    public bool bIsDead { get; set; }
    public float curHealth { get; set; }
    public float maxHealth { get; set; }


    void Start()
    {
        bIsDead = false;

        //Only for testing purposes, JSON parsing will come later
        maxHealth = 100;
        curHealth = maxHealth;
    }

    void Update()
    {
        BoundHealthVals();
    }

    private void BoundHealthVals()
	{
        if (!bIsDead)
        {
            if (curHealth > maxHealth)
            {
                ResetHealth();
            }
            else if (curHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void ResetHealth()
	{
        curHealth = maxHealth;
        bIsDead = false;
    }

    public void Kill()
    {
        curHealth = 0;
        bIsDead = true;
    }
}