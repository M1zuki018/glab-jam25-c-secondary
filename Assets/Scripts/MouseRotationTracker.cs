using System;
using UnityEngine;

public class MouseRotationTracker : MonoBehaviour
{
    public event Action<int, float> OnRotationUpdated; // (rotationCount, remainingTime)

    [SerializeField] private float limitTime = 5f;

    private Vector2 centerScreen;
    private float totalAngle = 0f;
    private int rotationCount = 0;
    private float rotationThreshold = 360f;

    private float lastAngle;
    private float elapsedTime = 0f;
    private bool isCounting = true;

    void Start()
    {
        centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
        lastAngle = GetMouseAngle();
    }

    void Update()
    {
        if (!isCounting) return;

        elapsedTime += Time.deltaTime;

        float currentAngle = GetMouseAngle();
        float deltaAngle = Mathf.DeltaAngle(lastAngle, currentAngle);

        totalAngle += deltaAngle;
        lastAngle = currentAngle;

        if (Mathf.Abs(totalAngle) >= rotationThreshold)
        {
            rotationCount++;
            totalAngle = 0f;
            Debug.Log($"Rotations: {rotationCount}");
        }

        if (elapsedTime >= limitTime)
        {
            isCounting = false;
            Debug.Log($"Time's up. Rotations: {rotationCount}");
        }

        float remainingTime = Mathf.Max(limitTime - elapsedTime, 0f);
        OnRotationUpdated?.Invoke(rotationCount, remainingTime);
    }

    public void CountReset()
    {
        rotationCount = 0;
        elapsedTime = 0f;
        isCounting = true;
        OnRotationUpdated?.Invoke(rotationCount, limitTime);
    }

    private float GetMouseAngle()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 direction = mousePos - centerScreen;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public int GetRotationCount() => rotationCount;
}
