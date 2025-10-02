using UnityEngine;
using TMPro;

public class ResultDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _resultText; // 0: 火加減, 1: 味付け, 2: かき混ぜ, 3: 総合
    [SerializeField] private GameObject[] resultPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TextMeshProUGUI _overallResultText;
    [SerializeField] private AudioClip goodResultClip;
    [SerializeField] private AudioClip badResultClip;

    private int total;

    private void Start()
    {
        int cooking = GameManager.Instance.cookingScore;
        int seasoning = GameManager.Instance.seasoningScore;
        int mixing = GameManager.Instance.mixingScore;
        total = GameManager.Instance.totalScore;

        _resultText[0].text = $"火加減 : {GetScoreText(cooking)}";
        _resultText[1].text = $"味付け : {GetScoreText(seasoning)}";
        _resultText[2].text = $"かき混ぜ : {GetScoreText(mixing)}";
        _resultText[3].text = $"総合 : {total}点";

        ShowResult(total);
        // _audioSource.clip = _scoreSounds[total];
        //_audioSource.Play();
    }

    void ShowResult(int totalScore)
    {
        totalScore = total;
        string result;

        int prefabIndex;
        if (totalScore <= 2)
        {
            prefabIndex = 0;
            result = "失敗";
            AudioManager.Instance.PlaySFX(badResultClip);
        }
        else if (totalScore <= 4)
        {
            prefabIndex = 1;
            result = "普通";
            AudioManager.Instance.PlaySFX(goodResultClip);

        }
        else
        {
            prefabIndex = 2;
            result = "完璧";
            AudioManager.Instance.PlaySFX(goodResultClip);
        }
        // Prefab instancie from index
        GameObject prefab = Instantiate(resultPrefabs[prefabIndex], spawnPoint.position, Quaternion.identity, spawnPoint);

        if (_overallResultText != null)
            _overallResultText.text = result;
    }

    private string GetScoreText(int score)
    {
        return score switch
        {
            0 => "残念",
            1 => "良し",
            2 => "完璧",
            _ => "不明"
        };
    }
}
