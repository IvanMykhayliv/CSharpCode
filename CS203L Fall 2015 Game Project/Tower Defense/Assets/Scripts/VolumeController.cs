using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Adjusts the volume of the game sound effects and music,
/// Used on the title screen.
/// </summary>
public class VolumeController : MonoBehaviour {
    public Text VolumeText;
    public Text MusicText;
    public AudioSource sampleSound;                                     ///<Windows "ding"
    public delegate void MusicVolumeChangedHandler();                   ///<Event handler for when the music volume changes
    public static event MusicVolumeChangedHandler MusicVolumeChanged;   ///<Event for when the volume changes

	void Start () {
        UpdateVolume(false);
        UpdateMusic();
	}

    /// <summary>
    /// Update the display for game volume and play Windows ding
    /// </summary>
    /// <param name="playSound"></param>
    private void UpdateVolume(bool playSound)
    {
        if (VolumeText != null)
            VolumeText.text = "Sound Volume\n" + Mathf.RoundToInt(GameController.GameVolume * 100).ToString() + "%";
        if (sampleSound != null)
            sampleSound.volume = GameController.GameVolume;
        if (playSound && sampleSound != null)
            sampleSound.Play();
    }

    /// <summary>
    /// Update the music volume display
    /// </summary>
    private void UpdateMusic()
    {
        if (MusicText != null)
            MusicText.text = "Music Volume\n" + Mathf.RoundToInt(GameController.MusicVolume * 100).ToString() + "%";
    }

    /// <summary>
    /// Increase game volume, called from a button
    /// </summary>
    public void IncreaseVolume()
    {
        if (GameController.GameVolume < 1.0f)
        {
            GameController.GameVolume += 0.05f;
            UpdateVolume(true);
        }
    }

    /// <summary>
    /// Decrease game volume, called from a button
    /// </summary>
    public void DecreaseVolume()
    {
        if (GameController.GameVolume > 0.0f)
        {
            GameController.GameVolume -= 0.05f;
            UpdateVolume(true);
        }
    }

    /// <summary>
    /// Increase game music volume, called from a button
    /// </summary>
    public void IncreaseMusic()
    {
        if (GameController.MusicVolume < 1.0f)
        {
            GameController.MusicVolume += 0.05f;
            UpdateMusic();
            if (MusicVolumeChanged != null)
            {
                MusicVolumeChanged();
            }
        }
    }

    /// <summary>
    /// Decrease game music volume, called from a button
    /// </summary>
    public void DecreaseMusic()
    {
        if (GameController.MusicVolume > 0.0f)
        {
            GameController.MusicVolume -= 0.05f;
            UpdateMusic();
            if (MusicVolumeChanged != null)
            {
                MusicVolumeChanged();
            }
        }
    }
}