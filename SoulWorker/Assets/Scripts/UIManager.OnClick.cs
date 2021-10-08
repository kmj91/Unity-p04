using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    public void OnClickCloseCharacterinfo()
    {
        m_characterinfo.gameObject.SetActive(false);
    }

    public void OnClickCloseSkillinfo()
    {
        Debug.Log("클릭");
    }
}
