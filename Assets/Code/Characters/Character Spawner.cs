using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Transform parentToSpawnCharacterInto;
    [SerializeField] private float minTimeBetweenCharacters = 5;
    [SerializeField] private float maxTimeBetweenCharacters = 10;

    [SerializeField] private CharacterScriptable[] possibleRandomCharacters;

    [Header("Used for fixed experience, leave empty for pure random")]
    [SerializeField] private bool useFixedCharacters;
    [SerializeField] private bool revertToRandomAfterwards;
    [SerializeField] private CharacterScriptable[] fixedSpawningCharacters;
    [SerializeField] private int fixedSpawningIndex = 0;

    private Character currentChar;
    private CharacterScriptable currentCharScriptable;
    private float timeTillNextSpawn = 0;

    private void Start()
    {
        if(SaveData.instance != null && SaveData.instance.hasLoadedData)
        {
            useFixedCharacters = SaveData.instance.currentData.usingFixedSpawning;
            fixedSpawningIndex = SaveData.instance.currentData.currentFixedSpawningIndex;

            currentCharScriptable = SaveData.instance.currentData.currentCharacter;
            if(currentCharScriptable != null)
            {
                SpawnCharacter(currentCharScriptable, SaveData.instance.currentData.currentCharacterStage);
            }
            else
            {
                SpawnNextCharacter();
            }
        }
        else
        {
            SpawnNextCharacter();
        }
    }

    private void FixedUpdate()
    {
        if(timeTillNextSpawn > 0)
        {
            timeTillNextSpawn -= Time.fixedDeltaTime;

            if(timeTillNextSpawn <= 0 )
            {
                SpawnNextCharacter();
            }
        }
    }

    private void SpawnNextCharacter()
    {
        if (currentChar != null)
        {
            Debug.LogError("Tried to spawn character while one already exists");
            return;
        }

        if (useFixedCharacters)
        {
            if(fixedSpawningIndex >= fixedSpawningCharacters.Length)
            {
                Debug.Log("No more fixed spawning characters");

                if (revertToRandomAfterwards)
                {
                    Debug.Log("Continuing with random characters");
                    useFixedCharacters = false;
                    SpawnNextCharacter();
                }
                else
                {
                    return;
                }
            }
            else
            {
                SpawnCharacter(fixedSpawningCharacters[fixedSpawningIndex]);
            }


        }
        else
        {
            SpawnCharacter(possibleRandomCharacters[Random.Range(0, possibleRandomCharacters.Length - 1)]);
        }
    }

    public void SpawnCharacter(CharacterScriptable characterInfo, int jumpToStage = 0)
    {
        if(currentChar != null)
        {
            Debug.LogError("Tried to spawn character while one already exists");
            return;
        }

        if(SaveData.instance != null && SaveData.instance.hasLoadedData)
        {
            SaveData.instance.currentData.currentCharacter = characterInfo;
        }

        currentCharScriptable = characterInfo;
        currentChar = Instantiate(characterInfo.characterPrefab, parentToSpawnCharacterInto);
        currentChar.Initialise(jumpToStage);
        currentChar.onDestroy.AddListener(OnCharacterDestroyed);
    }

    private void OnCharacterDestroyed()
    {
        if (useFixedCharacters)
        {
            fixedSpawningIndex++;
            if (SaveData.instance != null && SaveData.instance.hasLoadedData) SaveData.instance.currentData.currentFixedSpawningIndex = fixedSpawningIndex;
        }

        timeTillNextSpawn = Random.Range(minTimeBetweenCharacters, maxTimeBetweenCharacters);
        currentCharScriptable = null;
    }
}
