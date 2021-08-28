using UnityEngine;

public partial class HaruInfo : PlayerInfo
{
    // 텍스처 마스크 검사
    public void CheckTextureMask()
    {
        // 얼굴 텍스처 마스크 확인
        var materials = faceRenderer.materials;
        var size = materials.Length;
        for (int i=0; i < size; ++i)
        {
            if (materials[i].name.Contains("PC_A_Parts_Default_Face_02_Mask_01"))
            {
                // 텍스처 생성
                Texture2D faceTexture = new Texture2D(faceMask.width, faceMask.height);
                for (int y = 0; y < faceMask.height; ++y)
                {
                    for (int x = 0; x < faceMask.width; ++x)
                    {
                        Color mask = faceMask.GetPixel(x, y);
                        // R 값 원본 색상
                        float fPer = (1 - mask.r) * 1.5f + mask.r;
                        Color outColor = skinColor * fPer;
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


        // 바디 텍스처 마스크 확인
        materials = bodyRenderer.materials;
        size = materials.Length;
        for (int i = 0; i < size; ++i)
        {
            if (materials[i].name.Contains("PC_A_Parts_Default_Body_03_Mask_01"))
            {
                // 텍스처 생성
                Texture2D bodyTexture = new Texture2D(bodyMask.width, bodyMask.height);
                for (int y = 0; y < bodyMask.height; ++y)
                {
                    for (int x = 0; x < bodyMask.width; ++x)
                    {
                        Color mask = bodyMask.GetPixel(x, y);
                        // R 값 원본 색상
                        float fPer = (1 - mask.r) * 1.5f + mask.r;
                        Color outColor = skinColor * fPer;
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


    }
}
