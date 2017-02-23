using UnityEngine;
using System.Collections;

/// <summary>
/// Button for each available tower in the tower store
/// </summary>
public class TowerStoreButton : MonoBehaviour {
    public SpriteRenderer circleSprite;         ///<Indicates the range of the tower
    public float circleScale;                   ///<Scale the size of the circle to the tower's circle trigger
    public int cost;                            ///<The cost of the corresponding tower
    public GameObject towerStorePanel;          ///<Reference to the tower store drop-down panel
    public GameObject towerPrefab;              ///<The prefab of the button's corresponding tower
    public GameObject self;                     ///<Prefab for creating a duplicate button after being dragged off the menu
    public Sprite sprite;                       ///<Tower sprite
    SpriteRenderer spr;                         ///<Tower sprite's renderer, dim the sprite when the tower cannot be bought
    bool dragged;                               ///<Indicate if the tower is currently being dragged off the menu
    Vector3 pos;                                ///<Starting position of the button in the scene

	void Start () {
        dragged = false;
        spr = GetComponent<SpriteRenderer>();
        circleSprite.GetComponent<RangeCircle>().Scale = Vector3.one * circleScale;
        circleSprite.enabled = false;
        spr.sprite = sprite;
        CheckCurrency(Currency2.getCurrency(), 0);
	}

    void OnEnable()
    {
        Currency2.CurrencyChanged += CheckCurrency;
    }

    void OnDisable()
    {
        Currency2.CurrencyChanged -= CheckCurrency;
    }

    /// <summary>
    /// Check if the tower is able to be bought,
    /// If it cannot be bought, dim the sprite,
    /// Called by the Currency2.CurrencyChanged event
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="gbpoints"></param>
    void CheckCurrency(int currency, int gbpoints)
    {
        if (currency < cost)
        {
            spr.color = Color.white * 0.5f;
        }
        else
        {
            spr.color = Color.white;
        }
    }
	
	void Update () {
        Vector3 canvasPos = Camera.main.ScreenToWorldPoint(towerStorePanel.transform.position) + new Vector3(0f, -1f, 0f);
        //Debug.Log(canvasPos);
        canvasPos.z = 0;
        if (!dragged)
        {
            transform.position = canvasPos;
        }
        //transform.position = new Vector3(0f, 0f, 0f);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool mouseHover = Vector3.Distance(transform.position, mousePos) < 0.5f;
        if (Input.GetMouseButtonDown(0) && Currency2.getCurrency() >= cost)
        {
            if (mouseHover)
            {
                circleSprite.enabled = true;
                dragged = true;
                pos = transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (dragged)
            {
                dragged = false;
                if (Currency2.getCurrency() >= cost)
                {
                    Instantiate(towerPrefab, transform.position, Quaternion.identity);
                    Currency2.AddCurrency(-cost);
                    Instantiate(self, pos, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
        if (dragged)
        {
            transform.position = mousePos;
        }
	}
}
