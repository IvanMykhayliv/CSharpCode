using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Meant to read various components to understand how they work.
public class ValReader : MonoBehaviour
{
    private RectTransform rTnsfrm;
    private Rect nextRect;
    private RawImage rImg;
    private string logMsg;

    private float curW;
    private float curH;
    private Vector2 vect;

    public float shrinkRate;
    void Start()
    {
        rTnsfrm = GetComponent<RectTransform>();
        rImg = GetComponent<RawImage>();

        curW = rTnsfrm.rect.width;
        curH = rTnsfrm.rect.height;

        vect = new Vector2(curW, curH);

        /*rTnsfrm.rect.Set(0, 1, 2, 3);

        nextRect = rTnsfrm.rect;

        This is more or less how you change the RectTransform's inspector x, y, w, and h values.

        rTnsfrm.anchoredPosition = new Vector2(0, -10);
        rTnsfrm.sizeDelta = new Vector2(3, 30);
        */
    }

    // Update is called once per frame
    void Update()
    {

        ShrinkRect();
        //rTnsfrm.rect.Set(0, 1, 2, 3);

        /*logMsg = string.Format("RectTransform Info is as follows: X: {0}, Y: {1}, W: {2}, H: {3}",
            rTnsfrm.anchoredPosition.x, rTnsfrm.anchoredPosition.y, rTnsfrm.rect.width, rTnsfrm.rect.height);

        string logMsg2 = string.Format("RGBA is as follows: R: {0}, G: {1}, B: {2}, A: {3}",
            rImg.color.r, rImg.color.g, rImg.color.b, rImg.color.a);

        Debug.Log(logMsg);*/
    }

    private void ShrinkRect()
	{
        if (curW >= 0)
        {
            vect = new Vector2(curW, curH);
            rTnsfrm.sizeDelta = vect;

            curW = curW >= 0 ? curW - shrinkRate : -1;
        }
    }
}
