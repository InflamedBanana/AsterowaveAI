Shader "Custom/Panneau" {
 Properties 
 {
     _Fond ("Fond", 2D) = "white" {}
     _Flicker ("Flicker", 2D) = "white" {}
     _Flicker2 ("Flicker2", 2D) = "white" {}
    [HDR] _Emissive ("Emissive" , Color) = (1,1,1,1)
     _SpeedFlicker("Speed Flicker", Range(0,3)) = 0

 }
 
 SubShader 
 {
     Tags 
     {
     "Queue"="Transparent"  "RenderType"="Transparent" "ForceNoShadowCasting" = "True"

     }
     LOD 200	

     Cull Off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade
		// Use shader model 3.0 target, to get nicer looking lighting
		// Target :: nombre de calculs
		#pragma target 3.0
		 
		 sampler2D _Fond;
		 sampler2D  _Flicker;
		 sampler2D  _Flicker2;	
		 float4 _Emissive;
		 float _SpeedFlicker;
		 
		 struct Input 
		 {
		     float2 uv_Fond;
		     float2 uv_Flicker;
		     float2 uv_Flicker2;
		 };
		 
			 void surf (Input IN, inout SurfaceOutputStandard o) 
			 {
			 	 float4 t = tex2D(_Fond,IN.uv_Fond);
			     float4 f = tex2D(_Flicker, float2(1.5*_Time.y*_SpeedFlicker,1.5*_Time.y*_SpeedFlicker) + IN.uv_Flicker );
			     float4 f2 = tex2D(_Flicker2, float2(-1.5*_Time.y*_SpeedFlicker,-1.5*_Time.y*_SpeedFlicker) + IN.uv_Flicker2 );

			 	 o.Emission = _Emissive;
			 	 o.Metallic = 0.0;
			 	 o.Albedo = t.rgb;
			 	 o.Alpha = t.a*(f+f2);
			 }
			 ENDCG
		 }
 
 Fallback "Diffuse"

 }