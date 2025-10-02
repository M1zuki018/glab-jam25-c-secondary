using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClip;
    private float buttonVolume = 0.5f;

    public void LoadScene(string sceneName)
    {
        AudioManager.Instance.PlaySFX(buttonClip, buttonVolume);
        TransitionManager.Instance.StartTransition(sceneName);
    }

    public void ReplayButton()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
            GameManager.Instance = null;
        }

        LoadScene("Title");
    }

    public void Quit()
    {
        AudioManager.Instance.PlaySFX(buttonClip, buttonVolume);

        Application.Quit();
    }
}
