Shader "Custom/ShaderBackgroundUI"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _U ("U", float) = 0
        _V ("V", float) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite On ZTest Less

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _U;
            float _V;

            fixed4 frag(v2f i) : SV_Target
            {
                //i.uv += float2(_U, _V);
                fixed4 col = tex2D(_MainTex, i.uv - float2(_U, _V));
                
                // 경계선 위치 계산
                float lineY;
                if (_V < 0)
                {
                    lineY = 1 - ((_V * -1.0f) % 1);
                }
                else
                {
                    lineY = _V % 1;
                }
                
                // y축으로 갈수록 어둡게
                float gob;
                if (i.uv.y < lineY)
                {
                    gob = (1.0f - i.uv.y) - 0.3f;
                }
                else
                {
                    gob = 1.0f - i.uv.y;
                }
                col = (col * gob);

                return col;
            }
            ENDCG
        }
    }
}
