Shader "Unlit/Gaussian"
{
     Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Center("Center Position", Vector) = (0.5, 0.5, 0, 0) // UV座標の中心
        _Radius("Unblurred Radius", Float) = 0.53// 中央のくっきり範囲
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 Gaussian(float2 drawUV, float2 pickUV, float sigma)
            {
                float d = distance(drawUV, pickUV);
                return exp(-(d * d) / (2 * sigma * sigma + 0.0001));
            }

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
            float4 _Center; // 中心座標 (UV)
            float _Radius;  // くっきり見える範囲

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
                float _Sigma = 0.009;
                float _StepWidth = 0.002;

                // 現在のピクセルが中心からどれくらい離れているか計算
                float distanceFromCenter = distance(i.uv, _Center.xy);

                //補間係数(0.0から1.0)距離が_Radiusに近づくとブラーが強くなる
                float weight =smoothstep(_Radius,_Radius+0.1,distanceFromCenter);

                fixed4 centerColor=tex2D(_MainTex,i.uv);

                // ブラー計算
                float totalWeight = 0;
                float4 blurColor = fixed4(0, 0, 0, 0);
                for (float py = -_Sigma * 2; py <= _Sigma * 2; py += _StepWidth)
                {
                    for (float px = -_Sigma * 2; px <= _Sigma * 2; px += _StepWidth)
                    {
                        float2 pickUV = i.uv + float2(px, py);
                        fixed4 gaussianWeight = Gaussian(i.uv, pickUV, _Sigma);
                        blurColor += tex2D(_MainTex, pickUV) * gaussianWeight;
                        totalWeight += gaussianWeight;
                    }
                }
                blurColor/=totalWeight;
                //線形補間で境目を滑らかにする
                fixed4 finalColor=lerp(centerColor,blurColor,weight);
                finalColor.a = 1;
                return finalColor;
            }
            ENDCG
        }
    }
}
