using UnityEngine;
using System.Collections;

public class FloatingObstacle : MonoBehaviour {

	private float originalYPosition;
	private float originalXPosition;
	private bool movedUp = false;
	private bool movedDown = false;
	private float moveBy = .4f;

	// Use this for initialization
	void Start () {
		originalYPosition = transform.position.y;
		originalXPosition = transform.position.x;

	}

	// Update is called once per frame
	void Update () {

		if(!movedUp)
		{
			Vector2 position = Vector2.Lerp((Vector2)(transform.position), new Vector2(originalXPosition, 2.6f), Time.fixedDeltaTime);
			transform.position = position;
		}
		else if(!movedDown)
		{
			Vector2 position = Vector2.Lerp((Vector2)(transform.position), new Vector2(originalXPosition, 2.0f), Time.fixedDeltaTime);
			transform.position = position;
		}

		if(transform.position.y >= 2.59f)
		{
			movedUp = true;
			movedDown = false;
		}
		else if(transform.position.y <= 2.01f)
		{
			movedDown = true;
			movedUp = false;
		}

	}
}
