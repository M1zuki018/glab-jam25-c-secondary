// 2. UI表示クラス（MouseRotationTrackerを参照）
using TMPro;
using UnityEngine;

public class Display : MonoBehaviour
{
    [SerializeField] private MouseRotationTracker tracker;
    [SerializeField] private TextMeshProUGUI text;

    void OnEnable()
    {
        tracker.OnRotationUpdated += UpdateText;
    }
    void OnDisable()
    {
        tracker.OnRotationUpdated -= UpdateText;
    }

    private void UpdateText(int rotationCount, float remainingTime)
    {
        if (remainingTime <= 0f)
        {
            text.text = $"Rotations: {rotationCount}\nTime's up";
        }
        else
        {
            text.text = $"Rotations: {rotationCount}\nRemaining time: {remainingTime:F2}s";
        }
    }
}
