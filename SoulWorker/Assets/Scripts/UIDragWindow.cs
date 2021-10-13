using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragWindow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform m_DraggingWindow;

    private Vector2 m_DraggingPlane;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DraggingPlane = eventData.position;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        if (!eventData.IsPointerMoving())
            return;

        m_DraggingWindow.anchoredPosition += eventData.position - m_DraggingPlane;
        m_DraggingPlane = eventData.position;
    }
}
