Shader "Custom/UnlitStandard"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        //_MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
		CGPROGRAM
			#pragma surface surf Lambert
			
			struct Input
			{
				float2 uvMainTex;
			};

		fixed4 _Color;

		void surf(Input IN, inout SurfaceOutput o)
		{
			//o.Albedo = _Color.rgb;
			o.Emission = _Color.rgb;
		}

        ENDCG
    }
}
