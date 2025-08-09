using UnityEngine;
using UnityEngine.UI;

public class Gauuge_C : MonoBehaviour
{
    [SerializeField] private Seasoning_C _controller;
    [SerializeField] private Slider _gaugeSlider;
    [SerializeField] private Transform _rotatingObject; // 回転するオブジェクト
    [SerializeField] private float _angleTolerance = 5f; // 何度まで許容するか
    [SerializeField] private float _baseSpeed = 0.1f;
    [SerializeField] private float _maxBoost = 0.3f;
    [SerializeField] private float _slowFactor = 0.5f;
    [SerializeField] private float _deadZone = 0.5f; // 小さな揺れを無視

    private float _currentSpeed = 0f;

    void Update()
    {
        // Z軸の角度取得（0～180までを-180～180に変換）
        float zAngle = _rotatingObject.eulerAngles.z;
        if (zAngle > 180f) zAngle -= 360f;

        // 許容範囲内ならゲージ更新せずreturn
        if (Mathf.Abs(zAngle) <= _angleTolerance)
        {
            return;
        }

        float deltaY = _controller.DeltaY;

        if (Mathf.Abs(deltaY) > _deadZone)
        {
            if (deltaY < 0) // マウスを下に動かす
            {
                float boost = Mathf.Clamp(-deltaY * 0.01f, 0f, _maxBoost);
                _currentSpeed = _baseSpeed + boost;
            }
            else // マウスを上に動かす
            {
                _currentSpeed = _baseSpeed * _slowFactor;
            }
        }
        else
        {
            _currentSpeed = _baseSpeed; // 動かしていない時は通常速度
        }

        _gaugeSlider.value = Mathf.Clamp01(
            _gaugeSlider.value + _currentSpeed * Time.deltaTime
        );
    }
}
