using TMPro;
using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        GameManager.Instance.RegisterScoreText(scoreText);
    }
}