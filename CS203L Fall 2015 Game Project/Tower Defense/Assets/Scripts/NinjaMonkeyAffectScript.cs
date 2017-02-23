using UnityEngine;
using System.Collections;

public class NinjaMonkeyAffectScript : MonoBehaviour {
    public bool active;
    public Vector2 target = new Vector2(-20, 0);
    public Vector2 position;

	// Use this for initialization
	void Start () {
        transform.position = new Vector2(-20, 0);
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            active = true;
            transform.position = new Vector2(20, 0);
        }
        if (active)
        {
            position = new Vector2(transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(position, target, 5 * Time.deltaTime);
            if (position == target)
            {
                active = false;
            }
        }
	}



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
            Destroy(other.gameObject);
    }
}
