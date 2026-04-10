using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Vector3 originalScale;

    public float hoverScale = 1.1f;
    public float clickScale = 0.95f;
    private PacmanSelector selector;

    void Awake()
    {
        selector = FindObjectOfType<PacmanSelector>();
    }

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;

        selector.MoveTo(transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;

        selector.ClearTarget(transform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = originalScale * clickScale;
    }

}