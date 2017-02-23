using UnityEngine;
using System.Collections;

/// <summary>
/// Static class that manages the game's difficulty, speed, sound effect and music volume, Win and Game Over conditions
/// </summary>
public static class GameController : object
{
    public static int Difficulty { get; set; }
    public static float EnemySpeed { get; set; }
    public static float GameVolume { get; set; }
    public static float MusicVolume { get; set; }
    public static bool GameOver { get; set; }
    public static bool Win { get; set; }
    
    static GameController()
    {
        Difficulty = 1;
        EnemySpeed = 1.0f;
        GameVolume = 1.0f;
        MusicVolume = 1.0f;
        GameOver = false;
        Win = false;
    }
}