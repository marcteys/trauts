Shader "Trauts/UnlitColor" {
   	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
	}
 
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "AllowProjectors"="False" }
 
		blend SrcAlpha OneMinusSrcAlpha
 		cull Off
		CGPROGRAM
		#pragma surface surf NoLighting
 
		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			return fixed4(s.Albedo, s.Alpha);
		}
 
		sampler2D _MainTex;
 
		struct Input
		{
			float2 uv_MainTex;
		};
 
		float4 _Color;

		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Albedo = _Color.rgb ;
			o.Alpha =_Color.a;
		}
		ENDCG
	} 
	FallBack "Unlit"

}