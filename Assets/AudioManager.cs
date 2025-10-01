using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource sfxLoopSource;

    [Header("BGM")]
    public AudioClip BGM;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy duplicate instances
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (BGM != null && bgmSource != null)
            {
                bgmSource.clip = BGM;
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.pitch = Random.Range(0.95f, 1.05f); // variation for making it natural
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayLoopSFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null || sfxLoopSource == null) return;

        sfxLoopSource.clip = clip;
        sfxLoopSource.volume = volume;
        sfxLoopSource.loop = true;
        sfxLoopSource.Play();
    }

    public void StopLoopSFX()
    {
        if (sfxLoopSource != null)
        {
            sfxLoopSource.Stop();
            Destroy(sfxLoopSource);
            sfxLoopSource = null;
        }
    }
}
