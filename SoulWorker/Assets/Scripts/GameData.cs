using UnityEngine;

using System.IO;
using MyStruct;

public class GameData : Singleton<GameData>
{
    private GameData() { }


    private void Awake()
    {
        SingletonInit(this, true);

        // 스킬 정보 로드
        LoadSkillData();
    }

    // 스킬 정보 로드
    public void LoadSkillData()
    {
        // 스킬 정보 파일 읽기
        string filePath = Path.Combine(Application.streamingAssetsPath, "HaruSkillInfo.txt");
        string fileText = Utility.ReadText(filePath);
        string skillInfo = "";

        // 모든 스킬 정보 읽어오기
        while (Utility.Parser_GetNextArea(ref fileText, out skillInfo))
        {
            AbilityData data = new AbilityData();
            if (!Utility.Parser_GetValue_Float(skillInfo, "atk", out data.amount))
                return;
        }
    }
}
