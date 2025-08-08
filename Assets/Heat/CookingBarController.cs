using UnityEngine;

public class CookingBarController : MonoBehaviour
{
    [Header("Zones")]
    public RectTransform greenZone;
    public RectTransform yellowZoneL;
    public RectTransform yellowZoneR;

    [Header("Settings")]
    public float greenWidth = 20f;
    public float yellowWidth = 40f;

    private float barWidth;

    void Start()
    {
        barWidth = GetComponent<RectTransform>().rect.width;

        float totalZonesWidth = greenWidth + 2 * yellowWidth;
        float startX = Random.Range(0f, barWidth - totalZonesWidth);

        // Positionner les zones
        yellowZoneL.anchoredPosition = new Vector2(startX, 0);
        yellowZoneL.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, yellowWidth);

        greenZone.anchoredPosition = new Vector2(startX + yellowWidth, 0);
        greenZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, greenWidth);

        yellowZoneR.anchoredPosition = new Vector2(startX + yellowWidth + greenWidth, 0);
        yellowZoneR.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, yellowWidth);
    }
}
