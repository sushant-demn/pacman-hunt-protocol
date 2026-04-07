using UnityEngine;
using System.Collections;

public class PacmanSelector : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Transform defaultButton;

    private Vector3 targetPosition;
    private bool hasTarget = false;

    private Transform currentTarget;

    void Start()
    {
        gameObject.SetActive(false); // start hidden
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
        // SHOW when hovering
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

        // snap instantly first time (prevents sliding from weird position)
        transform.position = targetPosition;
    }

    public void ClearTarget(Transform button)
    {
        if (currentTarget == button)
        {
            StartCoroutine(ClearAfterDelay(button));
        }
    }



    IEnumerator ClearAfterDelay(Transform button)
    {
        yield return null; // wait 1 frame (prevents flicker when switching buttons)

        if (currentTarget == button)
        {
            hasTarget = false;
            currentTarget = null;

            // HIDE when no button is active
            gameObject.SetActive(false);
        }
    }

    float GetOffset(Transform button)
    {
        RectTransform rt = button.GetComponent<RectTransform>();
        return rt.rect.width * 0.62f;
    }
}