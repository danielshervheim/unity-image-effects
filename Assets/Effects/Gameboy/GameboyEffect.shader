Shader "Hidden/GameboyEffect" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 _Color1, _Color2, _Color3, _Color4;
			float _Transition12, _Transition23, _Transition34;

			fixed4 frag (v2f i) : SV_Target {
				float3 col = tex2D(_MainTex, i.uv);
				float lum = saturate(0.2126*col.r + 0.7152*col.g + 0.0722*col.b);

				if (lum < _Transition12) {
					return _Color1;
				}
				else if (lum < _Transition23) {
					return _Color2;
				}
				else if (lum < _Transition34) {
					return _Color3;
				}
				else {
					return _Color4;
				}
			}
			ENDCG
		}
	}
}
