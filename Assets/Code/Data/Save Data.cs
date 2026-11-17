using System;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;



public class SaveData : MonoBehaviour
{
    public bool EditorClearData = false;
    public bool EditorLoadData = false;

    public bool hasLoadedData = false;
    public GameplayData currentData;

    [Header("This will be used for new games, so set the starting data here")]
    [SerializeField] private GameplayData newSaveDefaultData;

    [Space]
    [Header("File paths, these are grabbed automatically")]
    [SerializeField] private string persistentPath;
    [SerializeField] private string filePath;

    public static SaveData instance;

    private void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        persistentPath = Application.persistentDataPath;
        filePath = persistentPath + "/" + "SaveFile";
    }

    public void LoadFile()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("No save file detected while loading");
        }
        else
        {
            string jsonData = File.ReadAllText(filePath);
            currentData = JsonUtility.FromJson<GameplayData>(jsonData);
            hasLoadedData = true;
        }
    }

    public void SaveFile()
    {
        string jsonData = JsonUtility.ToJson(currentData);
        File.WriteAllText(filePath, jsonData);
    }

    public void CreateSaveFile()
    {
        currentData = newSaveDefaultData;
        hasLoadedData = true;
    }

    public void DeleteFile()
    {
        File.Delete(filePath);
    }

    public void LoadOrCreateFile()
    {
        if(File.Exists(filePath))
        {
            LoadFile();
        }
        else
        {
            CreateSaveFile();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (EditorClearData)
        {
            EditorClearData = false;
            DeleteFile();
        }

        if (EditorLoadData)
        {
            EditorLoadData = false;
            LoadOrCreateFile();
        }
    }
}

[Serializable]
public struct GameplayData
{
    [Header("Progression Data")]
    public int money;
    public storedFace[] storedFaces;
    public Character[] alreadyMetCharacters;
    public bool hasSeenFirstCutscene;

    [Header("Current Setups for initialising scenes")]
    public CharacterScriptable currentCharacter;
    public int currentCharacterStage;

    public bool usingFixedSpawning;
    public int currentFixedSpawningIndex;

    public DialogueSegmentScriptable currentDialogue;
    public int currentDialogueIndex;
}
