using UnityEngine;
using System.Collections;

/// <summary>
/// Phoenix Tower
/// </summary>
public class Phoenix : MonoBehaviour {
    public GameObject gargoyle;
    public GameObject phoenix;
    int i = 0;
    public Animator anim;
    bool usable;
    EnemyController[] enemies;
    Vector2 startPos;
	// Use this for initialization
	void Start () {
        phoenix.SetActive(false);
        gargoyle.SetActive(true);
        usable = true;
        anim.SetInteger("Speed", 0);
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && usable)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(transform.position, mousePos) <= 0.5f)
            {
                enemies = GameObject.FindObjectsOfType<EnemyController>();
                if (enemies.Length == 0)
                {
                    Debug.Log("No enemies to kill.");
                }
                else
                    StartCoroutine(Run());
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().InstantKill();
            //enemies = GameObject.FindObjectsOfType<EnemyController>();
            i = 0;
        }
    }

    IEnumerator Run()
    {
        phoenix.SetActive(true);
        gargoyle.SetActive(false);
        usable = false;
        enemies = GameObject.FindObjectsOfType<EnemyController>();
        while (enemies.Length > 0)
        {
            i = 0;
            Debug.Log(i);
            Debug.Log(enemies.Length);
            while (enemies[i] == null)
            {
                i++;
                if (i >= enemies.Length)
                {
                    break;
                }
                if (enemies.Length == 0)
                {
                    break;
                }
            }
            if (enemies.Length == 0 || i >= enemies.Length)
            {
                break;
            }
            EnemyController target = enemies[i];
            while (target.currentHealth > 0)
            {
                Vector2 dist = target.gameObject.transform.position - transform.position;
                if (dist.x > 0)
                {
                    anim.SetInteger("Speed", 1);
                }
                else if (dist.x < 0)
                {
                    anim.SetInteger("Speed", -1);
                }
                else
                {
                    anim.SetInteger("Speed", 0);
                }
                dist.Normalize();
                transform.Translate(dist * 0.15f);
                yield return 0;
            }
        }
        while (Vector2.Distance(transform.position, startPos) > 0.15f)
        {
            Vector2 dist = startPos - (Vector2)transform.position;
            if (dist.x > 0)
            {
                anim.SetInteger("Speed", 1);
            }
            else if (dist.x < 0)
            {
                anim.SetInteger("Speed", -1);
            }
            else
            {
                anim.SetInteger("Speed", 0);
            }
            dist.Normalize();
            transform.Translate(dist * 0.15f);
            yield return 0;
        }
        phoenix.SetActive(false);
        gargoyle.SetActive(true);
        usable = true;
        anim.SetInteger("Speed", 0);
    }
}
