using System;
using UnityEngine;

public class Mix : MonoBehaviour
{
    
    [SerializeField] private Transform _target;
    [SerializeField] private Animator _targetAnimator;
    //速度調整用倍率
    [SerializeField] private float _speedMultiplier = 1f;
    //最大アニメーション速度
    [SerializeField] private float _maxAnimationSpeed = 3f;
    //速度変化の滑らかさ
    [SerializeField] private float _speedSmoothTime = 0.1f;
    public Timer _timer;
    public Score _score;
    private Vector3 _lastMousePos;
    // 累積角度
    private float _rotationSum; 
    // 回転数（1回転=360度）
    private int _sumMix;
    
    //回転速度計算用
    //現在の回転速度
    private float _currentRotationSpeed;
    //現在のアニメーション速度
    private float _currentAnimationSpeed;
    //SmoothDamp用
    private float _animationSpeedVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (_timer != null)
        {
            _timer.TimeUPAction += AnimationStop;
        }
    }

    void OnDestroy()
    {
        _timer.TimeUPAction -= AnimationStop;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_timer.IsTimeUP)
        {
            TrackMouseRotation();
            UpdateAnimationSpeed();
        }
    }

    private void TrackMouseRotation()
    {
        if (!_timer.CountStart) return;

        Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 center = _target.position;

        Vector2 lastDir = (Vector2)_lastMousePos - center;
        Vector2 currentDir = (Vector2)currentMousePos - center;

        float angle = Vector2.SignedAngle(lastDir, currentDir);
        float angleAbs = Mathf.Abs(angle);

        //回転速度を計算
        _currentRotationSpeed = angleAbs/Time.deltaTime;
        
        _rotationSum += angleAbs; 
        
        // 360度ごとに1回転とする
        while (_rotationSum >= 360f)
        {
            _rotationSum -= 360f;
            _sumMix++; // 回転数を加算
            _score.AddMixScore(1); // Scoreクラスにスコア加算を通知
            
        }

        _lastMousePos = currentMousePos;
    }
    
    private void UpdateAnimationSpeed()
    {
        if (_targetAnimator == null) return;

        // 回転速度に基づいてアニメーション速度を計算
        float targetAnimationSpeed = Mathf.Clamp(
            (_currentRotationSpeed / 360f) * _speedMultiplier, 
            0f, 
            _maxAnimationSpeed
        );

        // 滑らかに速度を変更
        _currentAnimationSpeed = Mathf.SmoothDamp(
            _currentAnimationSpeed, 
            targetAnimationSpeed, 
            ref _animationSpeedVelocity, 
            _speedSmoothTime
        );

        // Animatorの速度を設定
        _targetAnimator.speed = _currentAnimationSpeed;

        // 回転していない場合は速度を徐々に0に
        if (!_timer.CountStart || _currentRotationSpeed < 10f)
        {
            _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, 0f, Time.deltaTime * 2f);
        }
    }

    // デバッグ用：現在の回転速度を取得
    public float GetCurrentRotationSpeed()
    {
        return _currentRotationSpeed;
    }

    // デバッグ用：現在のアニメーション速度を取得
    public float GetCurrentAnimationSpeed()
    {
        return _currentAnimationSpeed;
    }

    private void AnimationStop()
    {
        _targetAnimator.speed = 0;
    }
}