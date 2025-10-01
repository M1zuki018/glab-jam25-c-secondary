using System;
using TMPro;
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
    public int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_timer != null)
        {
            _timer.TimeUPAction += SumScore;
        }
    }

    private void OnDestroy()
    {
        _timer.TimeUPAction -= SumScore;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = $"Points: {MixScore}";
        // スコアに基づいて評価を決定
    }

    public void AddMixScore(int score)
    {
        MixScore += score;
    }

    public void SumScore()
    {
        //setactive(true)にする
        //Debug.Log("TimeUp");
        score = GetScore(MixScore);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddMixingScore(score);
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