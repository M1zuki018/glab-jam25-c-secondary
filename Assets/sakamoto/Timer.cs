using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timer = 60f;
    /// <summary>
    /// カウントスタートするbool値
    /// </summary>
    public bool CountStart = false;
    /// <summary>
    /// TimeUpかどうか判定
    /// </summary>
    public bool IsTimeUP = false;
    /// <summary>
    /// TimeUpのアクション
    /// </summary>
    public Action TimeUPAction;

    public Text TimerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CountStart = false;
        IsTimeUP = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CountStart)
        {
            TimerCount();
        }
        TimerText.text=$"Time : {_timer:F1}";
    }

    private void TimerCount()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
        }
        else if (!IsTimeUP && _timer <= 0f)
        {
            _timer = 0f;
            TimeUPAction?.Invoke();
            IsTimeUP = true;
            CountStart = false;
        }
    }

    //ボタンやアクションで呼ぶ
    public void TimerStart()
    {
        if(CountStart==false)
        CountStart = true;
        else
        CountStart = false;
        
    }
}