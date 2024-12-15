Shader "Unlit/08_LimeLight"
{
   Properties
    {
        _MainTex("Texture", 2D) = "white" {}//テクスチャ
        _Color("Main Color", Color) = (1,1,1,1)//色
        _SpecularThreshold("Specular Threshold", Range(0, 1)) = 0 // 名前修正と範囲修正
        _DiffuseThreshold("Diffuse Threshold", Range(0, 1)) = 0 // 名前修正と範囲修正
        _Edge("Edge",Range(0,1)) = 0.05//ぼやけさせる強さ
        _LimPower("Lim Power",Range(0.1,4)) = 2.0//リムライトの強さ
        _LimColor("Lim Color",Color) = (1,1,1,1)//リムライトのカラー
        _LimThreshold("Lim Threshold",Range(0,1)) = 0.5//リムライトの輪郭線の強さ
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
            fixed4 _Color;
            float _SpecularThreshold;
            float _DiffuseThreshold;
            float _Edge;
            float _LimPower;
            fixed4 _LimColor; 
            float _LimThreshold;

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

                 // テクスチャ
                float2 tiling = _MainTex_ST.xy;
                float2 offset = _MainTex_ST.zw;
                _Color = tex2D(_MainTex, i.uv * tiling + offset)*_Color;

                // 環境光
                fixed4 ambient = _Color * 0.3 * _LightColor0;

                // ライトベクトルと視線ベクトル
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 eyeDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPosition);

                // 法線の正規化
                float3 normal = normalize(i.normal);
                
                //リムライトの計算
                float lim =1.0 - saturate(dot(eyeDir,normal));
                lim = step(_LimThreshold,pow(lim,_LimPower));
                fixed4 limColor=_LimColor*lim;

                // 拡散反射光 (トゥーン風に段階化)
                float intensity = saturate(dot(normal, lightDir)); 
                intensity = smoothstep(_Edge,_Edge + _DiffuseThreshold,intensity);//閾値で拡散光を段階化
                fixed4 diffuse = _Color * intensity * _LightColor0;

                // 鏡面反射光 (トゥーン風に段階化)
                float3 reflectDir = reflect(-lightDir,normal);
                float specularIntensity = pow(saturate(dot(reflectDir, eyeDir)), 20);
                specularIntensity = smoothstep(_Edge,_Edge + _SpecularThreshold, specularIntensity); // 閾値で鏡面反射を段階化
                fixed4 specular = _LightColor0 * specularIntensity;

                // 最終的な色の計算
                fixed4 limLight = ambient + diffuse + specular;
                limLight.rgb+=limColor.rgb;
                return limLight;
            }
            ENDCG
        }
    }
}
