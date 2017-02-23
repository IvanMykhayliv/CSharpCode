using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class NinjaMonkeyAbility : Ability {
    public static event ActionHandler KillAllEnemies;
    private GameObject GlossnerFlagPrefab;
    private GameObject GlossnerEffectPrefab;
    private AbilityManager am;
    private Image White;
    private IEnumerable<TowerScript> NinjaMonkeys;
    private Vector3 CameraStartPos;
    private Vector3 CameraPos;
    private float runDuration = 60f;//80f;
    public NinjaMonkeyAbility()
    {
        specialAbility = false;
        cooldownTime = 10;
        Name = "Glossner";
    }
    public override void Use()
    {
        am = GameObject.FindObjectOfType<AbilityManager>();
        White = am.White;

        //Get a list of all ninja monkeys on the field who haven't used their effect
        NinjaMonkeys = GameObject.FindObjectsOfType<TowerScript>().Where(n => n.TowerType == TowerType.Ted).Where(n => !n.EffectUsed);

        am.JohnCena();
        am.StartACoroutine(ShakeCamera());
        am.StartACoroutine(Run());
    }

    IEnumerator WhiteFlash()
    {
        White.rectTransform.localPosition = Vector3.zero;
        for (int i = 0; i < 20; i++)
        {
            float col = ((19f - i) / 20f) * 0.85f;
            White.color = new Color(1f, 1f, 1f, col);
            yield return 0;
        }
        White.rectTransform.localPosition = new Vector3(5000f, 0f, 0f);
    }

    IEnumerator ShakeCamera()
    {
        float screenShakeOffset = 0f;
        Camera mainCamera = Camera.main;
        CameraStartPos = mainCamera.transform.position;
        for (int i = 0; i < runDuration; i++)
        {
            screenShakeOffset += 0.008f;
            float x = Random.Range(CameraStartPos.x - screenShakeOffset, CameraStartPos.x + screenShakeOffset);
            float y = Random.Range(CameraStartPos.y - screenShakeOffset, CameraStartPos.y + screenShakeOffset);
            float z = Random.Range(CameraStartPos.z - screenShakeOffset, CameraStartPos.z + screenShakeOffset);
            CameraPos = new Vector3(x, y, z);
            mainCamera.transform.position = CameraPos;
            yield return 0;
        }
        CameraPos = CameraStartPos;
        mainCamera.transform.position = CameraStartPos;
    }

    IEnumerator Run()
    {
        //Dim one of the ninja monkeys
        TowerScript first = NinjaMonkeys.First();
        first.EffectUsed = true;
        first.DimSprite();


        float GameSpeed = GameController.EnemySpeed;
        GameController.EnemySpeed = 0.1f;
        GlossnerFlagPrefab = Resources.Load<GameObject>("Flag");
        GameObject GlossnerFlag = MonoBehaviour.Instantiate(GlossnerFlagPrefab, first.gameObject.transform.localPosition, Quaternion.identity) as GameObject;
        GlossnerEffectPrefab = Resources.Load<GameObject>("GlossnerParticle");
        GameObject GlossnerEffect = MonoBehaviour.Instantiate(GlossnerEffectPrefab, first.gameObject.transform.localPosition, Quaternion.Euler(270f, 0f, 0f)) as GameObject;
        GlossnerEffect.GetComponent<Renderer>().sortingLayerName = "Particles";
        MonoBehaviour.Destroy(GlossnerEffect, 4f);
        SpriteRenderer sr = GlossnerFlag.GetComponent<SpriteRenderer>();

        for (int i = 0; i < runDuration; i++)
        {
            float scale = i / runDuration;
            float alpha = 0.75f - 0.75f * i / runDuration;
            GlossnerFlag.transform.localScale = new Vector3(scale, scale, 1f);
            //Attach the flag as a child of the camera to prevent it from shaking
            GlossnerFlag.transform.parent = Camera.main.transform;

            sr.color = new Color(1f, 1f, 1f, alpha);
            yield return 0;
        }
        MonoBehaviour.Destroy(GlossnerFlag);

        if (KillAllEnemies != null)
        {
            KillAllEnemies();
        }
        GameController.EnemySpeed = GameSpeed;
        yield return WhiteFlash();
    }
}
