using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Outdated bomb script
/// </summary>
[System.Obsolete("This class is obsolete. Use Bomb instead.", true)]
public class BombScript : MonoBehaviour {
    public float Seconds;
    private float sec;
    public GameObject explosion;
    bool boom = false;
    private List<GameObject> Enemies = new List<GameObject>();

	// Use this for initialization
	void Awake() {
        sec = 0;
	}
	
	// Update is called once per frame
	void Update () {
        sec = sec + Time.deltaTime;

        if (sec >= Seconds){
            explosion.SetActive(true);
            //explosion.GetComponent<Animator>();
            StartCoroutine(Death());
        }
	}

    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Enemy"){
            //Enemies.Add(col.gameObject);
            Destroy(col);
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.tag == "Enemy"){
            Destroy(col);
            Enemies.Remove(col.gameObject);
        }
    }
    private IEnumerator Death(){
        for (int i = 0; i < Enemies.Count; i++){
            //Destroy(Enemies<i>);
        }
        Enemies.Clear();
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
