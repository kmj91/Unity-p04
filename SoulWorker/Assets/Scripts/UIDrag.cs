using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
        
    }

    private void SetDraggedPosition(PointerEventData eventData)
    {
        Debug.Log(m_DraggingWindow.anchoredPosition + ", " + eventData.position);

        m_DraggingWindow.anchoredPosition -= m_DraggingPlane - eventData.position;
        m_DraggingPlane = eventData.position;
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
}
