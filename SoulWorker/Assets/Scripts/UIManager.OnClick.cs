using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    public void OnClickCloseCharacterinfo()
    {
        m_UICharacterinfo.gameObject.SetActive(false);
    }

    public void OnClickCloseSkillinfo()
    {
        m_UISkillinfo.gameObject.SetActive(false);
    }
}
