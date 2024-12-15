Shader "Unlit/NoiseScan"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 worldPosition : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float _Speed = -0.2;  // ノイズのスクロール速度
                float _Width = 0.01; // ノイズがかかる幅
                float _Power = 0.5;  // 揺らめき具合
                
                // ノイズの開始と終了時間を計算
                float sbTime = frac(_Time.y * _Speed); // 0.0〜1.0の範囲に正規化
                float seTime = sbTime + _Width;
                
                // UV座標を揺らめきで変化
                float2 uv = float2(
                    i.uv.x + sin(smoothstep(sbTime, seTime, i.uv.y) * 2.0 * 3.14159) * _Power,
                    i.uv.y
                );
                
                // テクスチャの色を取得
                fixed4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}