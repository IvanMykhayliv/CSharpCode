using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUpTowerButton : MonoBehaviour {
    public Text text;
    public Text levelText;
    public Text costText;
    public ColorBlock cancelColor;
    private ColorBlock initColor;
    private GameObject currentTower;
    private Button button;
    RectTransform rt;

    void Start()
    {
        button = GetComponent<Button>();
        currentTower = null;
        rt = GetComponent<RectTransform>();
        initColor = GetComponent<Button>().colors;
    }

    void OnEnable()
    {
        TowerScript.TowerPickedUp += OnTowerPickedUp;
        RangeCircle.TowerSelected += OnTowerSelected;
    }

    void OnDisable()
    {
        TowerScript.TowerPickedUp -= OnTowerPickedUp;
        RangeCircle.TowerSelected -= OnTowerSelected;
    }

    void OnTowerPickedUp()
    {
        rt.position = new Vector3(-1000f, -1000f, 0f);
        Currency2.AddCurrency(-2);
        ColorBlock cb = GetComponent<Button>().colors;
        GetComponent<Button>().colors = initColor;
        text.text = "Move Tower";
    }

    void OnTowerSelected(GameObject obj, bool useButtons)
    {
        if (obj != null)
        {
            if (useButtons)
            {
                currentTower = obj;
                TowerScript ts = currentTower.GetComponentInParent<TowerScript>();
                int levelUpCost = ts.Level < 3 ? ts.Level * 2 : 0;
                levelText.text = "Level: " + ts.Level.ToString();
                costText.text = ts.Level < 3 ? "Cost: " + levelUpCost.ToString() + " tds" : "MAX";
                Vector3 pos = Camera.main.WorldToScreenPoint(obj.transform.position) + new Vector3(32f, 5f, 0f);
                button.interactable = (ts.Level < 3 && Currency2.currency >= levelUpCost);
                rt.position = pos;
            }
        }
        else
        {
            currentTower = null;
            rt.position = new Vector3(-1000f, -1000f, 0f);
            TowerScript.placeAgain = false;
            ColorBlock cb = GetComponent<Button>().colors;
            GetComponent<Button>().colors = initColor;
            text.text = "Level up";
        }
    }

    public void Click()
    {
        //Currently, the maximum level for towers is 3
        if (currentTower == null)
        {
            Debug.Log("Tower is null");
        }
        else
        {
            TowerScript ts = currentTower.GetComponentInParent<TowerScript>();
            //In the future, should use a static class to keep track of tower costs
            int levelUpCost = (ts.Level < 3) ? ts.Level * 2 : 0;
            Currency2.AddCurrency(-levelUpCost);
            ts.LevelUp();
            levelUpCost = (ts.Level < 3) ? ts.Level * 2 : 0;
            levelText.text = "Level: " + ts.Level.ToString();
            costText.text = ts.Level < 3 ? "Cost: " + levelUpCost.ToString() + " tds" : "MAX";
            Debug.Log("Tower leveled up!");
            button.interactable = (ts.Level < 3 && Currency2.currency >= levelUpCost);
        }
        //if (!TowerScript.placeAgain)
        //{
        //    if (Currency2.currency >= 2)
        //    {
        //        TowerScript.placeAgain = true;
        //        ColorBlock cb = GetComponent<Button>().colors;
        //        GetComponent<Button>().colors = cancelColor;
        //        text.text = "...";
        //    }
        //}
        //else
        //{
        //    TowerScript.placeAgain = false;
        //    ColorBlock cb = GetComponent<Button>().colors;
        //    GetComponent<Button>().colors = initColor;
        //    text.text = "Level up";
        //}
    }
}
