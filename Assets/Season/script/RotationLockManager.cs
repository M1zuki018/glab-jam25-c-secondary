using UnityEngine;
using UnityEngine.UI;

public class RotationLockManager : MonoBehaviour
{
    [SerializeField] private Seasoning_C target;
    [SerializeField] private Slider slider;  // �X���C�_�[���Q��

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
            }
        }
    }

    private void CheckSliderValue(float value)
    {
        if (value < 0.4f || value >= 0.61f)
        {
            Debug.Log("���s");
        }
        else if (value >= 0.4f && value < 0.48f)
        {
            Debug.Log("����");
        }
        else if (value >= 0.53f && value <= 0.6f)
        {
            Debug.Log("����");
        }
        else
        {
            Debug.Log("����");
        }
    }
}
