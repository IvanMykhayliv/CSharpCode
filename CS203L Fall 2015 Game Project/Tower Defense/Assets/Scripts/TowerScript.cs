///<summary>
///a tower controller class by Ben Melanson, Josh Rand, and Joshua Verner ☺
///</summary
using UnityEngine;
using System.Collections;

/// <summary>
/// enum to get which tower this script is attached to
/// </summary>
public enum TowerType {
    Edd = 0,
    Fred,
    Ted,
    Expresso,
    Fire_Pepe,
    Freeze_Elsa
}
/// <summary>
/// main class for the tower type
/// </summary>
public class TowerScript : MonoBehaviour 
{
    public delegate void TowerEventHandler();
    public static event TowerEventHandler TowerPickedUp;
    public static bool placeAgain;
    public TowerType TowerType;
    //Do not want Unity to serialize this
    public bool EffectUsed { get; set; }
    public GameObject FreezeCircleObject;
    private FreezeCircle freezeCircle;
    public CircleCollider2D rangeCircle;
    public int freezeInterval;
    private bool onPath = false;
    private bool isSelected = false;
    private bool isPlanted = false;
    private bool canBePlanted = true;
    private float nextFire;///<float to tell the tower when it can fire again
    public float rangeDelay = 10f;
    public int Multiplier = 1;///<multiplier to tell the tower how much to multiply it's attack, range, and attak speed by.

    public Transform target;///<the position of the furthermost enemy within range 
    public float rateOfFire = 0.5f;///<works with next fire to tell when the toewr can fire next
    public int attack;///<how powerful the towers attack is
    public int Level = 1;
    public Transform spawn;
    public bool Expressoed = false;///<sets to true when in range of the expresso tower so the benefits can't stack
    public bool snap;
    public Transform snapLocation;

    public GameObject Projectile;///<prefab for the shot graphic
    public Sprite[] LevelSprites;///<array for the different level sprites to give the tower a new look when it levels up
    public AudioSource tower_shoot;
    AudioSource Audio;
    public int burstCount = 0;

   TowerRange TR;

    static TowerScript()
    {
        placeAgain = false;
    }

	/// <summary>
	///sets the tower range to the tower and allows us to track enemies
	/// </summary>
	void Awake () 
    {
        EffectUsed = false;
        TR = GetComponentInChildren<TowerRange>();
        LevelUp();
	}

    void Start()
    {
        if (FreezeCircleObject != null)
        {
            float radius = rangeCircle.radius;
            //Debug.Log(radius);
            FreezeCircleObject.transform.localScale = Vector3.one * radius * 5.5f / 1.75f;
        }
        if (TowerType == TowerType.Freeze_Elsa)
        {
            freezeCircle = FreezeCircleObject.GetComponent<FreezeCircle>();
            freezeCircle.Enable = false;
            StartCoroutine(FreezePulse());
        }
    }

    IEnumerator FreezePulse()
    {
        SpriteRenderer sr = FreezeCircleObject.GetComponent<SpriteRenderer>();
        Color circleColor = sr.color;
        circleColor.a = 0f;
        sr.color = circleColor;
        while (true)
        {
            while (!EnemyGenerator.waveActive) { yield return 0; }
            for (float i = 0; i < freezeInterval - 20; i+= GameController.EnemySpeed)
            {
                yield return 0;
            }
            for (float i = 0; i < 20; i += GameController.EnemySpeed)
            {
                if (i == 0)
                    freezeCircle.Enable = true;
                else
                    freezeCircle.Enable = false;
                circleColor.a = 0.3f * (1f - (float)i / 20);
                sr.color = circleColor;
                yield return 0;
            }
            circleColor.a = 0f;
            sr.color = circleColor;
        }
    }
    /// <summary>
    /// dims the sprite when the effect is used, only used for Ted towers
    /// </summary>
    public void DimSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(0.3f, 0.3f, 0.3f, 1f);
    }
    /// <summary>
    /// undims the sprite... duh
    /// </summary>
    public void UndimSprite()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
    }
    /// <summary>
    /// allows the tower to be moved by selecting
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <param name="dist"></param>
    /// <returns></returns>
    //bool GetDistance(Vector2 pos1, Vector2 pos2, float dist)
    //{
    //    float num1 = Mathf.Pow(pos2.y - pos1.y, 2);
    //    float num2 = Mathf.Pow(pos2.x - pos1.x, 2);
    //    float actualDist = Mathf.Sqrt(num1 + num2);
    //    return (actualDist <= dist);
    //}
	
	/// <summary>
	/// updates the object every frame checks for tower movement and aims at the enemies new position
	/// </summary>
	void Update () {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && placeAgain)
        {
            if (Vector3.Distance(transform.position, mousePos) <= 0.5f)
            {
                if (isSelected)
                {
                    isPlanted = true;
                }
                if (TowerPickedUp != null)
                {
                    TowerPickedUp();
                }
                placeAgain = false;
                isSelected = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSelected = false;
        }

        if (isSelected && !isPlanted)
        {
            if (snap)
            {
                transform.position = new Vector3(snapLocation.position.x, snapLocation.position.y, 0.0f);
            }
            else
            {
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
            }
        }

        if (TowerType != TowerType.Freeze_Elsa)
        {
            if (TR.Enemies.Count > 0)
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + rateOfFire;
                    fire();
                }
            }

            if (TR.Enemies.Count > 0)
            {
                Vector3 TargetPosition = new Vector3(TR.Enemies[0].transform.position.x, TR.Enemies[0].transform.position.y, 0);
                TargetPosition = TargetPosition - transform.position;
                float angle = Mathf.Atan2(TargetPosition.y, TargetPosition.x) * Mathf.Rad2Deg - 180;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        
	}
    /// <summary>
    /// the function for firing at the enemies
    /// </summary>
    void fire()
    {
        GameObject bullet = Instantiate(Projectile, spawn.position, spawn.rotation) as GameObject;
        ProjectileScript pscript = bullet.GetComponent<ProjectileScript>();
        pscript.AttackPower = attack * Multiplier;
        pscript.delay = rangeDelay / Multiplier;

        Audio.Play();

        if(TowerType == TowerType.Edd)
        {
            if (burstCount == 3)
            {
                nextFire = 10;

            }
            else
            {
                Update();
            }

        }
    }

    /// <summary>
    /// multipurpose waiting function
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

        yield break;
    }

    /// <summary>
    /// upgrades the tower when the upgrade is purchased
    /// improves the look
    /// increases attack range and power
    /// decreases the time it takes to fire
    /// </summary>
    public void LevelUp()
    {
        Level++;
        CircleCollider2D rang = TR.gameObject.GetComponent<CircleCollider2D>();

        switch(Level)
        {
            case 1:
                break;

            case 2:
                rateOfFire -= (rateOfFire * 0.3f);
                attack = (attack * 2);
                rang.radius += rang.radius * 0.3f;
                rangeDelay = rangeDelay * 1.3f;
                break;

            case 3:
                rateOfFire -= (rateOfFire * 0.6f);
                attack = (attack * 4);
                rang.radius += rang.radius * 0.6f;
                rangeDelay = rangeDelay * 1.6f;
                break;

        }
        if (this.gameObject.tag == "Expresso")
        {
            Multiplier = (int)(Multiplier * 1.5);
        }
        //this.GetComponent<SpriteRenderer>().sprite = LevelSprites[Level - 1];


    }
}
