Shader "Example/Normal Extrusion" { Properties {

// color is part of the backface shader

 _Color ("Backface Color", Color) = (.1,.2,.5,0.5)  
 _Amount ("Extrusion Amount", float) = 0.05
 
} SubShader {

	Tags {"Queue" = "Geometry-10" }


// backface shader part

ZWrite Off Lighting Off Tags {Queue=Transparent} 


ZWrite On

ColorMask 0
Pass {}

	Pass { Cull Front	 
	
	}

 CGPROGRAM
 #pragma surface surf Lambert vertex:vert
 
 float4 _Color;
 float _Amount;

 struct Input {
   float4 color : COLOR;
 };
 
 void vert (inout appdata_full v) {
   v.vertex.y +=  _Amount;
 }
 
 void surf (Input IN, inout SurfaceOutput o) {
     o.Emission = _Color;
 }

ENDCG

}


 
  
    Fallback "Diffuse"}