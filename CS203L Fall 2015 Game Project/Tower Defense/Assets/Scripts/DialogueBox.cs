using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

/// <summary>
/// Dialogue box and cutscene seen in the beginning and end of the game
/// </summary>
public class DialogueBox : MonoBehaviour {
    public Image back;                      ///<Gray background during Micah cutscene (for color adjustment purposes)
    public Image scene;                     ///<Micah scene image (for color adjustment purposes)
    public bool screenShake;                ///<Indicate whether or not the screen will shake in this cutscene
    public AudioSource deathSound1;         ///<Played during Post-Micah battle cutscene
    public Text text;                       ///<Dialogue box text
    public Text tip;                        ///<Displays "Press [Space] to advance" in the first cutscene
    public Image box;                       ///<Black dialogue box
    public AudioSource beep;                ///<Text sound effect
    public string[] dialogue;               ///<Array of strings to store all the dialogue for the cutscene
    public Image[] sceneImages;
    public Image black;                     ///<Fade to black image
    public string firstLevelScene;          ///<Scene to load after the cutscene
    private const float charDelay = 0.03f;  ///<Character display interval, used to be 0.04f
    private bool spaceDown;                 ///<Indicate that the space bar has been pressed
    private bool spaceEnable;               ///<Allow the space bar to be pressed (key is locked for 1 frame to avoid skipping multiple dialogues)
    private bool inDialogue;                ///<Indicate whether or not the dialogue is active
    private bool dialogueEnd;               ///<Indicate whether or not at the end of a dialogue box
    private bool dialogueNext;              ///<Indicate whether or not to advance to the next dialogue
    private Color tipColor;
    private float boxMaxAlpha = 0.7f;
    private int fadeTime = 30;              ///<Duration of fade between cutscene images
    private int slowFadeTime = 90;          ///<Duration of fade after all dialogue at end of cutscene
    private int sceneIndex;                 ///<Indicate which scene image is currently active
    private bool whiteFlash;                ///<Indicate whether or not to fade to white
    private bool control;                   ///<Allow the player control of the dialogue box

	void Start () {
        sceneIndex = 0;
        spaceDown = false;
        spaceEnable = true;
        inDialogue = false;
        dialogueEnd = false;
        dialogueNext = false;
        whiteFlash = false;
        control = true;
        text.text = "";
        tipColor = Color.white;
        StartCoroutine(Run());
        if (screenShake)
        {
            StartCoroutine(ScreenShake());
        }
        if (tip != null)
        {
            tipColor = tip.color;
            StartCoroutine(RunTip());
        }
	}

    /// <summary>
    /// Shake the screen during Micah's death scene,
    /// Screen shakes more violently after the dialogue
    /// </summary>
    /// <returns></returns>
    IEnumerator ScreenShake()
    {
        Vector3 cameraPos = scene.rectTransform.position;
        float ampl = 15f;
        while (!whiteFlash)
        {
            scene.rectTransform.position = cameraPos + (Vector3)Random.insideUnitCircle * ampl;
            yield return 0;
        }
        Color initBackColor = back.color;
        Color initSceneColor = scene.color;
        float alpha = 1f;
        while (deathSound1.volume > 0)
        {
            deathSound1.volume -= 0.003f;
            alpha -= 0.005f;
            initBackColor.a = alpha;
            initSceneColor.a = alpha;
            back.color = initBackColor;
            scene.color = initSceneColor;
            ampl += 0.6f;
            scene.rectTransform.position = cameraPos + (Vector3)Random.insideUnitCircle * ampl;
            yield return 0;
        }
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene(firstLevelScene);
        Advance();
    }

    /// <summary>
    /// Advance to the next dialogue text
    /// </summary>
    void Advance()
    {
        spaceDown = true;
        if (inDialogue)
        {
            if (dialogueEnd)
            {
                dialogueNext = true;
                dialogueEnd = false;
            }
            else
            {
                dialogueNext = false;
                dialogueEnd = true;
            }
        }
    }
	
	void Update () {
        //Use the space bar to advance through the dialogue (only works if in the dialogue)
        if (Input.GetKeyDown(KeyCode.Space) && spaceEnable && control)
        {
            spaceDown = true;
            if (inDialogue)
            {
                if (dialogueEnd)
                {
                    dialogueNext = true;
                    dialogueEnd = false;
                }
                else
                {
                    dialogueNext = false;
                    dialogueEnd = true;
                }
            }
        }
        else
        {
            if (control)
                spaceDown = false;
        }
	}

    /// <summary>
    /// Disable use of the space key for one frame
    /// </summary>
    /// <returns></returns>
    IEnumerator LockSpace()
    {
        spaceDown = false;
        spaceEnable = false;
        yield return 0;
        spaceEnable = true;
    }

    /// <summary>
    /// For dialogue box testing purposes
    /// </summary>
    /// <returns></returns>
    IEnumerator TestRun()
    {
        while (!spaceDown)
            yield return 0;
        StartCoroutine(LockSpace());
        text.text = "1";

        while (!spaceDown)
            yield return 0;
        StartCoroutine(LockSpace());
        text.text = "2";

        while (!spaceDown)
            yield return 0;
        StartCoroutine(LockSpace());
        text.text = "3";

        while (!spaceDown)
            yield return 0;
        StartCoroutine(LockSpace());
        text.text = "4";

        while (!spaceDown)
            yield return 0;
        StartCoroutine(LockSpace());
        text.text = "5";

        while (!spaceDown)
            yield return 0;
        StartCoroutine(LockSpace());
        text.text = "6";
    }

    /// <summary>
    /// Display "Use the [Space] button to advance"
    /// </summary>
    /// <returns></returns>
    IEnumerator RunTip()
    {
        for (int i = 0; i < 80; i++)
        {
            if (spaceDown)
            {
                break;
            }
            float textAlpha = (i + 1) / 80.0f;
            tip.color = new Color(tipColor.r, tipColor.g, tip.color.b, (textAlpha));
            yield return 0;
        }
        while (!spaceDown)
        {
            yield return 0;
        }
        for (int i = 0; i < 80; i++)
        {
            float textAlpha = 1f - (i + 1) / 80.0f;
            tip.color = new Color(tipColor.r, tipColor.g, tipColor.b, (textAlpha));
            yield return 0;
        }

    }

    /// <summary>
    /// Change the cutscene image periodically
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeScene()
    {
        for (int i = 0; i < fadeTime; i++)
        {
            float alpha = ((i + 1) / (float)fadeTime);
            black.color = new Color(black.color.r, black.color.g, black.color.b, alpha);
            yield return 0;
        }
        if (sceneIndex < sceneImages.Length)
        {
            sceneImages[sceneIndex].enabled = false;
            sceneIndex++;
            sceneImages[sceneIndex].enabled = true;
        }
        for (int i = 0; i < fadeTime; i++)
        {
            float alpha = 1.0f - ((i + 1) / (float)fadeTime);
            black.color = new Color(black.color.r, black.color.g, black.color.b, alpha);
            yield return 0;
        }
    }

    /// <summary>
    /// Fade out sequence for the end of the dialogue scene
    /// </summary>
    /// <returns></returns>
    IEnumerator SlowFadeOut()
    {
        for (int i = 0; i < slowFadeTime; i++)
        {
            float alpha = ((i + 1) / (float)slowFadeTime);
            black.color = new Color(black.color.r, black.color.g, black.color.b, alpha);
            yield return 0;
        }
        //Application.LoadLevel(firstLevelScene);
        SceneManager.LoadScene(firstLevelScene);
    }

    /* Special dialogue commands
     * \w(#)    - Wait for the specified number of frames
     * \n       - New line
     * \s       - Next scene
     */
    /// <summary>
    /// Run the dialogue box routine
    /// </summary>
    /// <returns></returns>
    IEnumerator Run()
    {
        for (int i = 0; i < sceneImages.Length; i++)
        {
            sceneImages[i].enabled = false;
        }
        sceneImages[sceneIndex].enabled = true;
        //Fade in the dialogue box
        for (int i = 0; i < 20; i++)
        {
            float boxAlpha = ((i + 1) / 20.0f) * boxMaxAlpha;
            box.color = new Color(1f, 1f, 1f, boxAlpha);
            yield return 0;
        }
        //Start the dialogue
        inDialogue = true;
        foreach (string paragraph in dialogue)
        {
            text.text = "";
            string currentString = paragraph;
            currentString = paragraph.Replace("\\n", "\n");
            int length = currentString.Length;
            //Wait a little before reading the paragraph
            //yield return new WaitForSeconds(charDelay);
            for (int i = 0; i < length; i++)
            {
                bool waiting = false;
                //Skip the dialogue by pressing Space
                if (dialogueEnd)
                {
                    spaceDown = true;
                    //text.text = currentString;
                    //break;
                }
                if (i < length - 1)
                {
                    //Wait (\w#)
                    if (currentString[i] == '\\' && currentString[i + 1] == 'w' && currentString[i + 2] == '(')
                    {
                        waiting = true;
                        i += 3;
                        string framesString = "";
                        do
                        {
                            framesString += currentString[i++];
                        } while (currentString[i] != ')');
                        int frames = int.Parse(framesString);
                        if (!spaceDown)
                        {
                            for (int j = 0; j < frames; j++)
                            {
                                yield return 0;
                            }
                        }
                    }
                    //Change scene (\s)
                    else if (currentString[i] == '\\' && currentString[i + 1] == 's')
                    {
                        i += 1;
                        StartCoroutine(ChangeScene());
                        continue;
                    }
                    //White flash (\f)
                    else if (currentString[i] == '\\' && currentString[i + 1] == 'f')
                    {
                        i += 1;
                        whiteFlash = true;
                        continue;
                    }
                    //Disable dialogue control
                    else if (currentString[i] == '\\' && currentString[i + 1] == 'd')
                    {
                        i += 1;
                        control = false;
                        continue;
                    }
                    else
                    {
                        text.text += currentString[i];
                    }
                }
                else
                    text.text += currentString[i];
                //Skip spaces
                //if (currentString[i] == ' ')
                //{
                //    continue;
                //}
                //Skip newlines
                if (currentString[i] == '\n')
                {
                    continue;
                }
                if (currentString[i] != ' ' && currentString[i] != '\n' && !waiting)
                    beep.Play();
                if (i == length - 1)
                {
                    dialogueEnd = true;
                    break;
                }
                if (!spaceDown)
                    yield return new WaitForSeconds(charDelay);
            }
            if (spaceDown)
            {
                StartCoroutine(LockSpace());
            }
            //Wait for the user to press space again to read the next paragraph
            while (!spaceDown)
            {
                yield return 0;
            }
            StartCoroutine(LockSpace());

        }
        text.text = "";
        for (int i = 0; i < 20; i++)
        {
            float boxAlpha = boxMaxAlpha - ((i + 1) / 20.0f) * boxMaxAlpha;
            box.color = new Color(1f, 1f, 1f, boxAlpha);
            yield return 0;
        }
        StartCoroutine(SlowFadeOut());
    }
}
