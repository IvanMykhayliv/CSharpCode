using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class SaveFileManager : MonoBehaviour {
    public Text saveSlotText;
    public Text saveFileText;
    public bool canChangeFileSlot;
    public bool canSaveFile;
    public bool onTitleScreen;
    public static bool[] ExistingSaveFiles;
    public static int fileSlot;
    public Button[] FileButtons;
    private float saveSlotTextFrames;
    private float saveFileTextFrames;
    private string dir;

	void Start () {
        saveSlotTextFrames = 0f;
        saveFileTextFrames = 0f;
        saveSlotText.gameObject.SetActive(false);
        saveFileText.gameObject.SetActive(false);
        dir = Application.dataPath + "/";
        if (onTitleScreen)
        {
            fileSlot = 1;
            //WriteSaveFile(fileSlot);
            CheckExistingFiles();

            //LoadFile();
        }
	}

    void CheckExistingFiles()
    {
        ExistingSaveFiles = new bool[5] { false, false, false, false, false };
        for (int i = 0; i < 5; i++)
        {
            bool fileExists = File.Exists(dir + "SaveFile" + (i + 1).ToString() + ".txt");
            if (fileExists)
            {
                ExistingSaveFiles[i] = true;
                FileButtons[i].gameObject.SetActive(true);
            }
            else
            {
                FileButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void ChangeSaveSlot(int saveSlot)
    {
        if (!canChangeFileSlot)
        {
            return;
        }
        fileSlot = saveSlot;
        saveSlotTextFrames = 1.2f;
        saveSlotText.gameObject.SetActive(true);
        saveSlotText.text = "Save slot " + saveSlot + " selected";
    }

    public void WriteSaveFile()
    {
        //Format: SaveFile1.txt, SaveFile2.txt, ...
        using (FileStream file = new FileStream(dir + "SaveFile" + fileSlot.ToString() + ".txt", FileMode.Create, FileAccess.Write))
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write("CurrentLevel: " + Application.loadedLevelName + " ");
                writer.Write("CurrentWave: " + EnemyGenerator.Waves);
            }
        }
        saveFileTextFrames = 1.2f;
        saveFileText.gameObject.SetActive(true);
        saveFileText.text = "File " + fileSlot + " saved";
    }

    public void LoadFile(int fileSlot)
    {
        string levelToLoad = "";
        int waveNumber = 0;
        using (StreamReader streamReader = new StreamReader(dir + "SaveFile" + fileSlot.ToString() + ".txt"))
        {
            string[] saveFileText = streamReader.ReadToEnd().Split(' ');
            for (int i = 0; i < saveFileText.Length; i++)
            {
                if (saveFileText[i] == "CurrentLevel:")
                {
                    levelToLoad = saveFileText[i + 1];
                }
                else if (saveFileText[i] == "CurrentWave:")
                {
                    waveNumber = int.Parse(saveFileText[i + 1]);
                }
            }
        }

        EnemyGenerator.Waves = waveNumber;
        Application.LoadLevel(levelToLoad);
    }
	
	void Update () {
        if (saveSlotTextFrames > 0)
        {
            saveSlotTextFrames -= Time.deltaTime;
        }
        if (saveSlotTextFrames <= 0)
        {
            saveSlotText.gameObject.SetActive(false);
        }

        if (saveFileTextFrames > 0)
        {
            saveFileTextFrames -= Time.deltaTime;
        }
        if (saveFileTextFrames <= 0)
        {
            saveFileText.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeSaveSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeSaveSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeSaveSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ChangeSaveSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ChangeSaveSlot(5);
        }
	}
}