using UnityEngine;

public class ResetButton : MonoBehaviour
{
    SceneLoader scene;
    public void ReplayButton()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
            GameManager.Instance = null;
        }

        scene.LoadScene("Title");
    }
}
