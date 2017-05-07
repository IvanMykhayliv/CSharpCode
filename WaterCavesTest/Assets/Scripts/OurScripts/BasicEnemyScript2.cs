using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript2 : MonoBehaviour 
{

    private GameObject target;
    public int moveSpeed;
    public int rotationSpeed;
    public int maxDistance;
    public string intendedTag;
    public GameObject p;


    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
    }
    // Use this for initialization
    void Start()
    {
        GameObject GO = GameObject.FindGameObjectWithTag(intendedTag);

        //target.transform = GO.transform;
        target = GO;
        //maxDistance = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawLine(target.transform.position, myTransform.position, Color.yellow);

        //Look at target
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation
            (target.transform.position - myTransform.position), rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(target.transform.position, myTransform.position) > maxDistance)
        {
            //Move towards target
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
    }
    void OnTriggerEnter(Collider c)
    {
        //target.
        //intendedTag = "Player";
        if (c.tag == "Player")
        {
            intendedTag = "Player";
            GameObject GO = GameObject.FindGameObjectWithTag(intendedTag);

            target = GO;
        }
    }
}
