using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    private AudioSource audio;
    /// <summary>
    /// This interfaces with the built in audio class and the volume controller
    /// this wrapper holds the track being currently played 
    /// </summary>

    void Start()
    {
        audio = GetComponent<AudioSource>();
        SetVolume();
    }


    /// <notes>
    /// SetVolume below is an event
    /// </notes>
    void OnEnable()
    {
        VolumeController.MusicVolumeChanged += SetVolume;
    }

    void OnDisable()
    {
        VolumeController.MusicVolumeChanged -= SetVolume;
    }

    private void SetVolume()
    {
        audio.volume = GameController.MusicVolume;
    }
}
