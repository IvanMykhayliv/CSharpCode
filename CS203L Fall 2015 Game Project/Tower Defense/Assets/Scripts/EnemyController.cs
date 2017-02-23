using UnityEngine;
using System;
using System.Collections;
using Money;

/// <summary>
/// allows for different types of enemies
/// </summary>
public enum EnemyType
{
    Basic = 0,
    Split,
    Boss
}

/// <summary>
/// Class for controlling the enemies
/// </summary>
public class EnemyController : MonoBehaviour
{
    public delegate void OnTakeDamageHandler(GameObject enemy, int newHealth);
    public event OnTakeDamageHandler OnTakeDamage;
    public delegate void OnEnemyDestroyedHandler(GameObject enemy);
    public static event OnEnemyDestroyedHandler OnEnemyDestroyed;
    public static event Action<GameObject> OnEnemyCreated;

    /// <summary>
    /// The Enemy's look and collider
    /// </summary>
    public GameObject smallEnemyPrefab; 
    public GameObject MicahDeathParticlesPrefab;
    public GameObject cherryParticlesPrefab;
    
    public EnemyType enemyType;
    public float maxHealth = 1;
    public double currentHealth;
    public int damage = 1;///<how much damage is done to the defendable object when it is hit.
    public float speed = 1;///<the speed of the enemy
    public int money = 1;///<how many tendies the enemy gives when it is killed
    private Transform lifebarSprite;
    public float maxWidth = 40;
    public float direction = 0f;
    public double frozenTime = 0;
    public bool frozen = false;///<bool for telling the enemy to stop for effects
    private double freezeTimer;
    private int defaultSpeed = 2; ///< I had the enemy speed multiplied by a constant upon creation, I moved it up here to make it more visable

    private double burnedTimer;///<Timer for how long the enemy is affected by the burning sensation
    private double burnedDamage;///<how much damage to do over time

    private float freezeResistance = 30; ///< controls how rapidly faster enemies break out, bigger number causes faster enemies break out slower
    private bool reverseDir;///<controls if the enemy is going backwards or forwards
    private GameObject cherryParticles;
    private SpriteRenderer sr;

    public bool tunneled;
    public delegate void OnTunneledHendler(GameObject enemy);
    public static event OnTunneledHendler UnderGround;
    public delegate void OnExumedHendler(GameObject enemy);
    public static event OnExumedHendler AboveGround;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        reverseDir = false;
        lifebarSprite = GetComponentInChildren<Transform>();
        currentHealth = maxHealth;
        freezeResistance -= GameController.Difficulty;
        if (enemyType == EnemyType.Boss)
        {
            cherryParticles = Instantiate(cherryParticlesPrefab, new Vector3(-7.4f, 7.6f, 0f), Quaternion.Euler(0f, 0f, 225f)) as GameObject;
        }
    }

    void OnEnable()
    {
        ReverseAbility.ReverseEnemies += Reverse;
        NinjaMonkeyAbility.KillAllEnemies += InstantKill;
    }

    void OnDisable()
    {
        ReverseAbility.ReverseEnemies -= Reverse;
        NinjaMonkeyAbility.KillAllEnemies -= InstantKill;
    }

    void Reverse()
    {
        if (enemyType != EnemyType.Boss)
        {
            direction += 180f;
            reverseDir = true;
        }
    }

    /// <summary>
    /// Kill the enemy instantly
    /// </summary>
    public void InstantKill()
    {
        if (enemyType != EnemyType.Boss)
        {
            TakeDamage((int)currentHealth);
        }
    }

    /// <summary>
    /// take the enemies health down to one
    /// </summary>
    public void Sabotage()
    {
        if (enemyType != EnemyType.Boss)
        {
            TakeDamage((int)(currentHealth - 1));
        }
    }

    void Update()
    {
        ///TODO: change to coroutine
        if (frozen)
        {
            Halt();
            freezeTimer -= Time.deltaTime * GameController.EnemySpeed;
            if (freezeTimer <= 0)
            {
                frozen = false;
            }
        }
        else          
        {
            Move();
            frozenTime += Time.deltaTime * speed;
        }
        if (burnedTimer > 0)
        {
            // is the enemy on fire
            double duration = Time.deltaTime;
            if (burnedTimer < duration)
                duration = burnedTimer;
            burnedTimer -= duration;
            TakeDamage(duration * burnedDamage);
        }
    }

    /// <summary>
    /// freezes the enemy in place
    /// </summary>
    void Halt()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
    }
    /// <summary>
    /// makes the enemy move after being frozen
    /// </summary>
    void Move()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float xspeed = Mathf.Cos(direction * Mathf.PI / 180f) * speed * GameController.EnemySpeed;
        float yspeed = Mathf.Sin(direction * Mathf.PI / 180f) * speed * GameController.EnemySpeed;
        rb.velocity = new Vector2(xspeed, yspeed);
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

        else if (col.gameObject.tag == "Turn")
        {
            Turn turn = col.gameObject.GetComponent<Turn>();
            float turnDirection = turn.direction;
            if (turn.reverseDirection == reverseDir)
            {
                direction = turnDirection;
            }
        }
        else if (col.gameObject.tag == "ConditionalTurn")
        {
            ConditionalTurn turn = col.gameObject.GetComponent<ConditionalTurn>();
            float turnDirection = turn.direction;
            float requiredDir = turn.requiredDirection;
            if (turn.reverseDirection == reverseDir && direction == turn.requiredDirection)
            {
                direction = turnDirection;
            }
        }
        else if (col.gameObject.tag == "TurnJunction")
        {
            TurnJunction turn = col.gameObject.GetComponent<TurnJunction>();
            float turnDirection = turn.currentDirection;
            if (turn.reverseDirection == reverseDir)
            {
                direction = turnDirection;
            }
            turn.Switch();
        }
        else if (col.gameObject.tag == "Bullet")
        {
            TakeDamage(col.gameObject.GetComponent<ProjectileScript>().AttackPower);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "TunnelEntranceExit")
        {
            TunnelEntranceExit tunnel = col.gameObject.GetComponent<TunnelEntranceExit>();
            if (tunnel.tunnelType == TunnelType.Entrance)
            {
                if (tunnel.reverseDirection)
                {
                    sr.enabled = true;
                }
                else
                {
                    sr.enabled = false;
                }
            }
            else
            {
                if (tunnel.reverseDirection)
                {
                    sr.enabled = false;
                }
                else
                {
                    sr.enabled = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// a second freeze funstion
    /// </summary>
    /// <param name="time">the amount of time in seconds</param>
    public void Freeze(float time)
    {
        ///<time is seconds frozen
        freezeTimer = frozenTime * ((freezeResistance + defaultSpeed) / (freezeResistance + speed));
        frozen = true;
    }

    public void Burn(float time, double ignition)
    {
        ///< starts the bern
        TakeDamage(ignition);
        if (burnedDamage < ignition / 2)
            burnedDamage = ignition / 2;
        if (burnedTimer < time)
            burnedTimer = time;
    }

    /// <summary>
    /// handels when the enemy is underground
    /// </summary>
    public void Tunnel()
    {
        if(tunneled == false){
            if (UnderGround != null)
            {
                UnderGround(gameObject);
            }
            tunneled = true;
        }
    }

    /// <summary>
    /// handels when the enemy is resurfaced
    /// </summary>
    public void exume()
    {
        if (tunneled == true)
        {
            if (AboveGround != null)
            {
                AboveGround(gameObject);
            }
            tunneled = false;
        }
    }

    /// <summary>
    /// getting the damage applied to the enemy
    /// </summary>
    /// <param name="injury">the damage the enemy takes</param>
    public void TakeDamage(double injury)
    {
        if (enemyType == EnemyType.Boss)
        {
            Currency2.AddCurrency((int)injury);
        }
        if (!tunneled)
        {
            currentHealth -= injury;
            if (OnTakeDamage != null)
            {
                OnTakeDamage(gameObject, (int)currentHealth);
            }
        }
        // float xscale = (float)currentHealth / (float)maxHealth;
        // Debug.Log(xscale);
        // lifebarSprite.localScale = new Vector3(xscale, 1f, 1f);

        if (currentHealth <= 0)
        {
            if (OnEnemyDestroyed != null)
            {
                if (enemyType == EnemyType.Split)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        GameObject smallEnemy = Instantiate(smallEnemyPrefab, transform.position + new Vector3((0.5f - i) * Mathf.Cos(direction * Mathf.PI / 180f), (0.5f - i) * Mathf.Sin(direction * Mathf.PI / 180f), 0f), Quaternion.identity) as GameObject;
                        smallEnemy.GetComponent<EnemyController>().direction = direction;
                        if (OnEnemyCreated != null)
                        {
                            OnEnemyCreated(smallEnemy);
                        }
                    }
                }
                else if (enemyType == EnemyType.Boss)
                {
                    Instantiate(MicahDeathParticlesPrefab, transform.position, Quaternion.Euler(270f, 0f, 0f));
                    Destroy(cherryParticles);
                }
                OnEnemyDestroyed(gameObject);
                Debug.Log("HI");
            }
            Currency2.AddCurrency(money);//.increaseCurrency(money);
            Destroy(gameObject);
        }
    }

    void OnGUI()
    {
        float currentWidth = maxWidth * ((float)currentHealth / (float)maxHealth);
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
    }

    /// <summary>
    /// attacking the defendable object
    /// </summary>
    /// <returns>returns the damage to the defendable object</returns>
    public double DealDamage()
    {
        Destroy(gameObject);
        return damage;
    }
    
    public void EnemyStats(int level = 1, float haste = .5f, float durability = .5f)
	{
        /// "Precondition Lv >= 1 and the other two are 0 <= x < 1
        /// the doubles will typically be generated from Random class's 
        /// the following but makes sure the above is true
        if (level < 1)
        {
            level = 1;
        }
        if (haste < 0)
        {
            haste *= -1;
        }
        if (durability <= 0)
        {
            durability *= -1;
        }
        while (haste > 1)
        {
            haste /= 2.0f;
        }
        while (durability > 1)
        {
            durability /= 2.0f;
        }
        
        /// actual code
        /// the thinking about this code is that an enemy's dangerousness is it's damage times it's 
        money = (int)Mathf.Pow(level, .5F);
        speed = haste + .5f;
        maxHealth = Mathf.Round(level * durability / speed);
        if (maxHealth < 1)
            maxHealth = 1;
        damage = (int)(level / (maxHealth * speed));
        if (damage < 1)
            damage = 1;
        //Difficulty change
        speed *= defaultSpeed * Mathf.Log10(level + defaultSpeed * defaultSpeed) * (GameController.Difficulty + 1);
        maxHealth *= (GameController.Difficulty + 2);
    }

}