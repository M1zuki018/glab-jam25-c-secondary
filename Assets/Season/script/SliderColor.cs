using UnityEngine;
using UnityEngine.UI;

public class SliderColor : MonoBehaviour
{
    [SerializeField] private Image[] _zoneImages;
    [SerializeField] private Color[] _zoneColors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_zoneImages.Length != _zoneColors.Length)
        {
            Debug.LogWarning("Image‚Æcolors‚Ì”‚ª•sˆê’v"); 
            return;
        }
    
        for(int i = 0;  i < _zoneImages.Length; i++)
        {
            _zoneImages[i].color = _zoneColors[i];
        }
    }
}
