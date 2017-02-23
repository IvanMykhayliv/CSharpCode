using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Displays the defendable object's lifebar
/// </summary>
public class UI : MonoBehaviour {

    public Texture2D lifebarSprite;
    public DefendableObject defendableObject;   ///<Reference to the level's defendable object
    public float maxWidth = 100f;               ///<Maximum allowed width of the lifebar
    private float currentWidth;                 ///<Current width of the lifebar

    void OnEnable()
    {
        if (defendableObject != null)
        {
            defendableObject.TakeDamage += UpdateLifebar;
            defendableObject.Died += OnDefendableObjectDied;
        }
    }

    void OnDisable()
    {
        if (defendableObject != null)
        {
            defendableObject.TakeDamage -= UpdateLifebar;
            defendableObject.Died -= OnDefendableObjectDied;
        }
    }

	void Start () {
        currentWidth = maxWidth;
	}

    /// <summary>
    /// Draw the defendable object lifebar
    /// </summary>
    void OnGUI()
    {
        if (lifebarSprite != null)
        {
            GUI.DrawTexture(new Rect(10, 10, currentWidth, 50), lifebarSprite);
        }
    }

    /// <summary>
    /// Load the game over screen when the defendable object runs out of health
    /// </summary>
    /// <param name="currentHealth"></param>
    /// <param name="maxHealth"></param>
    void OnDefendableObjectDied(int currentHealth, int maxHealth)
    {
        GameController.GameOver = true;
        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
    /// Update the scale of the lifebar as the DO takes damage
    /// </summary>
    /// <param name="currentHealth"></param>
    /// <param name="maxHealth"></param>
    void UpdateLifebar(int currentHealth, int maxHealth)
    {
        currentWidth = Mathf.Max(maxWidth * ((float)currentHealth / (float)maxHealth), 0f);
    }
}
