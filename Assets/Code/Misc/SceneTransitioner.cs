using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] private bool useNextSceneIndex = false;
    [SerializeField] private int sceneIndex;
    [SerializeField] private float transitionTime = 2f;
    private float transitionTimer = -1;
    public void TriggerSceneTransition()
    {
        if (FadeCanvas.instance != null) FadeCanvas.instance.FadeToBlack();
        transitionTimer = transitionTime;
    }

    private void FixedUpdate()
    {
        if(transitionTimer > 0)
        {
            transitionTimer -= Time.fixedDeltaTime;
            if (transitionTimer <= 0)
            {
                loadSceneFromInt();
            }
        }
    }

    private void loadSceneFromInt()
    {
        if (useNextSceneIndex) sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(sceneIndex);
    }
}
