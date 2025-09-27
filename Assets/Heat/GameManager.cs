using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text scoreText;
    public int totalScore = 0;
    public int cookingScore = 0;
    public int seasoningScore = 0;
    public int mixingScore = 0;

    //public AudioClip BGM;
    //private AudioSource source;

    private void Awake()
    {

        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddCookingScore(int pts)
    {
        cookingScore += pts;
        FinalScore();
    }

    public void AddSeasoningScore(int pts)
    {
        seasoningScore += pts;
        FinalScore();
    }

    public void AddMixingScore(int pts)
    {
        mixingScore += pts;
        FinalScore();
    }

    public void FinalScore()
    {
        totalScore = cookingScore + seasoningScore + mixingScore;
        UpdateScoreUI();
    }

    public void RegisterScoreText(TMP_Text text)
    {
        scoreText = text;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + totalScore;
    }
}
