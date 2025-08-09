using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private CanvasGroup _fadeCanvas;
    [SerializeField] private float _fadeDuration = 2f;

    public void LoadScene(string sceneName)
    {
        _fadeCanvas.gameObject.SetActive(true);
        _fadeCanvas.alpha = 0f;

        _fadeCanvas.DOFade(1f, _fadeDuration)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(sceneName);
            });
    }
}
