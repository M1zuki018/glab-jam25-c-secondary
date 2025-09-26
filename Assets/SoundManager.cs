using UnityEngine;


public class SoundManager : MonoBehaviour
{
    private static SoundManager Instance;
    [SerializeField] private AudioClip BGM;
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            source = GetComponent<AudioSource>();
            if (BGM != null)
            {
                source.clip = BGM;
                source.Play();
            }
        }
    }
}
