using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour {

    public int TrapDamage = 0;

    void OnTriggerEnter(Collider other){
        if (other.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(TrapDamage);
            Destroy(this);
        }
    }
}