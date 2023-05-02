using UnityEngine;

public class EngineSetter : MonoBehaviour
{
    //Used to force 60 UPS/FPS in the game.
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}