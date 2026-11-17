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

    void Start()
    {
        if(SaveData.instance != null && SaveData.instance.hasLoadedData) score = SaveData.instance.currentData.money;
        scoreText.text = score.ToString() + "Total";
    }

    public void ChangeScore(int changeValue)
    {
        score += changeValue;
        if (SaveData.instance != null && SaveData.instance.hasLoadedData) SaveData.instance.currentData.money = score;
        scoreText.text = score.ToString() + " Total";
    }
}
