using UnityEngine;

public partial class HaruInfo : PlayerInfo
{
    // 텍스처 마스크 검사
    public void CheckTextureMask()
    {
        // 헤어 텍스처 마스크 확인
        var materials = m_hairRenderer.materials;
        var size = materials.Length;
        for (int i = 0; i < size; ++i)
        {
            if (materials[i].name.Contains("Mask"))
            {
                // 텍스처 생성
                Texture2D hairTexture = new Texture2D(m_hairMask.width, m_hairMask.height);
                for (int y = 0; y < m_hairMask.height; ++y)
                {
                    for (int x = 0; x < m_hairMask.width; ++x)
                    {
                        Color mask = m_hairMask.GetPixel(x, y);
                        // R 값 원본 색상
                        float fPer = (1 - mask.r) * 1.5f + mask.r;
                        Color outColor = m_hiarColor * fPer;
                        // G 값 하이라이트 
                        fPer = mask.g * 1.5f + (1 - mask.g);
                        outColor = outColor * fPer;
                        hairTexture.SetPixel(x, y, outColor);
                    }
                }
                hairTexture.Apply();
                // 삽입
                materials[i].mainTexture = hairTexture;

                break;
            }
        }

        // 얼굴 텍스처 마스크 확인
        materials = m_faceRenderer.materials;
        size = materials.Length;
        for (int i=0; i < size; ++i)
        {
            if (materials[i].name.Contains("PC_A_Parts_Default_Face_02_Mask_01"))
            {
                // 텍스처 생성
                Texture2D faceTexture = new Texture2D(m_faceMask.width, m_faceMask.height);
                for (int y = 0; y < m_faceMask.height; ++y)
                {
                    for (int x = 0; x < m_faceMask.width; ++x)
                    {
                        Color mask = m_faceMask.GetPixel(x, y);
                        // R 값 원본 색상
                        float fPer = (1 - mask.r) * 1.5f + mask.r;
                        Color outColor = m_skinColor * fPer;
                        // G 값 하이라이트 
                        fPer = mask.g * 1.5f + (1 - mask.g);
                        outColor = outColor * fPer;
                        faceTexture.SetPixel(x, y, outColor);
                    }
                }
                faceTexture.Apply();
                // 삽입
                materials[i].mainTexture = faceTexture;

                break;
            }
        }

        // 상의 텍스처 마스크 확인
        materials = m_bodyRenderer.materials;
        size = materials.Length;
        for (int i = 0; i < size; ++i)
        {
            if (materials[i].name.Contains("PC_A_Parts_Default_Body_03_Mask_01"))
            {
                // 텍스처 생성
                Texture2D bodyTexture = new Texture2D(m_bodyMask.width, m_bodyMask.height);
                for (int y = 0; y < m_bodyMask.height; ++y)
                {
                    for (int x = 0; x < m_bodyMask.width; ++x)
                    {
                        Color mask = m_bodyMask.GetPixel(x, y);
                        // R 값 원본 색상
                        float fPer = (1 - mask.r) * 1.5f + mask.r;
                        Color outColor = m_skinColor * fPer;
                        // G 값 하이라이트 
                        fPer = mask.g * 1.5f + (1 - mask.g);
                        outColor = outColor * fPer;
                        bodyTexture.SetPixel(x, y, outColor);
                    }
                }
                bodyTexture.Apply();
                // 삽입
                materials[i].mainTexture = bodyTexture;

                break;
            }
        }

        // 손 텍스처 마스크 확인
        materials = m_handsRenderer.materials;
        size = materials.Length;
        for (int i = 0; i < size; ++i)
        {
            if (materials[i].name.Contains("PC_A_Parts_Default_Body_03_Mask_01"))
            {
                // 텍스처 생성
                Texture2D handsTexture = new Texture2D(m_bodyMask.width, m_bodyMask.height);
                for (int y = 0; y < m_bodyMask.height; ++y)
                {
                    for (int x = 0; x < m_bodyMask.width; ++x)
                    {
                        Color mask = m_bodyMask.GetPixel(x, y);
                        // R 값 원본 색상
                        float fPer = (1 - mask.r) * 1.5f + mask.r;
                        Color outColor = m_skinColor * fPer;
                        // G 값 하이라이트 
                        fPer = mask.g * 1.5f + (1 - mask.g);
                        outColor = outColor * fPer;
                        handsTexture.SetPixel(x, y, outColor);
                    }
                }
                handsTexture.Apply();
                // 삽입
                materials[i].mainTexture = handsTexture;

                break;
            }
        }

        // 하의 텍스처 마스크 확인
        materials = m_pantsRenderer.materials;
        size = materials.Length;
        for (int i = 0; i < size; ++i)
        {
            if (materials[i].name.Contains("PC_A_EQ_Default_Socks_05_Mask_01"))
            {
                // 텍스처 생성
                Texture2D pantsTexture = new Texture2D(m_sockMask.width, m_sockMask.height);
                for (int y = 0; y < m_sockMask.height; ++y)
                {
                    for (int x = 0; x < m_sockMask.width; ++x)
                    {
                        Color mask = m_sockMask.GetPixel(x, y);
                        // R 값 원본 색상
                        float fPer = (1 - mask.r) * 1.5f + mask.r;
                        Color outColor = m_skinColor * fPer;
                        // G 값 하이라이트 
                        fPer = mask.g * 1.5f + (1 - mask.g);
                        outColor = outColor * fPer;
                        pantsTexture.SetPixel(x, y, outColor);
                    }
                }
                pantsTexture.Apply();
                // 삽입
                materials[i].mainTexture = pantsTexture;

                break;
            }
        }

        // 신발 텍스처 마스크 확인
        materials = m_shoessRenderer.materials;
        size = materials.Length;
        for (int i = 0; i < size; ++i)
        {
            if (materials[i].name.Contains("PC_A_EQ_Default_Socks_05_Mask_01"))
            {
                // 텍스처 생성
                Texture2D shoessTexture = new Texture2D(m_sockMask.width, m_sockMask.height);
                for (int y = 0; y < m_sockMask.height; ++y)
                {
                    for (int x = 0; x < m_sockMask.width; ++x)
                    {
                        Color mask = m_sockMask.GetPixel(x, y);
                        // R 값 원본 색상
                        float fPer = (1 - mask.r) * 1.5f + mask.r;
                        Color outColor = m_skinColor * fPer;
                        // G 값 하이라이트 
                        fPer = mask.g * 1.5f + (1 - mask.g);
                        outColor = outColor * fPer;
                        shoessTexture.SetPixel(x, y, outColor);
                    }
                }
                shoessTexture.Apply();
                // 삽입
                materials[i].mainTexture = shoessTexture;

                break;
            }
        }
    }
}
