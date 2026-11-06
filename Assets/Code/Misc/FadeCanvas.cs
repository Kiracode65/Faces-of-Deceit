using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    public Animator animator;
    public static FadeCanvas instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        FadeFromBlack();
    }

    public async Task FadeToBlack()
    {
        animator.Play("FadeCanvasFadeOut");
    }

    public async Task FadeFromBlack()
    {
        animator.Play("FadeCanvasFadeIn");
    }
}
