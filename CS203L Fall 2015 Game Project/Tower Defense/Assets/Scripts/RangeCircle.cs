///a script for our range circle. set to grow and appear when the tower levels up and are clicked
using UnityEngine;
using System;
using System.Collections;

public class RangeCircle : MonoBehaviour {
    public bool UseButtons;
    public static event Action<GameObject, bool> TowerSelected;
    public static GameObject selectedTower;
    public Vector3 Scale {
        get
        {
            return scale;
        }
        set 
        {
            scale = value;
            transform.localScale = value * ratio;
        }
    }
    private Vector3 scale;
    public CircleCollider2D circleCollider;
    SpriteRenderer spr;
    float ratio = 5.5f / 1.75f;
	// Use this for initialization
	void Start () {
        spr = GetComponent<SpriteRenderer>();
        spr.enabled = false;
        if (circleCollider != null)
        {
            Scale = Vector3.one * circleCollider.radius;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            bool mouseHover = Vector3.Distance(transform.position, mousePos) < 0.5f;
            if (mouseHover)
            {
                if (TowerSelected != null && selectedTower != gameObject)
                {
                    selectedTower = gameObject;
                    TowerSelected(gameObject, UseButtons);
                }
                spr.enabled = true;
            }
            else
            {
                if (TowerSelected != null && selectedTower == gameObject)
                {
                    selectedTower = null;
                    TowerSelected(null, UseButtons);
                }
                spr.enabled = false;
            }
        }
	}
}
