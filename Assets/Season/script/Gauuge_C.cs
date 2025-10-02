using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Gauuge_C : MonoBehaviour
{
    [SerializeField] private Seasoning_C _controller;
    [SerializeField] private Slider _gaugeSlider;
    [SerializeField] private Transform _rotatingObject; // 回転するオブジェクト
    [SerializeField] private float rotationThreshold = 60f; // Fill start
    [SerializeField] private float fillMultiplier = 0.01f; // Fill speed
    [SerializeField] private float maxAngle = 180f; // angle max
    [SerializeField] AudioClip SeasoningClip;

    private int score = 0;

    private bool _hasPassedThreshold = false;
    private bool _hasLockedRotation = false;
    private bool _isPlayingSeasoning = false;

    void Update()
    {
        SaltRotation();
    }

    private void SaltRotation()
    {
        if (_hasLockedRotation) return;

        // Angle actuel du sel
        float zAngle = _rotatingObject.eulerAngles.z;
        zAngle = Mathf.Clamp(zAngle, 0f, maxAngle); // Never pass through 180 degree

        // If threshold have been through
        if (!_hasPassedThreshold && zAngle >= rotationThreshold)
        {
            _hasPassedThreshold = true;
        }

        // More big is the angle more speed it goes
        if (_hasPassedThreshold && zAngle > rotationThreshold)
        {
            if (!_isPlayingSeasoning)
            {
                AudioManager.Instance.PlayLoopSFX(SeasoningClip);
                AudioManager.Instance.sfxLoopSource.pitch = 0.15f;
                _isPlayingSeasoning = true;
            }

            float angleAboveThreshold = zAngle - rotationThreshold; // 0 to 90 max
            _gaugeSlider.value += angleAboveThreshold * fillMultiplier * Time.deltaTime;
            _gaugeSlider.value = Mathf.Clamp01(_gaugeSlider.value);
        }

        // After go above the threshold after we through it once
        if (_hasPassedThreshold && zAngle < rotationThreshold)
        {
            _controller.CanRotate = false;
            _hasLockedRotation = true;

            // Reset rotation
            _rotatingObject.rotation = Quaternion.Euler(0f, 0f, 0f);

            // Check score and go for next scene
            CheckSliderValue(_gaugeSlider.value);
            GameManager.Instance.AddSeasoningScore(score);
            if (_isPlayingSeasoning)
            {
                AudioManager.Instance.StopLoopSFX();
                _isPlayingSeasoning = false;
            }
        }
    }

    private void CheckSliderValue(float value)
    {
        if (value < 0.4f || value >= 0.61f)
        {
            Debug.Log("失敗");
            score = 0;
        }
        else if ((value >= 0.4f && value < 0.48f) || (value >= 0.53f && value <= 0.6f))
        {
            Debug.Log("成功");
            score = 1;
        }
        else
        {
            Debug.Log("完璧");
            score = 2;
        }

        _controller.gameActive = false;
    }
}
