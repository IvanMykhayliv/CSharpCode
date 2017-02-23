using UnityEngine;
using System.Collections;

public class BossCrushTrigger : MonoBehaviour
{
	public BossEventController bossController;

	void OnTriggerEnter2D( Collider2D collidedObject )
	{
		if(bossController.currentEvent != BossEventController.bossEvents.fallingToNode)
			return;

		if(collidedObject.tag == "Player" || collidedObject.tag == "Enemy")
			collidedObject.SendMessageUpwards("hitByCrusher");

        if(collidedObject.tag == "GoldEnemy")
            collidedObject.SendMessageUpwards("hitByCrusher");
	}
} 