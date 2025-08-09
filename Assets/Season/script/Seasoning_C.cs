using UnityEngine;
//using UnityEngine.InputSystem;

public class Seasoning_C : MonoBehaviour
{
    private Vector3 prevMousePos;

    [SerializeField]
    private float rotationSpeed = 0.1f;

    public float DeltaY {  get; private set; }
    public bool CanRotate { get; set; } = true;

    void Start()
    {
        prevMousePos = Input.mousePosition;
    }

    void Update()
    {
        if (!CanRotate) return;

        Vector3 currentMousePos = Input.mousePosition;
        DeltaY = currentMousePos.y - prevMousePos.y; // ★ここで毎フレーム更新

        if (Mathf.Abs(DeltaY) > 0.5f)
        {
            float currentZRotation = NormalizeAngle(transform.eulerAngles.z);
            float newZRotation = currentZRotation - DeltaY * rotationSpeed;

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