using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Transform parentToSpawnCharacterInto;
    [SerializeField] private Character sampleCharacter;

    private Character currentChar;

    private void Start()
    {
        SpawnCharacter(sampleCharacter);
    }
    public void SpawnCharacter(Character character)
    {
        if(currentChar != null)
        {
            Debug.LogError("Tried to spawn character while one already exists");
            return;
        }

        currentChar = Instantiate(character, parentToSpawnCharacterInto);
    }
}
