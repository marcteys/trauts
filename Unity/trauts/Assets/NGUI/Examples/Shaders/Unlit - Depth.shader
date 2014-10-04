Shader "Unlit/Depth"
{
	SubShader
	{
		Lod 200

		Tags
		{
			"Queue" = "Geometry+1"
			"RenderType"="Opaque"
		}
		
		Pass
        {
            ZWrite On
            ZTest LEqual
            ColorMask 0
        }
	}
}