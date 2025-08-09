using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public RectTransform cursor;
    public RectTransform bar;    // 赤いバー
    public float speed = 300f;   // スピード
    public float pulseSpeed = 6f;
    public float pulseAmount = 0.05f;

    private float minX;
    private float maxX;
    private int direction = 0; // 1 = みぎ, -1 = ひだり

    public void Initialize()
    {
        float barWidth = bar.rect.width;
        float cursorHalfWidth = cursor.rect.width / 2f;

        minX = -barWidth / 2f + cursorHalfWidth;
        maxX = barWidth / 2f - cursorHalfWidth;

        cursor.anchoredPosition = new Vector2(0, cursor.anchoredPosition.y);
        direction = 0;
    }

    void Update()
    {
        float scaleY = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        cursor.localScale = new Vector3(1f, scaleY, 1f);

        if (direction != 0)
        {
            cursor.anchoredPosition += Vector2.right * direction * speed * Time.deltaTime;
            cursor.anchoredPosition = new Vector2(
                Mathf.Clamp(cursor.anchoredPosition.x, minX, maxX),
                cursor.anchoredPosition.y
            );
        }
    }

    public IEnumerator Flash(Image img, Color color, float duration)
    {
        img.color = color;
        img.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        img.gameObject.SetActive(false);
    }

    public void HandleMove(int moveDir, InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            direction = moveDir;
        }
    }

    public bool IsMoving() => direction != 0;

    public void Stop() => direction = 0;

    public float GetCursorPosition() => cursor.anchoredPosition.x;
}
