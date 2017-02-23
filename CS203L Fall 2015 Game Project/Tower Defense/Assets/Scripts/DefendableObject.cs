using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// The defendable object script, the player must protect this object in order to win the level
/// </summary>
public class DefendableObject : MonoBehaviour
{
    public delegate void ObjectDamageHandler(int currentHealth, int maxHealth); ///<Event handler for when the object gains or loses health
    public event ObjectDamageHandler TakeDamage;                                ///<Event for when the health changes
    public event ObjectDamageHandler Died;                                      ///<Event for when the DO loses all health

    public int maximumHealth = 1000;                                            ///<Maximum health
    public int currentHealth;                                                   ///<Starting health

    void Start()
    {
        currentHealth = maximumHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enem = other.gameObject.GetComponent<EnemyController>();
            int damage;
            if (enem == null)
                // if you want regular damage connect EnemyController to your enemy
                damage = 100;
            else
                damage = enem.damage;
            currentHealth -= damage;
            Destroy(other.gameObject);
            //TODO: Get number of damage enemy can deal to defendable object
            //Replace -10

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            if (TakeDamage != null)
                TakeDamage(currentHealth, maximumHealth);
            if (currentHealth == 0 && Died != null)
            {
                Died(currentHealth, maximumHealth);
            }

            if (currentHealth == 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    /// <summary>
    /// Add life to the defendable object and fire the TakeDamage event
    /// </summary>
    /// <param name="medicine"></param>
    public void heal(int medicine)
    {
        /// for the healing ability
        currentHealth += medicine;
        if (currentHealth > maximumHealth)
            currentHealth = maximumHealth;
        if (TakeDamage != null)
        {
            TakeDamage(currentHealth, maximumHealth);
        }
    }

    /// <summary>
    /// Add 1/3 of the max life to the defendable object and fire the TakeDamage event,
    /// Called by the HealDOAbility ability
    /// </summary>
    public void PartialHeal()
    {
        /// for the healing ability
        currentHealth += maximumHealth / 3;
        if (currentHealth > maximumHealth)
            currentHealth = maximumHealth;
        if (TakeDamage != null)
        {
            TakeDamage(currentHealth, maximumHealth);
        }
    }
}