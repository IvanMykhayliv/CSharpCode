using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Sets the game difficulty on the title screen,
/// Uses static field GameController.Difficulty
/// </summary>
public class DifficultyManager : MonoBehaviour {
    public string[] difficultyNames;    ///<Easy, Normal, Hard, Donald Trump
    public Color[] difficultyColor;     ///<Cyan, Green, Red, Dark Red
    public int minDifficulty = 0;
    public int maxDifficulty = 3;
    public Text DifficultyText;

	void Start () {
        UpdateText();
	}

    /// <summary>
    /// Update the difficulty display when the buttons are pressed
    /// </summary>
    private void UpdateText()
    {
        int index = GameController.Difficulty;
        DifficultyText.text = difficultyNames[index];
        DifficultyText.color = difficultyColor[index];
    }

    /// <summary>
    /// Increase the difficulty and update the text display
    /// </summary>
    public void IncreaseDifficulty()
    {
        if (GameController.Difficulty < maxDifficulty)
        {
            GameController.Difficulty++;
            UpdateText();
        }
    }

    /// <summary>
    /// Decrease the difficulty and update the text display
    /// </summary>
    public void DecreaseDifficulty()
    {
        if (GameController.Difficulty > minDifficulty)
        {
            GameController.Difficulty--;
            UpdateText();
        }
    }
}
