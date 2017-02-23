using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Allows fast forward buttons to change the game speed,
/// Uses GameController.EnemySpeed
/// </summary>
public class FfwdButton : MonoBehaviour
{
    /// <summary>
    /// Normal game speed
    /// </summary>
    public void OnClick1x()
    {
        if (GameController.EnemySpeed != 1.0f)
            GameController.EnemySpeed = 1.0f;
    }

    /// <summary>
    /// 2x game speed
    /// </summary>
    public void OnClick2x(){
        if (GameController.EnemySpeed != 2.0f)
            GameController.EnemySpeed = 2.0f;
    }

    /// <summary>
    /// 5x speed
    /// </summary>
    public void LUDICROUSSPEED()
    {
        if (GameController.EnemySpeed != 5.0F)
            GameController.EnemySpeed = 5.0F;
    }
}