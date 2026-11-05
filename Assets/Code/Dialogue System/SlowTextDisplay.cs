using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SlowTextDisplay : MonoBehaviour
{
    [TextArea(1, 10)]
    public string textToDisplay;
    [Min(0.01f)] public float timeToTake;

    public TMP_Text textField;
    public UnityEvent onComplete;

    private int charsPerFrame;
    public bool Displaying = false;

    // Start is called before the first frame update
    void Start()
    {
        StartDisplayText(textToDisplay, timeToTake);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Displaying)
        {
            if(textField.text == textToDisplay)
            {
                onComplete.Invoke();
                Displaying = false;
                return;
            }
            else
            {
                textField.text = textToDisplay.Substring(0, Mathf.Clamp(textField.text.Length + charsPerFrame, 0, textToDisplay.Length));
            }
         
        }
    }

    public void StartDisplayText(string text, float time = 2)
    {
        textToDisplay = text;
        timeToTake = time;
        Displaying = true;
        textField.text = "";
        charsPerFrame = Mathf.CeilToInt(text.Length / time * Time.fixedDeltaTime);
    }

    public void StartDisplayTextBySpeed(string text, int charactersPerSecond = 3)
    {
        StartDisplayText(text, charactersPerSecond / Time.fixedDeltaTime);
    }

    public void SkipText()
    {
        if (!Displaying) return;
        textField.text = textToDisplay;
        Displaying = false;
        onComplete.Invoke();
    }

    public void ClearText()
    {
        textField.text = "";
        Displaying = false;
    }
}
