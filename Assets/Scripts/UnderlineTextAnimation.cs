using UnityEngine;
using UnityEngine.EventSystems;

public class UnderlineTextAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform underline;
    public float speed = 8f;

    private Vector3 targetScale;

    void Start()
    {
        targetScale = new Vector3(0, 1, 1);
        underline.localScale = targetScale;
    }

    void Update()
    {
        underline.localScale = Vector3.Lerp(
            underline.localScale,
            targetScale,
            Time.deltaTime * speed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = new Vector3(1, 1, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = new Vector3(0, 1, 1);
    }
}