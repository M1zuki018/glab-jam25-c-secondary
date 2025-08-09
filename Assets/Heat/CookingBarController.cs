using UnityEngine;

public class CookingBarController : MonoBehaviour
{
    [Header("Zones")]
    public RectTransform greenZone;
    public RectTransform yellowZoneL;
    public RectTransform yellowZoneR;

    [Header("Points by zone")]
    public int pointsRed = 0;
    public int pointsYellow = 1;
    public int pointsGreen = 2;


    [Header("Settings")]
    public float greenWidth = 20f;
    public float yellowWidth = 40f;

    private float barWidth;

    void Start()
    {
        barWidth = GetComponent<RectTransform>().rect.width;

        float totalZonesWidth = greenWidth + 2 * yellowWidth;

        // Décalage depuis le CENTRE
        float startX = -barWidth / 2f + Random.Range(0f, barWidth - totalZonesWidth);

        // Positionner les zones avec ancrage CENTER
        yellowZoneL.anchoredPosition = new Vector2(startX + yellowWidth / 2f, 0);
        yellowZoneL.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, yellowWidth);

        greenZone.anchoredPosition = new Vector2(startX + yellowWidth + greenWidth / 2f, 0);
        greenZone.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, greenWidth);

        yellowZoneR.anchoredPosition = new Vector2(startX + yellowWidth + greenWidth + yellowWidth / 2f, 0);
        yellowZoneR.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, yellowWidth);
    }


    public int GetPoints(float x)
    {

        Debug.Log($"Test de position x={x}");
        Debug.Log($"Zone verte = {greenZone.anchoredPosition.x - greenZone.rect.width / 2f} à {greenZone.anchoredPosition.x + greenZone.rect.width / 2f}");
        Debug.Log($"Zone jaune L = {yellowZoneL.anchoredPosition.x - yellowZoneL.rect.width / 2f} à {yellowZoneL.anchoredPosition.x + yellowZoneL.rect.width / 2f}");
        Debug.Log($"Zone jaune R = {yellowZoneR.anchoredPosition.x - yellowZoneR.rect.width / 2f} à {yellowZoneR.anchoredPosition.x + yellowZoneR.rect.width / 2f}");

        if (IsInsideZone(greenZone, x)) return pointsGreen;
        if (IsInsideZone(yellowZoneL, x) || IsInsideZone(yellowZoneR, x)) return pointsYellow;
        return pointsRed;
    }

    private bool IsInsideZone(RectTransform zone, float x)
    {
        float left = zone.anchoredPosition.x - zone.rect.width / 2f;
        float right = zone.anchoredPosition.x + zone.rect.width / 2f;
        return x >= left && x <= right;
    }

}
