Shader "Trauts\UnlitColor" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
    }
   
    SubShader {
        Tags { "RenderType"="Transparent"  "Queue" = "Geometry+2"}
        LOD 100
        Pass {
            Lighting Off
            ZWrite On
            Cull Off
            
            SetTexture[_] {
                constantColor [_Color]
                Combine constant
            }
        }
    }
}