using UnityEngine;
using System.Collections;

public class MicahDeathSequence : MonoBehaviour {

    void Start()
    {
        Destroy(gameObject, 5f);
        StartCoroutine(ScreenShake());
    }

    IEnumerator ScreenShake()
    {
        int duration = 140;
        //Should temporary disable abilities during this sequence?
        float initAmpl = 2f;
        float initGameSpeed = GameController.EnemySpeed;
        GameController.EnemySpeed *= 0.2f;
        Vector3 initCameraPos = Camera.main.transform.position;

        for (int i = 1; i <= duration; i++)
        {
            Vector2 dir = Random.insideUnitCircle;
            float ampl = initAmpl * (1f - (float)i / duration);
            Camera.main.transform.position = initCameraPos + (Vector3)dir * ampl;
            yield return 0;
        }

        Camera.main.transform.position = initCameraPos;
        GameController.EnemySpeed = initGameSpeed;
    }
}
