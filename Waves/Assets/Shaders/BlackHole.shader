Shader "Custom/BlackHole"
 {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_FlowMap("Flow Map",2D) = "white" {}
		_Speed("Speed", Float) = 5

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _FlowMap;
		float _Speed;

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_FlowMap;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			float s = sin(_Time.x * _Speed);
			float c = cos(_Time.x * _Speed);
			// Albedo comes from a texture tinted by color
			float2x2 rotateUV = float2x2(c,-s,s,c);
			IN.uv_MainTex -= 0.5;
			IN.uv_MainTex = mul(IN.uv_FlowMap,rotateUV);
			IN.uv_MainTex += 0.5;
//			float2 flow = tex2D(_FlowMap,IN.uv_MainTex).rg;
//			float2 finalUV = float2(IN.uv_MainTex.x*flow.x,IN.uv_MainTex.y*flow.y);


			fixed4 t = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = t.rgb;
			// Metallic and smoothness come from slider variables
			o.Alpha = t.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
