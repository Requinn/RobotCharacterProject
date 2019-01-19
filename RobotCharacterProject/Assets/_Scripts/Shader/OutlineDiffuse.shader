Shader "OUtline/OutlineDiffuse"
{
	Properties
	{
		_MainTex("Base(RGB)", 2D) = "white" {}
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline Thickness", Range(0.0 , .5)) = 0.075
	}

		CGINCLUDE
#include "UnityCG.cginc"

			struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};

		struct v2f {
			float4 pos : POSITION;
			float4 col : COLOR;
		};

		uniform float _Outline;
		uniform float4 _OutlineColor;

		v2f vert(appdata v) {
			//make a copy of incoming vertex data scaled to normal direction
			v2f o;
			v.vertex *= (1 + _Outline);
			o.pos = UnityObjectToClipPos(v.vertex);
			//set color at that vertex to whatever we have set
			o.col = _OutlineColor;
			return o;
		}
		ENDCG

			SubShader{
				CGPROGRAM
			#pragma surface surf Lambert
			sampler2D _MainTex;
			fixed4 _Color;

			struct Input {
				float2 uv_MainTex;
			};

			//set alpha and albedo so we can see it
			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			ENDCG

				Pass{
					Name "OUTLINE"
					Tags{"Lightmode" = "Always"}
					Cull Front //hide front facing geometry to get just the ones in the back, so now it shows an outline with original object inside of it
					ZWrite On
					ColorMask RGB
					Blend SrcAlpha OneMinusSrcAlpha

					CGPROGRAM
					#pragma vertex vert //Referring to the vertex shader from above
					#pragma fragment frag
					half4 frag(v2f i) : COLOR{return i.col; }
					ENDCG
			}
		}

			SubShader{
				CGPROGRAM
				#pragma surface surf Lambert
				sampler2D _MainTex;
				fixed4 _Color;

			struct Input {
				float2 uv_MainTex;
			};

			//set alpha and albedo so we can see it
			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
			ENDCG

				Pass{
				Name "OUTLINE"
				Tags{ "Lightmode" = "Always" }
				Cull Front //hide front facing geometry to get just the ones in the back, so now it shows an outline with original object inside of it
				ZWrite On
				ColorMask RGB
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert //Referring to the vertex shader from above
				#pragma exclude_renderers gles xbox360 ps3
				ENDCG
				SetTexture[_MainTex] {combine primary}
			}
			}
				Fallback "Diffuse"

}
