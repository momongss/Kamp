Shader "Custom/CampingPack_Moon" {
	Properties {
		_Color ("MoonColor", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		ZWrite Off 
		Fog { Mode Off }
		CGPROGRAM

		#pragma surface surf Standard fullforwardshadows nofog
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
		};
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = _Color.rgb;
			o.Emission = _Color;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
