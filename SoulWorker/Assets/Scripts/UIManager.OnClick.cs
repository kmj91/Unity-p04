using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    public void OnClickCloseCharacterinfo()
    {
        m_characterinfo.gameObject.SetActive(false);
    }

    public void OnClickCloseSkillinfo()
    {
        m_skillinfo.gameObject.SetActive(false);
    }
}
