using UnityEngine;
//using UnityEngine.InputSystem;

public class Seasoning_C : MonoBehaviour
{
    private Vector3 prevMousePos;

    [SerializeField]
    private float rotationSpeed = 0.1f;

    void Start()
    {
        prevMousePos = Input.mousePosition;
    }

    void Update()
    {
        Vector3 currentMousePos = Input.mousePosition;
        float deltaY = currentMousePos.y - prevMousePos.y;

        if (Mathf.Abs(deltaY) > 0.5f)
        {
            float currentZRotation = NormalizeAngle(transform.eulerAngles.z);
            float newZRotation = currentZRotation - deltaY * rotationSpeed;

            //  0°未満に行かないよう制限
            newZRotation = Mathf.Max(newZRotation, 0f);

            transform.rotation = Quaternion.Euler(0f, 0f, newZRotation);
        }

        prevMousePos = currentMousePos;
    }

    // 360°→-180〜180°に変換（扱いやすくするため）
    float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}