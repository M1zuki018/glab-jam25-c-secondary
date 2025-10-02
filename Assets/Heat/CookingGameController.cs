using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class CookingGameController : MonoBehaviour
{
    [Header("References")]
    public CookingBarController barController;
    public CursorController cursorController;
    public TMP_Text finishText;

    [SerializeField] private AudioClip cookingClip;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioSource audioSource;

    public GameObject animationBase;
    public GameObject animationEnd;

    public float cookingTime = 5f;

    private float timer;
    public TMP_Text timerText;
    private bool timerRunning = false;
    private bool gameActive = false;
    private PlayerInput playerInput;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        finishText.gameObject.SetActive(false);
        cursorController.Initialize(); // reset position + setup boundaries

        animationBase.SetActive(true);
        animationEnd.SetActive(false);

        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        yield return CountingDownUI.Instance.PlayCountdown();
        AudioManager.Instance.PlayLoopSFX(cookingClip, 0.55f);
        if (audioSource != null)
        {
            audioSource.clip = fireClip;
            audioSource.volume = 0.3f;
            audioSource.Play();
        }
        gameActive = true;
        timer = cookingTime;
    }

    private void Update()
    {
        if (!gameActive) return;

        if (cursorController.IsMoving())
            timerRunning = true;

        if (timerRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndCooking();
            }
            timerText.text = Mathf.Ceil(timer).ToString();
        }
    }

    private void EndCooking()
    {
        timerRunning = false;
        gameActive = false;

        cursorController.Stop();
        playerInput.DeactivateInput();

        animationBase.SetActive(false);
        animationEnd.SetActive(true);

        Invoke("WaitForContinue", 0.5f);
    }

    private void WaitForContinue()
    {

        float cursorX = cursorController.GetCursorPosition();
        Debug.Log($"Cursor X = {cursorX}");


        int score = barController.GetPoints(cursorController.GetCursorPosition());
        Debug.Log($"Score  : {score}");

        //stock the GameManager score
        GameManager.Instance.AddCookingScore(score);
        //Stop the SFX
        AudioManager.Instance.StopLoopSFX();
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (!gameActive) return;
        cursorController.HandleMove(-1, context);
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (!gameActive) return;
        cursorController.HandleMove(1, context);
    }
}
