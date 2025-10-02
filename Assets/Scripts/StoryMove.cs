using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StoryMove : MonoBehaviour
{
    [SerializeField] private Text _storyText;
    [SerializeField] private float _moveYCoordinate = 20f;
    [SerializeField] private float _moveDuration = 10f;

    [SerializeField] private float _moveZCoordinate = -10f;

    void Start()
    {
        RectTransform _moveStoryText = _storyText.rectTransform;
        var moveStoryTween = DOTween.Sequence();

        moveStoryTween.Append(_moveStoryText.DOAnchorPosY(_moveYCoordinate, _moveDuration))
            .Join(_moveStoryText.DOScale(_moveZCoordinate, _moveDuration))
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                moveStoryTween.Kill();
            });
    }
}
