using UnityEngine;

public class MouseRotationTracker : MonoBehaviour
{
    private Vector2 centerScreen;
    private float totalAngle = 0f;
    public int rotationCount = 0;
    public float rotationThreshold = 360f;

    private float lastAngle;

    void Start()
    {
        centerScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
        lastAngle = GetMouseAngle();
    }

    void Update()
    {
        float currentAngle = GetMouseAngle();
        float deltaAngle = Mathf.DeltaAngle(lastAngle, currentAngle);

        totalAngle += deltaAngle;
        lastAngle = currentAngle;

        if (Mathf.Abs(totalAngle) >= rotationThreshold)
        {
            rotationCount++;
            totalAngle = 0f;
            Debug.Log($"‰ñ“]”: {rotationCount}");
        }
    }

    private float GetMouseAngle()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 direction = mousePos - centerScreen;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public int GetRotationCount() => rotationCount;
}
