using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDropPreset : MonoBehaviour, IDropHandler
{
    private Image m_presetSlot;     // 프리셋 슬롯 이미지

    public void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<Image>(); ;
        if (item != null)
        {
            m_presetSlot.sprite = item.sprite;
            m_presetSlot.enabled = true;
        }
    }


    public void Start()
    {
        m_presetSlot = GetComponent<Image>();
    }
}
