using UnityEngine;
using System.Collections;

/// <summary>
/// Bomb Explosion circle, inflicts 2 damage on enemies
/// </summary>
public class BombCircle : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D col)
    {
        if (GetComponent<SpriteRenderer>().enabled && col.gameObject.tag == "Enemy")
        {
            col.gameObject.SendMessage("TakeDamage", 2);
        }
    }
}
