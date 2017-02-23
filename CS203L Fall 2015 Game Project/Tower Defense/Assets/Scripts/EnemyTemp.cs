using UnityEngine;
using System.Collections;

/// <summary>
/// Old Enemy Controller script
/// </summary>
[System.Obsolete("This class is obsolete, use EnemyController instead.", true)]
public class EnemyTemp : MonoBehaviour {

    //To be overwritten by Micah
	// Use this for initialization

    public delegate void OnTakeDamageHandler(GameObject enemy, int newHealth);
    public event OnTakeDamageHandler OnTakeDamage;
    public float speed = 3f;
    public float direction = 0f;
    public int maxHealth = 3;
    public float maxWidth = 40;
    public int goldworth = 1;
    private int currentHealth;
    private Transform lifebarSprite;

	void Start () {
        currentHealth = maxHealth;
        lifebarSprite = GetComponentInChildren<Transform>();
	}
	
	// Update is called once per frame
    void Update()
    {
        Move();
	}

    void Move()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float xspeed = Mathf.Cos(direction * Mathf.PI / 180f) * speed * GameController.EnemySpeed;
        float yspeed = Mathf.Sin(direction * Mathf.PI / 180f) * speed * GameController.EnemySpeed;
        rb.velocity = new Vector2(xspeed,yspeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Turn Right")
        {
            direction += 270f;
        }

        else if (col.gameObject.tag == "Turn Left")
        {
            direction += 90f;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        float xscale = (float)currentHealth / (float)maxHealth;
        //Debug.Log(xscale);
        //lifebarSprite.localScale = new Vector3(xscale, 1f, 1f);
        if (OnTakeDamage != null)
        {
            OnTakeDamage(gameObject, currentHealth);
        }
        if (currentHealth <= 0)
        {
            Destroy(this);
            Money.Currency.increaseCurrency(goldworth);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void OnGUI()
    {
        float currentWidth = maxWidth * ((float)currentHealth / (float)maxHealth);
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
    }
}