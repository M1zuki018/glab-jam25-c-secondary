using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private int _file = 10;
    [SerializeField] private int _perfect = 15;
    [SerializeField] private Timer _timer;
    public int MixScore;
    public Text ScoreText;
    public Text EvaluationText;
    public int TotalScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_timer != null)
        {
            _timer.TimeUPAction += SumScore;
        }
        EvaluationText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _timer.TimeUPAction -= SumScore;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = $"Score: {MixScore}";
        // スコアに基づいて評価を決定
        string evaluation = GetEvaluation(MixScore);

        // 評価テキストを設定
        if (EvaluationText != null)
        {
            EvaluationText.text = evaluation;
        }
    }

    public void AddMixScore(int score)
    {
        MixScore += score;
    }

    public void SumScore()
    {
        //setactive(true)にする
        //Debug.Log("TimeUp");
        TotalScore = GetScore(MixScore);
        Debug.Log(TotalScore);
        EvaluationText.gameObject.SetActive(true);
    }

    /// <summary>
    /// スコアに基づいて評価を返す
    /// </summary>
    /// <param name="score">現在のスコア</param>
    /// <returns>評価文字列</returns>
    private string GetEvaluation(int score)
    {
        if (score >= _perfect)
        {
            return "Perfect!";
        }
        else if (score >= _file)
        {
            return "Good!";
        }
        else
        {
            return "Fair";
        }
    }

    private int GetScore(int score)
    {
        if (score >= _perfect)
        {
            return 2;
        }
        else if (score >= _file)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}