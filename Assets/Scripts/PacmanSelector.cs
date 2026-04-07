using UnityEngine;
using System.Collections;

public class PacmanSelector : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Transform defaultButton;

    private Vector3 targetPosition;
    private bool hasTarget = false;

    private Transform currentTarget;
    private Coroutine clearRoutine;

    void Start()
    {
        // ✅ Initialize properly (fixes your first-hover bug)
        if (defaultButton != null)
        {
            MoveTo(defaultButton);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!hasTarget) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );


    }

    public void MoveTo(Transform button)
    {
        if (button == null) return;

        // ✅ Cancel any pending hide (CRITICAL)
        if (clearRoutine != null)
        {
            StopCoroutine(clearRoutine);
            clearRoutine = null;
        }

        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        currentTarget = button;
        hasTarget = true;

        RectTransform btn = button.GetComponent<RectTransform>();
        Vector3 worldPos = btn.TransformPoint(btn.rect.center);

        targetPosition = new Vector3(
            worldPos.x - GetOffset(button),
            worldPos.y,
            worldPos.z
        );

        // Snap instantly to avoid weird first movement
        transform.position = targetPosition;
    }

    public void ClearTarget(Transform button)
    {
        if (!gameObject.activeInHierarchy) return;
        if (currentTarget != button) return;

        // ✅ Prevent multiple coroutines stacking
        if (clearRoutine != null)
        {
            StopCoroutine(clearRoutine);
        }

        clearRoutine = StartCoroutine(ClearAfterDelay(button));
    }

    IEnumerator ClearAfterDelay(Transform button)
    {
        yield return null; // wait 1 frame

        // If user hovered another button → cancel hide
        if (currentTarget != button) yield break;

        hasTarget = false;
        currentTarget = null;
        clearRoutine = null;

        gameObject.SetActive(false);
    }

    float GetOffset(Transform button)
    {
        RectTransform rt = button.GetComponent<RectTransform>();
        return rt.rect.width * 0.62f;
    }
}