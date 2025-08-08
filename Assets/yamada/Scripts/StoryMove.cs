using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StoryMove : MonoBehaviour
{
    [SerializeField] private Text _storyText;
    [SerializeField] private float _moveCoordinate = 20f;
    [SerializeField] private float _moveDuration = 10f;
    private Tween _moveStoryTween;

    void Start()
    {
        RectTransform _moveStoryText = _storyText.rectTransform;

        _moveStoryTween = _moveStoryText.DOAnchorPosY(_moveCoordinate, _moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _moveStoryTween.Kill();
            });
    }
}
