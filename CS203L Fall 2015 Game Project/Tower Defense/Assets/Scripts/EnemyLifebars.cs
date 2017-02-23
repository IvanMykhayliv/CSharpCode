using UnityEngine;
using System.Collections;

/// <summary>
/// Attaches lifebars onto the enemies as they are spawned
/// </summary>
public class EnemyLifebars : MonoBehaviour {
    public Sprite lifebarSprite;
    public Vector3 lifebarPosition;                                 ///<The lifebar's position relative to it's corresponding enemy
    public Vector3 lifebarScale = new Vector3(1.5f, 0.25f, 1f);

    void OnEnable()
    {
        EnemyGenerator.OnEnemyCreated += OnEnemyCreated;
        EnemyController.OnEnemyCreated += OnEnemyCreated;
    }

    void OnDisable()
    {
        EnemyGenerator.OnEnemyCreated -= OnEnemyCreated;
        EnemyController.OnEnemyCreated -= OnEnemyCreated;
    }

    /// <summary>
    /// Method called when the lifebar's corresponding enemy takes damage
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="number"></param>
    void OnEnemyTakeDamage(GameObject enemy, int number)
    {
        Debug.Log("Enemy take damage");
        Transform lifebar = enemy.transform.GetChild(0);
        //At this point, every enemy should have a lifebar
        if (lifebar != null)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
			if (enemyController == null)
            {
				enemyController.OnTakeDamage -= OnEnemyTakeDamage;
            }
            float xscale = (float)enemyController.currentHealth / enemyController.maxHealth;
            lifebar.localScale = new Vector3(lifebarScale.x * xscale, lifebarScale.y, lifebarScale.z);
        }
        else
        {
            Debug.Log("Error");
        }
    }

    /// <summary>
    /// Attach the lifebar to the enemy
    /// </summary>
    /// <param name="enemy"></param>
    void OnEnemyCreated(GameObject enemy)
    {
        GameObject lifebar = new GameObject("lifebar");
        lifebar.transform.localScale = new Vector3(lifebarScale.x * enemy.transform.localScale.x, lifebarScale.y * enemy.transform.localScale.y, 1f);
        SpriteRenderer spriteRenderer = lifebar.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = lifebarSprite;
        spriteRenderer.sortingLayerName = "Lifebar";
        lifebar.transform.localPosition = enemy.transform.localPosition + lifebarPosition;
        lifebar.transform.parent = enemy.transform;
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
            enemyController.OnTakeDamage += OnEnemyTakeDamage;
        else
        {
            Debug.Log("Error! Missing EnemyTemp script");
        }
    }
}