using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Pull-down panel at the top of the screen for buying towers
/// </summary>
public class TowerStore : MonoBehaviour {
    public Button button;               ///<Button to pull down or pull up the store panel
    public Image Panel;                 ///<The tower store panel
    bool menuOpen;                      ///<Indicate if the menu is currently open or not
    int duration = 15;                  ///<Time in frames that the menu moves

	void Start () {
        menuOpen = false;
	}

    /// <summary>
    /// Expand or collapse the menu when the button is clicked
    /// </summary>
    public void MoveMenu()
    {
        if (menuOpen)
        {
            button.image.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            menuOpen = false;
            StopCoroutine(ExpandMenu());
            StartCoroutine(CollapseMenu());
        }
        else
        {
            button.image.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 180f);
            menuOpen = true;
            StopCoroutine(CollapseMenu());
            StartCoroutine(ExpandMenu());
        }
    }

    /// <summary>
    /// Expand the menu
    /// </summary>
    /// <returns></returns>
    IEnumerator ExpandMenu()
    {
        Panel.rectTransform.anchoredPosition = new Vector3(0f, 100f, 0f);
        for (int i = 1; i <= duration; i++)
        {
            Panel.rectTransform.anchoredPosition = new Vector3(0f, 100f * (1f - (float)i / duration), 0f);
            yield return 0;
        }
    }

    /// <summary>
    /// Collapse the menu
    /// </summary>
    /// <returns></returns>
    IEnumerator CollapseMenu()
    {
        Panel.rectTransform.anchoredPosition = new Vector3(0f, 0, 0f);
        for (int i = 1; i <= duration; i++)
        {
            Panel.rectTransform.anchoredPosition = new Vector3(0f, 100f * (float)i / duration, 0f);
            yield return 0;
        }
    }
}
