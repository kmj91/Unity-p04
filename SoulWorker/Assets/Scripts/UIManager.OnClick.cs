using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    public void OnClickCloseCharacterinfo()
    {
        m_characterinfo.gameObject.SetActive(false);
    }
}
