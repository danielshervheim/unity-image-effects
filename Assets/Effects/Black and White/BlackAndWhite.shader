Shader "Hidden/BlackAndWhite"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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

			int _ComputeLuminance;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			float4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);
				if (_ComputeLuminance == 1) {
					float lum = saturate(0.2126*col.r + 0.7152*col.g + 0.0722*col.b);
					return float4(lum, lum, lum, 1);
				}
				else {
					float avg = (col.r+col.g+col.b)/3.0;
					return float4(avg, avg, avg, col.a);
				}
			}
			ENDCG
		}
	}
}
