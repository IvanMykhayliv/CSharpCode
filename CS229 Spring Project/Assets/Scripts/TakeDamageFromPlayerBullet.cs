using UnityEngine;
using System.Collections;

public class TakeDamageFromPlayerBullet : MonoBehaviour
{
	public delegate void hitByPlayerBullet();
	public event hitByPlayerBullet hitByBullet;
	
	void OnTriggerEnter2D( Collider2D collidedObject )
	{
        if (collidedObject.tag == "Player Bullet" || collidedObject.tag == "Player Slash")
		{
			if(hitByBullet != null)
				hitByBullet();
		}
	}    
}