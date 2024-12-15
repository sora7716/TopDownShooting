Shader "Unlit/11_ITU-R_REC_BT.709"
{
   Properties
   {
     _MainTex("MainTex",2D)=""{}
   }

   SubShader 
   {
      Pass
      {
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         #include "UnityCG.cginc"

         struct appdata
         {
            float4 vertex:POSITION;
            float2 uv:TEXCOORD0;
         };
         struct v2f
         {
            float4 vertex:SV_POSITION;
            float2 uv :TEXCOORD0;
         };
         sampler2D _MainTex;//•K‚¸_MainTex‚Æ‚·‚é
         float4 _MainTex_ST;
         v2f vert(appdata v)
         {
            v2f o;
            o.vertex=UnityObjectToClipPos(v.vertex);
            o.uv=v.uv;
            return o;
         }
         fixed4 frag(v2f i):SV_Target
         {
         fixed4 col=tex2D(_MainTex,i.uv);
         float grayScale= dot(col,float3(0.2126,0.7152,0.722));
         return fixed4(grayScale,grayScale,grayScale,1);
         }
         ENDCG
      }
   }
}
