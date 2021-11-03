using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using MyEnum;

public class UIDropPreset : MonoBehaviour, IDropHandler
{
    public int m_column;    // 열
    public int m_row;       // 행

    private Image m_presetSlot;     // 프리셋 슬롯 이미지

    public void OnDrop(PointerEventData eventData)
    {
        var item = eventData.pointerDrag.GetComponent<Image>();
        if (item == null)
            return;

        int skillNum = eventData.pointerDrag.GetComponent<UISkillIconDrag>().m_skillNum;
        // 스킬 번호 확인
        if ((int)HaruSkill.FirstBlade > skillNum && (int)HaruSkill.None <= skillNum)
            return;

        m_presetSlot.sprite = item.sprite;
        m_presetSlot.enabled = true;

        // 스킬 프리셋에 스킬 등록
        UIManager.Instance.m_haruInfo.SetSkillPreset((HaruSkill)skillNum, m_column, m_row);
    }


    private void Start()
    {
        m_presetSlot = GetComponent<Image>();
    }
}
