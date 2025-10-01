using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
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
        Application.Quit();
    }
}
