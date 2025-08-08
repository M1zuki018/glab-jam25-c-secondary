using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StoryMove : MonoBehaviour
{
    [SerializeField] private Image _storyImage;
    [SerializeField] private float _moveCoordinate = 20f;
    [SerializeField] private float _moveDuration = 10f;
    private Tween _moveStoryTween;

    void Start()
    {
        RectTransform _moveStoryImage = _storyImage.rectTransform;

        _moveStoryTween = _moveStoryImage.DOAnchorPosY(_moveCoordinate, _moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _moveStoryTween.Kill();
            });
    }
}
