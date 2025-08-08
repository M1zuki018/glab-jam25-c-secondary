using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Vector2 centerScreen;
    private float totalAngle = 0f;
    public  int rotationCount = 0;
    public float rotationThreshold = 360f;
    private float lastAngle;
    void Update()
    {
        float currentAngle = GetMouseAngle1();
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
    private void Start()
    {
        centerScreen = new Vector2(Screen.width/2, Screen.height/2);
        
    }
    private float GetMouseAngle1()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 direction = mousePos - centerScreen;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
    public int GetRotationCount() => rotationCount;

}
