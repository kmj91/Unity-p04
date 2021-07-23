using UnityEngine;
using UnityEngine.AI;

using System.IO;

public static class Utility
{
    public static Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance, int areaMask)
    {
        var randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, areaMask);

        return hit.position;
    }

    public static float GetRandomNormalDistribution(float mean, float standard)
    {
        var x1 = Random.Range(0f, 1f);
        var x2 = Random.Range(0f, 1f);
        return mean + standard * (Mathf.Sqrt(-2.0f * Mathf.Log(x1)) * Mathf.Sin(2.0f * Mathf.PI * x2));
    }

    // 텍스트 파일 쓰기
    public static void WriteText(string filePath, string message)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(filePath));

        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        FileStream fileStream
            = new FileStream(filePath, FileMode.Append, FileAccess.Write);

        StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.Unicode);

        writer.WriteLine(message);
        writer.Close();
    }

    // 텍스트 파일 읽기
    public static string ReadText(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        string value = "";

        if (fileInfo.Exists)
        {
            StreamReader reader = new StreamReader(filePath);
            value = reader.ReadToEnd();
            reader.Close();
        }

        return value;
    }

    public static bool Parser_GetArea(string fileText, string areaName, out string data)
    {
        int length = fileText.Length;
        int begin = 0;
        int end = 0;

        data = "";

        while (end < length)
        {
            if (!NextWord(fileText, ref begin, ref end))
                return false;

            if (ParsingArea(fileText, areaName, ref begin, ref end, ref data))
                return true;
        }

        return false;
    }

    public static bool Parser_GetValue_Float(string text, string name, out float value)
    {
        int length = text.Length;
        int begin = 0;
        int end = 0;
        string data = "";

        value = 0f;

        while (end < length)
        {
            if (!NextWord(text, ref begin, ref end))
                return false;

            if (!NameCompare(text, name, ref begin, ref end))
            {
                if (!NextLine(text, ref begin, ref end))
                    return false;

                continue;
            }

            if (GetValue(text, ref data, ref begin, ref end))
            {
                value = System.Convert.ToSingle(data);
                return true;
            }
            else
            {
                if (!NextLine(text, ref begin, ref end))
                    return false;

                continue;
            }
        }

        return false;
    }



    private static bool NextWord(string text, ref int begin, ref int end)
    {
        // 다음 문자로
        while (text[end] == 0x20 || text[end] == 0x09 || text[end] == 0x0a || text[end] == 0x0d
            || text[end] == '"' || text[end] == '{' || text[end] == '}')
        {
            // 버퍼의 끝
            if (end == text.Length)
            {
                begin = end;
                return false;
            }

            // 다음 글자로
            ++end;
        }

        begin = end;
        return true;
    }

    private static bool NextLine(string text, ref int begin, ref int end)
    {
        while (text[end] != 0x0a && text[end] != 0x0d)
        {
            // 버퍼의 끝
            if (end == text.Length)
            {
                begin = end;
                return false;
            }

            // 다음 글자로
            ++end;
        }

        begin = end;
        return true;
    }

    private static bool EndString(string text, int begin, ref int end)
    {
        while (text[end] != 0x20 && text[end] != 0x09 && text[end] != 0x0a && text[end] != 0x0d
            && text[end] != '"')
        {
            // 버퍼의 끝
            if (end == text.Length)
                return false;

            // 다음 글자로
            ++end;
        }

        return true;
    }

    private static bool NameCompare(string text, string name, ref int begin, ref int end)
    {
        if (!EndString(text, begin, ref end))
            return false;

        // 문자열 잘라냄
        string word = text.Substring(begin, end - begin);
        begin = end;
        // 문자열이 같지 않음
        if (word.CompareTo(name) != 0)
            return false;
        
        return true;
    }

    private static bool GetValue(string text, ref string data, ref int begin, ref int end)
    {
        if (!NextWord(text, ref begin, ref end))
            return false;

        if (!EndString(text, begin, ref end))
            return false;

        // 문자열 잘라냄
        string word = text.Substring(begin, end - begin);
        // 문자열이 같지 않음
        if (word.CompareTo("=") != 0)
            return false;

        begin = end;

        if (!NextWord(text, ref begin, ref end))
            return false;

        if (!EndString(text, begin, ref end))
            return false;

        // 값 획득
        data = text.Substring(begin, end - begin);

        return true;
    }

    // 구역 파싱
    // fileText : 파싱할 텍스트 문자열
    // areaName : 파싱할 구역 이름
    // begin : 구역 시작 인덱스
    // end : 구역 끝 인덱스
    private static bool ParsingArea(string fileText, string areaName, ref int begin, ref int end, ref string data)
    {
        while (fileText[end] != 0x0a && fileText[end] != 0x0d
            && fileText[end] != '"' && fileText[end] != '{' && fileText[end] != '}')
        {
            // 버퍼의 끝
            if (end == fileText.Length)
                return false;

            // 다음 글자로
            ++end;
        }

        // 문자열 잘라냄
        string word = fileText.Substring(begin, end - begin);
        // 문자열이 같지 않음
        if (word.CompareTo(areaName) != 0)
        {
            // '}' 찾기
            while (fileText[end] != '}')
            {
                // 버퍼의 끝
                if (end == fileText.Length)
                    return false;

                // 다음 글자로
                ++end;
            }

            begin = end;
            return false;
        }

        // 문자열 구역 잘라내기
        begin = end;

        // '{' 찾기
        while (fileText[begin] != '{')
        {
            // 버퍼의 끝
            if (begin == fileText.Length)
            {
                end = begin;
                return false;
            }

            // 다음 글자로
            ++begin;
        }

        end = begin;

        // '}' 찾기
        while (fileText[end] != '}')
        {
            // 버퍼의 끝
            if (end == fileText.Length)
                return false;

            // 다음 글자로
            ++end;
        }

        // 구역 획득
        data = fileText.Substring(begin, end - begin);

        return true;
    }
}
