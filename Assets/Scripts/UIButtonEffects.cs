using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonEffects : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    private Vector3 originalScale;

    public float hoverScale = 1.1f;
    public float clickScale = 0.95f;

    private PacmanSelector selector;


    void Awake()
    {
        selector = FindObjectOfType<PacmanSelector>();
        originalScale = transform.localScale;
    }

    void OnEnable()
    {
        transform.localScale = originalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;

        if (selector != null)
            selector.MoveTo(transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;

        if (selector != null)
            selector.ClearTarget(transform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = originalScale * clickScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;
    }
}