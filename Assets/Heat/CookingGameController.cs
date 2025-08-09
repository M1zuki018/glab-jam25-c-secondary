using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class CookingGameController : MonoBehaviour
{
    [Header("References")]
    public CookingCountdownUI countdownUI;
    public CookingBarController barController;
    public CursorController cursorController;
    public TMP_Text finishText;

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
        yield return countdownUI.PlayCountdown();
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

        finishText.gameObject.SetActive(true);
        finishText.text = "FINISH!\nAppuyez sur une touche";

        cursorController.Stop();
        playerInput.DeactivateInput();

        animationBase.SetActive(false);
        animationEnd.SetActive(true);

        StartCoroutine(WaitForContinue());
    }

    private IEnumerator WaitForContinue()
    {
        while (!Keyboard.current.anyKey.wasPressedThisFrame)
            yield return null;

        float cursorX = cursorController.GetCursorPosition();
        Debug.Log($"Cursor X = {cursorX}");


        int score = barController.GetPoints(cursorController.GetCursorPosition());
        Debug.Log($"Score gagné : {score}");

        // TODO : stocker score dans GameManager ici
        GameManager.Instance.AddScore(score);
        finishText.text = "Loading next screen...";
        Debug.Log("Passage à l'écran suivant !");
        TransitionManager.Instance.StartTransition("Season_Test");
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
