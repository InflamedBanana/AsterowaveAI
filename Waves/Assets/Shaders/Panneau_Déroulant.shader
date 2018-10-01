Shader "Custom/Panneau_Pan" {
 Properties 
 {
     _Texte ("Symbole", 2D) = "white" {}
     _Alpha ("Alpha", 2D) = "white" {}
    [HDR] _Emissive ("Emissive" , Color) = (1,1,1,1)
     _PanSpeed("Pan Speed", float) = 1

 }
 
 SubShader 
 {
     Tags 
     {
     "Queue"="Transparent"  "RenderType"="Transparent" 

     }
     LOD 200


		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade
		// Use shader model 3.0 target, to get nicer looking lighting
		// Target :: nombre de calculs
		#pragma target 3.0
		 
		 sampler2D _Texte ;
		 sampler2D _Alpha;
		 float4 _Emissive;
		 float _SpeedFlicker;
		 float _PanSpeed;

		 struct Input 
		 {
		     float2 uv_Texte;
		 };
		 
			 void surf (Input IN, inout SurfaceOutputStandard o) 
			 {
			 	 float4 t = tex2D(_Texte, IN.uv_Texte- float2(_Time.x*_PanSpeed,_Time.x*_PanSpeed));
			 	 float4 a = tex2D (_Alpha, IN.uv_Texte- float2(_Time.x*_PanSpeed,_Time.x*_PanSpeed));
			 	 o.Metallic = 0.0;
			 	 o.Albedo = t.rgb;
			 	 o.Alpha = a;
			 }
			 ENDCG
		 }
 
 Fallback "Diffuse"

 }