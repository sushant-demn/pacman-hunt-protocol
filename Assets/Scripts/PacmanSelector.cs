using UnityEngine;
using System.Collections;

public class PacmanSelector : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Transform defaultButton;

    private Vector2 targetPosition;
    private bool hasTarget = false;

    private Transform currentTarget;
    private Coroutine clearRoutine;

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
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

        rect.anchoredPosition = Vector2.MoveTowards(
            rect.anchoredPosition,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    public void MoveTo(Transform button)
    {
        if (button == null) return;

        if (clearRoutine != null)
        {
            StopCoroutine(clearRoutine);
            clearRoutine = null;
        }

        gameObject.SetActive(true);

        RectTransform btn = button.GetComponent<RectTransform>();
        RectTransform myRect = GetComponent<RectTransform>();

        // ✅ LEFT EDGE instead of center
        Vector3 leftEdge = new Vector3(btn.rect.xMin, btn.rect.center.y, 0);
        Vector3 worldPos = btn.TransformPoint(leftEdge);

        Camera cam = Camera.main;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, worldPos);

        RectTransform canvasRect = myRect.parent as RectTransform;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPoint,
            cam,
            out localPoint
        );

        // ✅ smaller offset
        targetPosition = localPoint + new Vector2(-50f, 0);

        myRect.anchoredPosition = targetPosition;
    }

    public void ClearTarget(Transform button)
    {
        if (!gameObject.activeInHierarchy) return;
        if (currentTarget != button) return;

        if (clearRoutine != null)
        {
            StopCoroutine(clearRoutine);
        }

        clearRoutine = StartCoroutine(ClearAfterDelay(button));
    }

    IEnumerator ClearAfterDelay(Transform button)
    {
        yield return null;

        if (currentTarget != button) yield break;

        hasTarget = false;
        currentTarget = null;
        clearRoutine = null;

        gameObject.SetActive(false);
    }

    float GetOffset(RectTransform button)
    {
        return button.rect.width * 0.6f;
    }
}