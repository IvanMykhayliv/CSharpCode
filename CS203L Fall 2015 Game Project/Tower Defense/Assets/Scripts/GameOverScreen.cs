using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Game Over screen displayed after the defendable object loses all health
/// </summary>
public class GameOverScreen : MonoBehaviour {
    public Text GameOverText;

	void Start () {
        StartCoroutine(YeahDelay());
	}

    /// <summary>
    /// Wait for the "Yeah" audio clip to play before displaying the text,
    /// Game text starts out as just "Game Over";
    /// </summary>
    /// <returns></returns>
    IEnumerator YeahDelay()
    {
        yield return new WaitForSeconds(1.3f);
        GameOverText.text += "\nYEEEEEAAAAH!!!";
    }
}
