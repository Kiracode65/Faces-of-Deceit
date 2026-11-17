using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string mainSceneName;
    [SerializeField] private string cutSceneName;

    public void PlayGame()
    {
        SaveData.instance.CreateSaveFile();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void ContinueGame()
    {
        SaveData.instance.LoadOrCreateFile();
        if (SaveData.instance.currentData.hasSeenFirstCutscene)
        {
            SceneManager.LoadScene(mainSceneName);
        }
        else
        {
            SceneManager.LoadScene(cutSceneName);
        }

    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        
        Application.Quit();
    }
}
