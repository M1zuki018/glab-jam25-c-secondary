using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    public Transform leftPanel;
    public Transform rightPanel;
    public float transitionDuration = 2f;

    private Vector2 leftStartPos;
    private Vector2 rightStartPos;
    private Vector2 leftTargetPos;
    private Vector2 rightTargetPos;

    private bool isTransitioning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //Save position
            leftStartPos = leftPanel.localPosition;
            rightStartPos = rightPanel.localPosition;

            //Targetted position
            leftTargetPos = new Vector2(0, leftStartPos.y);
            rightTargetPos = new Vector2(0, rightStartPos.y);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTransition(string nextScene)
    {
        if (!isTransitioning)
            StartCoroutine(TransitionCoroutine(nextScene));
    }

    IEnumerator TransitionCoroutine(string nextScene)
    {
        isTransitioning = true;

        // Close
        yield return SlidePanels(leftTargetPos, rightTargetPos);

        // Changement de scène TEST
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);
        while (!asyncLoad.isDone)
            yield return null;

        // Open
        yield return SlidePanels(leftStartPos, rightStartPos);

        isTransitioning = false;
    }

    IEnumerator SlidePanels(Vector2 leftTarget, Vector2 rightTarget)
    {
        float timer = 0f;
        Vector2 leftInitial = leftPanel.localPosition;
        Vector2 rightInitial = rightPanel.localPosition;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;

            leftPanel.localPosition = Vector2.Lerp(leftInitial, leftTarget, t);
            rightPanel.localPosition = Vector2.Lerp(rightInitial, rightTarget, t);

            yield return null;
        }

        leftPanel.localPosition = leftTarget;
        rightPanel.localPosition = rightTarget;
    }
}
