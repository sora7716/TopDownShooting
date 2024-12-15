Shader "Unlit/08_LimeLight"
{
   Properties
    {
        _MainTex("Texture", 2D) = "white" {}//�e�N�X�`��
        _Color("Main Color", Color) = (1,1,1,1)//�F
        _SpecularThreshold("Specular Threshold", Range(0, 1)) = 0 // ���O�C���Ɣ͈͏C��
        _DiffuseThreshold("Diffuse Threshold", Range(0, 1)) = 0 // ���O�C���Ɣ͈͏C��
        _Edge("Edge",Range(0,1)) = 0.05//�ڂ₯�����鋭��
        _LimPower("Lim Power",Range(0.1,4)) = 2.0//�������C�g�̋���
        _LimColor("Lim Color",Color) = (1,1,1,1)//�������C�g�̃J���[
        _LimThreshold("Lim Threshold",Range(0,1)) = 0.5//�������C�g�̗֊s���̋���
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

                 // �e�N�X�`��
                float2 tiling = _MainTex_ST.xy;
                float2 offset = _MainTex_ST.zw;
                _Color = tex2D(_MainTex, i.uv * tiling + offset)*_Color;

                // ����
                fixed4 ambient = _Color * 0.3 * _LightColor0;

                // ���C�g�x�N�g���Ǝ����x�N�g��
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 eyeDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPosition);

                // �@���̐��K��
                float3 normal = normalize(i.normal);
                
                //�������C�g�̌v�Z
                float lim =1.0 - saturate(dot(eyeDir,normal));
                lim = step(_LimThreshold,pow(lim,_LimPower));
                fixed4 limColor=_LimColor*lim;

                // �g�U���ˌ� (�g�D�[�����ɒi�K��)
                float intensity = saturate(dot(normal, lightDir)); 
                intensity = smoothstep(_Edge,_Edge + _DiffuseThreshold,intensity);//臒l�Ŋg�U����i�K��
                fixed4 diffuse = _Color * intensity * _LightColor0;

                // ���ʔ��ˌ� (�g�D�[�����ɒi�K��)
                float3 reflectDir = reflect(-lightDir,normal);
                float specularIntensity = pow(saturate(dot(reflectDir, eyeDir)), 20);
                specularIntensity = smoothstep(_Edge,_Edge + _SpecularThreshold, specularIntensity); // 臒l�ŋ��ʔ��˂�i�K��
                fixed4 specular = _LightColor0 * specularIntensity;

                // �ŏI�I�ȐF�̌v�Z
                fixed4 limLight = ambient + diffuse + specular;
                limLight.rgb+=limColor.rgb;
                return limLight;
            }
            ENDCG
        }
    }
}
