///<summary>
///a tower range controller class by Ben Melanson, Josh Rand, and Joshua Verner ☺
///</summary
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Main class
/// </summary>
public class TowerRange : MonoBehaviour {

    public List<GameObject> Enemies;///<list for holding all the enemies within range
    private string thisTag;///<the tag of this game object
    private int multiplier;///<the multipler for the expresso tower
	
    ///<summary>
    ///this is used for setting up the range for use when it is spawned
    /// </summary>
	void Awake () 
    {
	    Enemies = new List<GameObject>();
        thisTag = this.tag;
        Debug.Log(thisTag);
        multiplier = GetComponentInParent<TowerScript>().Multiplier;
	}

    void OnEnable()
    {
        EnemyController.OnEnemyDestroyed += RemoveEn;
        EnemyController.UnderGround += RemoveEn;
        EnemyController.AboveGround += EnemyAppeared;
    }

    void OnDisable()
    {
        EnemyController.OnEnemyDestroyed -= RemoveEn;
        EnemyController.UnderGround -= RemoveEn;
        EnemyController.AboveGround -= EnemyAppeared;
    }

    /// <summary>
    /// get the enemy and puts it in the range if a normal tower
    /// get the tower and apply the multiplier if an expresso tower
    /// </summary>
    /// <param name="col">the collider that enters the range</param>
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("GameObject: " + gameObject.transform.parent.gameObject.name.ToString() +  "  this.Tag== " + this.tag.ToString() + "  other.Tag== " + col.tag.ToString() );
        if ((col.tag == "Enemy") && (thisTag != "Expresso"))
        {
            ///<note>
            /// this was added because if a tunneled enemy eneters a tower range it was added anyways even though the enemy could not be hurt
            ///</note>
            if (col.gameObject.GetComponent<EnemyController>().tunneled == false)
            {
                AddEn(col.gameObject);
            }
        }
        if ((col.tag == "Tower") && (thisTag == "Expresso"))
        {
            Debug.Log("MOM!");
            if (col.gameObject.GetComponent<TowerScript>().Expressoed == false)
            {
                Debug.Log(multiplier);
                col.gameObject.GetComponent<TowerScript>().Multiplier = multiplier;
                col.gameObject.GetComponent<TowerScript>().Expressoed = true;
            }
        }
    }

    /// <summary>
    /// removes the enemy or tower from the range and sets the tower back to normal.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit2D(Collider2D col)
    {
        if ((col.tag == "Enemy") && (thisTag != "Expresso"))
        {
            RemoveEn(col.gameObject);
        }
        else if ((col.tag == "Tower") && (thisTag == "Expresso"))
        {
            col.gameObject.GetComponent<TowerScript>().Multiplier = 1;
            col.gameObject.GetComponent<TowerScript>().Expressoed = false;
        }
    }
    
    /// <summary>
    /// prevents the random adding of enemies out side of range
    /// </summary>
    /// <param name="enemy"></param>
    void EnemyAppeared(GameObject enemy)
    {
        Collider2D thisCollider = transform.GetComponent<Collider2D>();
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
        if (thisCollider.bounds.Intersects(enemyCollider.bounds))
        {
            AddEn(enemy);
        }
    }

    /// <summary>
    /// adds the enemy to the list.
    /// </summary>
    /// <param name="enemy"></param>
    void AddEn(GameObject enemy)
    {
        Enemies.Add(enemy);
    }

    /// <summary>
    /// erases the enemy from the list.
    /// </summary>
    /// <param name="enemy"></param>
    void RemoveEn(GameObject enemy) 
    {
        Enemies.Remove(enemy);
    }
}
