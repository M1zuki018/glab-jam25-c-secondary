using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _time;
    private float _timeLimit = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _time = Time.deltaTime;
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
