using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField] private Text[] _resultText;
    [SerializeField] private float _fadeDuration = 0f;
    [SerializeField] private float _fadeInterval = 0.5f;

    private void Start()
    {
        SetText();
        TextDisplay();
    }

    private void SetText()
    {
        //_resultText[0] = "火加減：" +; 
        //_resultText[1] = "味付け：" +;
        //_resultText[2] = "かきまぜ；" +;
        //_resultText[3] = "総合得点：" +;
    }

    private void TextDisplay()
    {
        Sequence sequence = DOTween.Sequence();

        foreach(var resultText in _resultText)
        {
            Color color = resultText.color;
            color.a = 0f;
            resultText.color = color;

            sequence.Append(resultText.DOFade(1f, _fadeDuration))
                .AppendInterval(_fadeInterval);
        }
    }
}
