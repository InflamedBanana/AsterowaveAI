Shader "Waves/Portal" 
{
Properties {
	_Refraction ("Refraction", Range (0.00, 100.0)) = 1.0
	_DistortTex ("Base (RGB)", 2D) = "white" {}
	_Color("Color",Color) = (1,1,1,1)
	_Strenght("Strenght",Float) = 300
	_Transparancy("Transparancy",Range(0,1)) = 0
}

SubShader 
{	
	Tags { "RenderType"="Transparent" "Queue"="Overlay" "ForceNoShadowCasting" = "True "}
	LOD 100
	GrabPass 
	{ 
		
	}
	
CGPROGRAM
//#pragma exclude_renderers gles

#pragma surface surf NoLighting 
#pragma vertex vert
#pragma target 4.0

fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
    {
        fixed4 c;
        c.rgb = s.Albedo; 
        c.a = s.Alpha;
        return c;
    }

sampler2D _GrabTexture : register(s0);
sampler2D _DistortTex : register(s2);
float _Refraction;
float4 _Color;
float _Strenght;
float _Transparancy;

float4 _GrabTexture_TexelSize;

struct Input 
{
	float2 uv_DistortTex;
	float3 color;
////	float3 worldRefl; 
	float4 screenPos;
	INTERNAL_DATA
};

void vert (inout appdata_full v, out Input o)
{
  UNITY_INITIALIZE_OUTPUT(Input,o);
//  o.color = v.color;
}

void surf (Input IN, inout SurfaceOutput o) 
{
    float3 distort = tex2D(_DistortTex, IN.uv_DistortTex) * IN.color.rgb;
    float2 offset = distort * _Refraction * _GrabTexture_TexelSize.xy * _Strenght;
	IN.screenPos.xy = offset * IN.screenPos.z + IN.screenPos.xy;	
	float4 finalColor = lerp(float4(1,1,1,1),_Color,_Transparancy);
	float4 refrColor = tex2Dproj( _GrabTexture, IN.screenPos );
	o.Alpha = refrColor.a;
	o.Albedo = refrColor.rgb * (finalColor *( distort) * 10);
	o.Emission = refrColor.rgb + (finalColor *( distort) * 5);
	o.Emission = lerp(refrColor.rgb,refrColor.rgb * finalColor,distort);
}
ENDCG
}
FallBack "Diffuse"
}