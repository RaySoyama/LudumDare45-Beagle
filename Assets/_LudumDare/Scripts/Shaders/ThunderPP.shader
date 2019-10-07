Shader "Hidden/ThunderPP"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CircleTex ("Circle Tex", 2D) = "white" {}
		_Effect("Effect Size", Float) = 1
		_TileX("X Size", Float) = 1.6
		_TileY("Y Size", Float) = 1
		_XOffSet("X offset", Range(-1,1)) = 1
		_YOffSet("y offset", Range(-1,1)) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CircleTex;
			float _Effect;
			float _TileX;
			float _TileY;

			float _XOffSet;
			float _YOffSet;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 circ = tex2D(_CircleTex, float2((i.uv.x + _XOffSet) / _TileX + ((i.uv.x + _XOffSet) / _TileX * _Effect - _Effect / 2),(i.uv.y+ _YOffSet) / _TileY + ((i.uv.y + _YOffSet) / _TileY * _Effect - _Effect / 2)));


				float check = floor(circ.a + 0.5f);


				col.g -= (col.g - col.r) * check;
				col.b -= (col.b - col.r) * check;

                return col;
            }
            ENDCG
        }
    }
}
