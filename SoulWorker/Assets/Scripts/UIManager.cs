using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UIManager>();

            return instance;
        }
    }

    [SerializeField] private Image hpBar;   // 체력바
    [SerializeField] private Text hpText;   // 체력 텍스트


    public void UpdateHpTxt(float currentHp, float maxHp)
    {
        hpText.text = currentHp + "/" + maxHp;
        hpBar.fillAmount = currentHp / maxHp;
    }
}
