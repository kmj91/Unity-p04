using UnityEngine;
using UnityEngine.UI;
using MyStruct;

public class UICharacterinfo : MonoBehaviour
{
    public HaruInfo m_haruInfo;

    [SerializeField] private RectTransform m_equipmentTransform;    // 장비 창 위치
    [SerializeField] private Text m_equipmentHP;                    // 캐릭터 정보 HP
    [SerializeField] private Text m_equipmentAttack;                // 캐릭터 정보 공격력
    [SerializeField] private Text m_equipmentCritical;              // 캐릭터 정보 치명타
    [SerializeField] private Text m_equipmentAttackSpeed;           // 캐릭터 정보 공격 속도
    [SerializeField] private Text m_equipmentAccuracy;              // 캐릭터 정보 적중도
    [SerializeField] private Text m_equipmentArmourBreak;           // 캐릭터 정보 적 방어구 관통
    [SerializeField] private Text m_equipmentExtraDmgToEnemy;       // 캐릭터 정보 적 추가 피해 일반
    [SerializeField] private Text m_equipmentExtraDmgToBossNamed;   // 캐릭터 정보 적 추가 피해 네임드/보스
    [SerializeField] private Text m_equipmentStamina;               // 스태미나
    [SerializeField] private Text m_equipmentMoveSpeed;             // 이동속도
    [SerializeField] private Text m_equipmentDefense;               // 방어도
    [SerializeField] private Text m_equipmentEvade;                 // 회피도
    [SerializeField] private Text m_equipmentDamageReduction;       // 피해 감소
    [SerializeField] private Text m_equipmentCriticalResistance;    // 치명타 저항
    [SerializeField] private Text m_equipmentShorterCooldown;       // 재사용 대기시간 감소


    // 창 닫기
    public void OnClickCloseCharacterinfo()
    {
        gameObject.SetActive(false);
    }


    // 캐릭터 스텟 정보 초기화
    public void InitEquipmentStat()
    {
        PlayerData data = m_haruInfo.currentPlayerData;

        m_equipmentHP.text = data.hp.ToString();
        m_equipmentAttack.text = data.minAtk + " - " + data.maxAtk;
        m_equipmentCritical.text = data.criticalRate.ToString("F1") + "% (+" + data.criticalDamage + ")";
        m_equipmentAttackSpeed.text = (data.atkSpeed * 100f).ToString("F1") + "%";
        m_equipmentAccuracy.text = data.accuracy.ToString();
        m_equipmentArmourBreak.text = data.armourBreak.ToString("F1") + "%";
        m_equipmentExtraDmgToEnemy.text = data.extraDmgToEnemy.ToString("F1") + "%";
        m_equipmentExtraDmgToBossNamed.text = data.extraDmgToBossNamed.ToString("F1") + "%";
        m_equipmentStamina.text = data.stamina.ToString();
        m_equipmentMoveSpeed.text = data.moveSpeed.ToString("F1") + "%";
        m_equipmentDefense.text = data.defense.ToString();
        m_equipmentEvade.text = data.m_evade.ToString();
        m_equipmentDamageReduction.text = data.damageReduction.ToString("F1") + "%";
        m_equipmentCriticalResistance.text = data.criticalResistance.ToString("F1") + "%";
        m_equipmentShorterCooldown.text = data.shorterCooldown.ToString("F1") + "%";
    }

    // 장비창 트랜스폼 정보 얻어오기
    public Transform GetEquipmentTransform()
    {
        return m_equipmentTransform;
    }

    // 캐릭터 정보 창 토글
    public void ToggleCharacterinfo()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
