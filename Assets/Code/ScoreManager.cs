using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;

public class ScoreManager : MonoBehaviour



{
    public TMP_Text scoreText;
    public static ScoreManager instance;
    



    int score = 0;

    public void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = score.ToString() + "Total";
    }

    // Update is called once per frame


    public void ChangeScore(int changeValue)
    {
        score += changeValue;
        scoreText.text = score.ToString() + " Total";
    }
}
