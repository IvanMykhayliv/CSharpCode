/// A script that allows the tower to be moved. 
/// This script also handles where the button shows up and when it shows up
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveTowerButton : MonoBehaviour {
    public Text text;
    public ColorBlock cancelColor;
    private ColorBlock initColor;
    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        initColor = GetComponent<Button>().colors;
    }

    /// <summary>
    /// sets the tower that is being picked up to true
    /// </summary>
    void OnEnable()
    {
        TowerScript.TowerPickedUp += OnTowerPickedUp;
        RangeCircle.TowerSelected += OnTowerSelected;
    }

    /// <summary>
    /// sets the tower being placed to false
    /// </summary>
    void OnDisable()
    {
        TowerScript.TowerPickedUp -= OnTowerPickedUp;
        RangeCircle.TowerSelected -= OnTowerSelected;
    }

    /// <summary>
    /// moves the button off screen and takes the money from the account.
    /// </summary>
    void OnTowerPickedUp()
    {
        rt.position = new Vector3(-1000f, -1000f, 0f);
        Currency2.AddCurrency(-2);
        ColorBlock cb = GetComponent<Button>().colors;
        GetComponent<Button>().colors = initColor;
        text.text = "Move Tower";
    }

    /// <summary>
    /// shows the button by the tower when selected
    /// </summary>
    /// <param name="obj">The Tower</param>
    /// <param name="moveTowerButton"></param>
    void OnTowerSelected(GameObject obj, bool moveTowerButton)
    {
        if (obj != null)
        {
            if (moveTowerButton)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(obj.transform.position) + new Vector3(32f, -25f, 0f);
                rt.position = pos;
            }
        }
        else
        {
            rt.position = new Vector3(-1000f, -1000f, 0f);

            TowerScript.placeAgain = false;
            ColorBlock cb = GetComponent<Button>().colors;
            GetComponent<Button>().colors = initColor;
            text.text = "Move";
        }
    }

    /// <summary>
    /// handles picking up and placing the tower
    /// </summary>
    public void Click()
    {
        if (!TowerScript.placeAgain)
        {
            if (Currency2.currency >= 2)
            {
                TowerScript.placeAgain = true;
                ColorBlock cb = GetComponent<Button>().colors;
                GetComponent<Button>().colors = cancelColor;
                text.text = "...";
            }
        }
        else
        {
            TowerScript.placeAgain = false;
            ColorBlock cb = GetComponent<Button>().colors;
            GetComponent<Button>().colors = initColor;
            text.text = "Move";
        }
    }
}
