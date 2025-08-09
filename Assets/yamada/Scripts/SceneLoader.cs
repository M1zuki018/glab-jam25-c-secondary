using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        TransitionManager.Instance.StartTransition(sceneName);
    }
}
