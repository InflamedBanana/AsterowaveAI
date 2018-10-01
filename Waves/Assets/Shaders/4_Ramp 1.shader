Shader "Cours/CustomLighting/4_RampMultiplied" {
	Properties {
		_MainTex("Albedo",2D) = "white"{}
		_Ramp("Ramp",2D) = "white"{}
		_BumpMap("Normal",2D) = "bump"{}
		_PanSpeed("Pan Speed", float) = 1
	
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf CustomLambert fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Ramp;
		sampler2D _BumpMap; 
		float _PanSpeed;

		float4 LightingCustomLambert(SurfaceOutput s,half3 lightDir,half atten)
		{
			float Ndot = (dot(s.Normal, lightDir)+1)/2.05;
			float3 ramp = tex2D(_Ramp,float2 (Ndot.x,0.0)).rgb;
			float4 c;
			c.rgb = s.Albedo * _LightColor0.rgb* (ramp * atten);
			c.a = 1;
			return c ;
		}

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float4 screenPos;

		};


		void surf (Input IN, inout SurfaceOutput o) 
		{
			float2 screenUV = IN.screenPos.xy/IN.screenPos.w;
			fixed4 c = tex2D(_MainTex,IN.uv_MainTex- float2(_Time.x*_PanSpeed,_Time.x*_PanSpeed));
			fixed4 c2 = tex2D(_Ramp,screenUV);
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			o.Albedo = c.rgb *15 ;
	

		}
		ENDCG
	}
	FallBack "Diffuse"
}
