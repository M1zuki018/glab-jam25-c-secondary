using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    
    [SerializeField]MouseRotationTracker mouseRotationTracker;
    [SerializeField] TextMeshProUGUI number;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
      
        int count = mouseRotationTracker.GetRotationCount();
        number.text = count .ToString();
    }

    // Update is called once per frame
    
}
