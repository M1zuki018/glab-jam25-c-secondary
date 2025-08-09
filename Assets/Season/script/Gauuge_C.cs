using UnityEngine;
using UnityEngine.UI;

public class Gauuge_C : MonoBehaviour
{
    [SerializeField] private Seasoning_C _controller;
    [SerializeField] private Slider _gaugeSlider;
    [SerializeField] private Transform _rotatingObject; // ��]����I�u�W�F�N�g
    [SerializeField] private float _angleTolerance = 5f; // ���x�܂ŋ��e���邩
    [SerializeField] private float _baseSpeed = 0.1f;
    [SerializeField] private float _maxBoost = 0.3f;
    [SerializeField] private float _slowFactor = 0.5f;
    [SerializeField] private float _deadZone = 0.5f; // �����ȗh��𖳎�

    private float _currentSpeed = 0f;

    void Update()
    {
        // Z���̊p�x�擾�i0�`180�܂ł�-180�`180�ɕϊ��j
        float zAngle = _rotatingObject.eulerAngles.z;
        if (zAngle > 180f) zAngle -= 360f;

        // ���e�͈͓��Ȃ�Q�[�W�X�V����return
        if (Mathf.Abs(zAngle) <= _angleTolerance)
        {
            return;
        }

        float deltaY = _controller.DeltaY;

        if (Mathf.Abs(deltaY) > _deadZone)
        {
            if (deltaY < 0) // �}�E�X�����ɓ�����
            {
                float boost = Mathf.Clamp(-deltaY * 0.01f, 0f, _maxBoost);
                _currentSpeed = _baseSpeed + boost;
            }
            else // �}�E�X����ɓ�����
            {
                _currentSpeed = _baseSpeed * _slowFactor;
            }
        }
        else
        {
            _currentSpeed = _baseSpeed; // �������Ă��Ȃ����͒ʏ푬�x
        }

        _gaugeSlider.value = Mathf.Clamp01(
            _gaugeSlider.value + _currentSpeed * Time.deltaTime
        );
    }
}
