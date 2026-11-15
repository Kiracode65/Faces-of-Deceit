using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Transform parentToSpawnCharacterInto;
    [SerializeField] private float minTimeBetweenCharacters = 5;
    [SerializeField] private float maxTimeBetweenCharacters = 10;

    [SerializeField] private Character[] possibleRandomCharacters;

    [Header("Used for fixed experience, leave empty for pure random")]
    [SerializeField] private bool useFixedCharacters;
    [SerializeField] private bool revertToRandomAfterwards;
    [SerializeField] private Character[] fixedSpawningCharacters;
    [SerializeField] private int fixedSpawningIndex = 0;

    private Character currentChar;
    private float timeTillNextSpawn = 0;

    private void Start()
    {
        if(currentChar == null)
        {
            SpawnNextCharacter();
        }
        else
        {

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
                fixedSpawningIndex++;
            }


        }
        else
        {
            SpawnCharacter(possibleRandomCharacters[Random.Range(0, possibleRandomCharacters.Length - 1)]);
        }
    }

    public void SpawnCharacter(Character character)
    {
        if(currentChar != null)
        {
            Debug.LogError("Tried to spawn character while one already exists");
            return;
        }

        currentChar = Instantiate(character, parentToSpawnCharacterInto);
        currentChar.Initialise();
        currentChar.onDestroy.AddListener(OnCharacterDestroyed);
    }

    private void OnCharacterDestroyed()
    {
        timeTillNextSpawn = Random.Range(minTimeBetweenCharacters, maxTimeBetweenCharacters);
    }
}
