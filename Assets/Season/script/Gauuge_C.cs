using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gauuge_C : MonoBehaviour
{
    [SerializeField] private Seasoning_C _controller;
    [SerializeField] private Slider _gaugeSlider;
    [SerializeField] private Transform _rotatingObject; // 回転するオブジェクト
    [SerializeField] private float rotationThreshold = 60f; // seuil à partir duquel la barre se remplit
    [SerializeField] private float fillMultiplier = 0.01f; // vitesse de remplissage
    [SerializeField] private float maxAngle = 180f; // angle max

    private bool _hasPassedThreshold = false;
    private bool _hasLockedRotation = false;

    void Update()
    {
        if (_hasLockedRotation) return;

        // Angle actuel du sel
        float zAngle = _rotatingObject.eulerAngles.z;
        zAngle = Mathf.Clamp(zAngle, 0f, maxAngle); // ne jamais dépasser 0-180

        // On check si le seuil a été dépassé
        if (!_hasPassedThreshold && zAngle >= rotationThreshold)
        {
            _hasPassedThreshold = true;
        }

        // Remplissage proportionnel à l’angle au-delà du seuil
        if (_hasPassedThreshold && zAngle > rotationThreshold)
        {
            float angleAboveThreshold = zAngle - rotationThreshold; // de 0 à 90 max
            _gaugeSlider.value += angleAboveThreshold * fillMultiplier * Time.deltaTime;
            _gaugeSlider.value = Mathf.Clamp01(_gaugeSlider.value);
        }

        // Si on redescend sous le seuil après l’avoir dépassé
        if (_hasPassedThreshold && zAngle < rotationThreshold)
        {
            _controller.CanRotate = false;
            _hasLockedRotation = true;

            // Reset rotation
            _rotatingObject.rotation = Quaternion.Euler(0f, 0f, 0f);

            // Vérifie le score et lance la transition
            CheckSliderValue(_gaugeSlider.value);
            GameManager.Instance.AddScore(_gaugeSlider.value >= 0.48f && _gaugeSlider.value <= 0.53f ? 2 : 1);

            StartCoroutine(DelayedTransition());
        }
    }

    private void CheckSliderValue(float value)
    {
        if (value < 0.4f || value >= 0.61f)
        {
            Debug.Log("失敗");
        }
        else if ((value >= 0.4f && value < 0.48f) || (value >= 0.53f && value <= 0.6f))
        {
            Debug.Log("成功");
        }
        else
        {
            Debug.Log("完璧");
        }

        _controller.gameActive = false;
    }

    private IEnumerator DelayedTransition()
    {
        yield return new WaitForSeconds(2f);
        TransitionManager.Instance.StartTransition("Mazeru");
    }
}
