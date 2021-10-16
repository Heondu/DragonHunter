using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform canvas;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;

    private Vector3 stickFirstPos;
    private Vector3 joyVec = Vector3.zero;
    private float radius;
    private bool isDrag = false;

    public float Horizontal => joyVec.x;
    public float Vertical => joyVec.y;

    private void Start()
    {
        radius = background.sizeDelta.y * 0.4f;
        stickFirstPos = handle.transform.position;

        float canvasScaleX = canvas.localScale.x;
        radius *= canvasScaleX;

        background.gameObject.SetActive(false);
        handle.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = eventData.position;

        if (isDrag == false)
        {
            isDrag = true;

            background.position = pos;
            stickFirstPos = pos;

            background.gameObject.SetActive(true);
            handle.gameObject.SetActive(true);
        }

        joyVec = (pos - stickFirstPos).normalized;

        float distance = Vector3.Distance(pos, stickFirstPos);

        if (distance < radius)
        {
            handle.position = stickFirstPos + joyVec * distance;
        }
        else
        {
            handle.position = stickFirstPos + joyVec * radius;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        background.gameObject.SetActive(false);
        handle.gameObject.SetActive(false);
        
        handle.position = stickFirstPos;
        joyVec = Vector3.zero;
    }
}
