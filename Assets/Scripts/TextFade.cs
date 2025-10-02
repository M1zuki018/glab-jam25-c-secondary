using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Textfade : MonoBehaviour
{
    [SerializeField] private CanvasGroup _skipText;

    void Start()
    {
        _skipText.alpha = 1.0f;
        _skipText.DOFade(0f, 3f);
    }
}
