Shader "Custom/Shield"
 {
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RimColor ("Rim Color", Color) = (1,1,1,1)
		_RimPower("Rim Power", Range(0,10)) = 3.0
		_Bump("Normal Map",2D) = "bump"{}
		_Speed("Speed",float) = 1
		_PanSpeed("Speed",float) = 1

	}
	SubShader {
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			float3 viewDir;
		};
		fixed4 _Color;
		float4 _RimColor;
		float _RimPower;
		sampler2D _Bump;
		float _Speed;
		float _PanSpeed;

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			float2 uvPan = IN.uv_MainTex + float2(0,_Time.x * _Speed);
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex - float2(_Time.x*_PanSpeed,_Time.x*_PanSpeed)) * _Color;
			o.Albedo = c.rgb;
			float3 bump = UnpackNormal(tex2D(_Bump,uvPan));
			o.Normal = bump;
			half rim = 1.0 - saturate(dot(normalize(IN.viewDir),o.Normal));
			o.Emission = (c*1.1*_RimColor) * pow(rim,_RimPower);
			o.Alpha = saturate(1-rim) - 0.2f;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
