using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedEnemyScript : MonoBehaviour 
{
    public GameObject[] waypoints;
    public int num = 0;

    //public float minDist;
    //public float speed;

    public int moveSpeed;
    public int rotSpeed;
    public int minDist;

    public GameObject p;

    public bool bPlayerSpotted = false;
    public bool rand = false;
    public bool go = true;

    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }
	
	/*void Start () 
    {
        
	}*/
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        float dist = Vector3.Distance(myTransform.position, waypoints[num].transform.position);
        //float dist = Vector3.Distance(myTransform.position, target.transform.position);

        if(go)
        {
            if(dist > minDist && !bPlayerSpotted)
            //if (Vector3.Distance(waypoints[num].transform.position, gameObject.transform.position) > maxDist)
            {
                Patrol();
            }
            else
            {
                if(!rand)
                {
                    if(num + 1 == waypoints.Length)
                    {
                        num = 0;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    num = Random.Range(0, waypoints.Length);
                }

                if (bPlayerSpotted)
                {
                    MoveToPlayer();
                }
            }
        }
	}

    public void Patrol()
    {
        Debug.DrawLine(waypoints[num].transform.position, myTransform.position, Color.yellow);

        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation
            (waypoints[num].transform.position - myTransform.position), rotSpeed * Time.deltaTime);

        myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    }

    public void MoveToPlayer()
    {
            Debug.DrawLine(p.transform.position, myTransform.position, Color.yellow);

            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation
                (p.transform.position - myTransform.position), rotSpeed * Time.deltaTime);

            if (Vector3.Distance(p.transform.position, myTransform.position) > 0)
            {
                myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
            }
    }

    void OnTriggerEnter(Collider c)
    {
        //target.
        //intendedTag = "Player";
        if (c.tag == "Player")
        {
            bPlayerSpotted = true;
            //p = GameObject.FindGameObjectWithTag("Player");
            //MoveToPlayer();
        }
    }
}
