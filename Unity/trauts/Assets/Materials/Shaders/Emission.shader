Shader "Trauts/Emission" {
 
    Properties {
        _EmissionLM ("Emission (Lightmapper)", Float) = 1
    }
 
    SubShader {
 
        Tags { "RenderType"="Opaque" }
        LOD 200
 
        CGPROGRAM
        #pragma surface surf Lambert
 
        struct Input {
            float4 color : COLOR; // Get vertex color
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = IN.color.rgb;
            o.Emission = IN.color.rgb; // Always results in white?!?
        }
 
        ENDCG
 
    } 
 
}
