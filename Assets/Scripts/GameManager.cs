using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text scoreText;
    public int totalScore = 0;
    public int cookingScore = 0;
    public int seasoningScore = 0;
    public int mixingScore = 0;
    public float bounceScale = 1.5f;
    public float animDuration = 0.3f;
    [SerializeField] AudioClip goodClip;
    [SerializeField] AudioClip badClip;

    private void Awake()
    {

        if (Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private IEnumerator AnimateScoreAndTransition(int score, string nextScene)
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateScore(score));  // Wait for text animation
        TransitionManager.Instance.StartTransition(nextScene);
        yield return null;
    }

    public void AddCookingScore(int pts)
    {
        cookingScore += pts;
        FinalScore();

        // Display result
        StartCoroutine(AnimateScoreAndTransition(pts, "Season_Test"));
    }

    public void AddSeasoningScore(int pts)
    {
        seasoningScore += pts;
        FinalScore();

        StartCoroutine(AnimateScoreAndTransition(pts, "Mazeru"));
    }

    public void AddMixingScore(int pts)
    {
        mixingScore += pts;
        FinalScore();

        StartCoroutine(AnimateScoreAndTransition(pts, "Result"));
    }

    public void FinalScore()
    {
        totalScore = cookingScore + seasoningScore + mixingScore;
        //UpdateScoreUI();
    }

    /*public void RegisterScoreText(TMP_Text text)
    {
        scoreText = text;
        //UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + totalScore;
    }*/

    private string GetResultScore(int score)
    {
        return score switch
        {
            0 => "Ž¸”s" ,
            1 => "—Ç‚µ",
            2 => "Š®àø",
            _ => ""
        };

    }

    public IEnumerator AnimateScore(int score)
    {
        yield return new WaitForSeconds(0.5f);

        if (scoreText != null)
        {
            scoreText.text = GetResultScore(score);
            scoreText.transform.localScale = Vector3.zero;
            scoreText.gameObject.SetActive(true);
        }

        if (score == 0)
        {
            AudioManager.Instance.PlaySFX(badClip, 0.4f);
        }
        else
        {
            AudioManager.Instance.PlaySFX(goodClip, 0.4f);

        }

        float t = 0f;
        while (t < animDuration)
        {
            t += Time.deltaTime;
            float scale = Mathf.SmoothStep(0f, bounceScale, t / animDuration);
            scoreText.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        t = 0f;
        while (t < animDuration)
        {
            t += Time.deltaTime;
            float scale = Mathf.SmoothStep(bounceScale, 1f, t / animDuration);
            scoreText.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        scoreText.gameObject.SetActive(false);
    }
}
