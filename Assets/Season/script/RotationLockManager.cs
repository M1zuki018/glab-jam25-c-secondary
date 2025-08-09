using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RotationLockManager : MonoBehaviour
{
    [SerializeField] private Seasoning_C target;
    [SerializeField] private Slider slider;  // スライダーを参照
    private int score = 0;

    private bool _hasRotatedOnce = false;
    private bool _hasLockedRotation = false;

    void Update()
    {
        if (!_hasRotatedOnce && target.DeltaY != 0)
        {
            _hasRotatedOnce = true;
        }

        if (_hasRotatedOnce && target.transform.eulerAngles.z <= 0.1f)
        {
            if (!_hasLockedRotation)
            {
                target.CanRotate = false;
                _hasLockedRotation = true;

                CheckSliderValue(slider.value);
                GameManager.Instance.AddScore(score);

                StartCoroutine(DelayedTransition());
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
        else if (value >= 0.4f && value < 0.48f)
        {
            Debug.Log("成功");
            score = 1;

        }
        else if (value >= 0.53f && value <= 0.6f)
        {
            Debug.Log("成功");
            score = 1;

        }
        else
        {
            Debug.Log("完璧");
            score = 2;

        }
    }

    private IEnumerator DelayedTransition()
    {
        yield return new WaitForSeconds(2f);
        TransitionManager.Instance.StartTransition("Mazeru");
    }
}
