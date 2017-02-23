//by Joshua Rand
using UnityEngine;
using System.Collections;
/// <summary>
/// A bomb for instantly killing enemies on the screen
/// </summary>
public class Bomb : MonoBehaviour {
    public int duration;            ///<Duration in frames for flashing bomb
    public float explosionSize;     ///<Radius of the explosion
    public SpriteRenderer White;    ///<White image for flashing
    public SpriteRenderer Graphic;  ///<Bomb image
    public GameObject Circle;       ///<Explosion circle
    private float timeRemaining;
	// Use this for initialization
	void Start () {
        timeRemaining = 0f;
        StartCoroutine(Run());
	}

    /// <summary>
    /// Coroutine for flashing bomb and then explosion
    /// </summary>
    /// <returns></returns>
    IEnumerator Run()
    {
        float position = 0f;
        //timeRemaining = duration;
        for (float i = 0f; i < duration; i+=GameController.EnemySpeed)
        {
            //float dAlpha = 20f + (Mathf.Pow(i, 2) * 0.75f / duration);
            //position += dAlpha;
            position = (Mathf.Pow(i, 2) * 50f / duration);
            float alpha = 0.75f + 0.25f * Mathf.Cos(position * Mathf.PI / 180f);
            Graphic.color = new Color(1f, 1f, 1f, alpha);
            yield return 0;
        }
        yield return Explode();
    }

    /// <summary>
    /// Bomb explosion, inflicts 2 damage
    /// </summary>
    /// <returns></returns>
    IEnumerator Explode()
    {
        SpriteRenderer[] childrenSr = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < 2; i++)
        {
            childrenSr[i].enabled = false;
        }
        Transform t = Circle.transform;
        SpriteRenderer sr = Circle.GetComponent<SpriteRenderer>();
        for (float i = 0f; i < duration; i+=GameController.EnemySpeed)
        {
            float scale = Mathf.Min(i * 0.8f, explosionSize);
            t.localScale = new Vector3(scale, scale, scale);
            sr.color = new Color(1f, 0f, 0f, 1 - (i / (float)duration));
            yield return 0;
        }

        Destroy(gameObject);
    }
}
