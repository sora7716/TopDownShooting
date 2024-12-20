Shader "Unlit/09_PostEffect"
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
         sampler2D _MainTex;//必ず_MainTexとする
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
         return fixed4(0,col.g,0,1);
         }
         ENDCG
      }
   }
}
