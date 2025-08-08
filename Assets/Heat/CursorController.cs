using UnityEngine;

public class CursorController : MonoBehaviour
{
    public RectTransform cursor; 
    public RectTransform bar;    // 赤いバー
    public float speed = 200f;   // スピード

    private float minX;
    private float maxX;
    private int direction = 1; // 1 = みぎ, -1 = ひだり

    void Start()
    {
        float barWidth = bar.rect.width;
        float cursorHalfWidth = cursor.rect.width / 2f;

        
        minX = -barWidth / 2f + cursorHalfWidth;
        maxX = barWidth / 2f - cursorHalfWidth;
    }

    void Update()
    {
        // 自動
        cursor.anchoredPosition += Vector2.right * direction * speed * Time.deltaTime;

        // ブロック
        if (cursor.anchoredPosition.x >= maxX)
        {
            direction = -1; // 左
        }
        else if (cursor.anchoredPosition.x <= minX)
        {
            direction = 1; // 右
        }
    }
}
