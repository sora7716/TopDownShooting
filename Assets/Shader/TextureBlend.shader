Shader "Unlit/TextureBlend"
{
   Properties
   {
  
     _MainTex("Main_Texture",2D)="white"{}
     _SubTex("Sub_Texture",2D)="white"{}
     _MaskTex("Mask_Texture",2D)="white"{}
   }

   SubShader 
   {
       Tags
       {
           "Queue"="Transparent"
       }
       Blend SrcAlpha OneMinusSrcAlpha
      Pass
      {
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
         #include "UnityCG.cginc"

         fixed Gaussian(float2 drawUV,float2 pickUV,float sigma)
         {
             float d=distance(drawUV,pickUV);
             return exp(-(d*d)/(2*sigma*sigma));
         }

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
         sampler2D _MainTex;
         sampler2D _SubTex;
         sampler2D _MaskTex;
         v2f vert(appdata v)
         {
            v2f o;
            o.vertex=UnityObjectToClipPos(v.vertex);
            o.uv=v.uv;
            return o;
         }
         fixed4 frag(v2f i):SV_Target
         {
            fixed4 main=tex2D(_MainTex,i.uv);
            fixed4 sub=tex2D(_SubTex,i.uv);
            fixed4 mask=tex2D(_MaskTex,i.uv);
            //mask‚ÌRƒ`ƒƒƒ“ƒlƒ‹‚Å”»’è
            fixed4 col=lerp(main,sub,mask.r);
            return col;
         }
         ENDCG
      }
   }
}
