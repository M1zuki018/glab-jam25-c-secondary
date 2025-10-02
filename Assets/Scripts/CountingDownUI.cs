using UnityEngine;
using TMPro;
using System.Collections;

public class CountingDownUI : MonoBehaviour
{
    public static CountingDownUI Instance;

    public TMP_Text countdownText;
    public float bounceScale = 1.5f;
    public float animDuration = 0.3f;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public IEnumerator PlayCountdown()
    {
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(true);

        yield return AnimateNumber("3");
        yield return AnimateNumber("2");
        yield return AnimateNumber("1");
        yield return AnimateNumber("GO!", true);

        countdownText.gameObject.SetActive(false);
    }

    private IEnumerator AnimateNumber(string text, bool quick = false)
    {
        countdownText.text = text;
        countdownText.transform.localScale = Vector3.zero;

        float t = 0f;
        while (t < animDuration)
        {
            t += Time.deltaTime;
            float scale = Mathf.SmoothStep(0f, bounceScale, t / animDuration);
            countdownText.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        t = 0f;
        while (t < animDuration)
        {
            t += Time.deltaTime;
            float scale = Mathf.SmoothStep(bounceScale, 1f, t / animDuration);
            countdownText.transform.localScale = Vector3.one * scale;
            yield return null;
        }

        yield return new WaitForSeconds(quick ? 0.3f : 0.5f);
    }
}
