using UnityEngine;

public class MouseRotationAnimationController : MonoBehaviour
{
    [SerializeField] private MouseRotationTracker rotationTracker;
    [SerializeField] private Animator animator;
    [SerializeField] private string parameterCountName = "RotationCount";
    [SerializeField] private string triggerIncrementName = "RotationIncremented";

    private int lastRotationCount = 0;

    private void OnEnable()
    {
        if (rotationTracker != null)
        {
            rotationTracker.OnRotationUpdated += OnRotationUpdated;
        }
    }

    private void OnDisable()
    {
        if (rotationTracker != null)
        {
            rotationTracker.OnRotationUpdated -= OnRotationUpdated;
        }
    }

    private void OnRotationUpdated(int rotationCount, float remainingTime)
    {
        animator.SetInteger(parameterCountName, rotationCount);

        if (rotationCount > lastRotationCount)
        {
            animator.SetTrigger(triggerIncrementName);
        }

        lastRotationCount = rotationCount;
    }
}
