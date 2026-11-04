using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(NextScene());
    }

   IEnumerator NextScene()
    {
        yield return new WaitForSeconds(5.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }
}
