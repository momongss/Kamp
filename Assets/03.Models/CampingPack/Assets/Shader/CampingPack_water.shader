Shader "Custom/CampingPack_water" {
	Properties {
		_Color ("Base color", Color) = (1,1,1,1)
		_BumpTex ("Wave Normal", 2D) = "bump" {}
		_Cube ("Cube Map", cube) = ""{}
		_Scroll ("Rim_pow", range(0.5,3)) = 2.2
		_wavScroll ("Wave_pow", range(0,3)) = 2.2
	}

	SubShader {
		Tags { "RenderType"="Queue"}

		GrabPass{}
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert noambient

		sampler2D _MainTex;
		sampler2D _BumpTex;
		samplerCUBE _Cube;
		sampler2D _GrabTexture;
		fixed _Scroll;
		fixed _wavScroll;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpTex;
			float3 worldRefl;
			float3 viewDir;
			float4 screenPos;
			INTERNAL_DATA
		};

		//----Vertex Shader
		void vert(inout appdata_full v){
			v.vertex.y += cos(abs(v.texcoord.y*2-1)*30 +_Time.x *30) *_wavScroll;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			float3 nr1 = UnpackNormal(tex2D (_BumpTex, IN.uv_BumpTex + _Time.y*0.15));
			float3 nr2 = UnpackNormal(tex2D (_BumpTex, float2(IN.uv_BumpTex.x , IN.uv_BumpTex.y - _Time.x*0.3 )));

			float3 screenUV = IN.screenPos.xyz / IN.screenPos.w;

			o.Normal = (nr1+nr2) *0.5;

			//----Grabtexture
			float3 Cube = texCUBE(_Cube, WorldReflectionVector(IN,o.Normal)).rgb;
			float4 Grab = tex2D(_GrabTexture, screenUV.xy + o.Normal.xy*0.2);

			//----water ref
			float rim = pow(saturate(dot(normalize(IN.viewDir), o.Normal)), _Scroll);
			
			

			//----carl
			o.Albedo = _Color;
			o.Emission = saturate(lerp(Grab,Cube,1-rim)) ;
			o.Alpha =1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
